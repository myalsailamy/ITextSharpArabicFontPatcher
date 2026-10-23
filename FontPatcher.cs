using System.Text;
using HarfBuzzSharp;

namespace ArabicFontPatcher
{
    public sealed class PatchResult
    {
        public int TotalForms;      // عدد رموز Presentation Forms المعرّفة في Unicode
        public int AlreadyMapped;   // الموجودة أصلاً في الخط
        public int Added;           // التي أمكن ملؤها
        public int Skipped;         // التي تعذّر ملؤها
    }

    /// <summary>
    /// يملأ رموز Arabic Presentation Forms الفارغة في جدول cmap
    /// بأرقام glyph التي يعيدها HarfBuzz من جداول GSUB للخط.
    /// </summary>
    public static class FontPatcher
    {
        public static PatchResult Run(string inputPath, string outputPath, string unicodeDataPath,
                                      bool dryRun, IProgress<string> log)
        {
            byte[] src = File.ReadAllBytes(inputPath);
            if (Encoding.ASCII.GetString(src, 0, 4) == "ttcf")
                throw new InvalidOperationException("ملفات TTC (مجموعات الخطوط) غير مدعومة. استخرج الخط إلى ملف TTF منفرد.");

            var result = new PatchResult();
            var map = new SortedDictionary<int, int>();

            log.Report("قراءة جدول UnicodeData.txt ...");
            var forms = LoadForms(unicodeDataPath);
            result.TotalForms = forms.Count;
            log.Report("عدد رموز Presentation Forms: " + forms.Count);

            using (var blob = Blob.FromFile(inputPath))
            using (var face = new Face(blob, 0))
            using (var font = new HarfBuzzSharp.Font(face))
            {
                log.Report("قراءة جدول cmap الحالي ...");
                for (int cp = 0; cp <= 0x10FFFF; cp++)
                {
                    if (cp >= 0xD800 && cp <= 0xDFFF) continue;
                    if (font.TryGetGlyph(cp, out uint g) && g != 0) map[cp] = (int)g;
                }
                log.Report("عدد الرموز المربوطة حالياً: " + map.Count);

                log.Report("ملء الفراغات عبر HarfBuzz ...");
                foreach (var (cp, tag, b) in forms)
                {
                    if (map.ContainsKey(cp)) { result.AlreadyMapped++; continue; }

                    string s;
                    switch (tag)
                    {
                        case "initial": s = b + "\u200D"; break;
                        case "medial": s = "\u200D" + b + "\u200D"; break;
                        case "final": s = "\u200D" + b; break;
                        default: s = b; break; // isolated وغيرها
                    }

                    using (var buf = new HarfBuzzSharp.Buffer())
                    {
                        buf.Flags = BufferFlags.RemoveDefaultIgnorables;
                        buf.Direction = Direction.RightToLeft;
                        buf.Script = Script.Arabic;
                        buf.AddUtf16(s);
                        font.Shape(buf);
                        var infos = buf.GlyphInfos;
                        if (infos.Length == 1 && infos[0].Codepoint != 0)
                        {
                            map[cp] = (int)infos[0].Codepoint;
                            result.Added++;
                        }
                        else result.Skipped++;
                    }
                }
            }

            log.Report("تمت تغطية " + result.Added + " رمزاً، وتعذّر " + result.Skipped + " رمزاً، وكان موجوداً أصلاً " + result.AlreadyMapped + ".");

            if (!dryRun)
            {
                if (result.Added == 0)
                {
                    log.Report("لا يوجد ما يُضاف، لم يُكتب أي ملف.");
                    return result;
                }
                log.Report("بناء جدول cmap وكتابة الخط ...");
                File.WriteAllBytes(outputPath, Rebuild(src, BuildCmap(map)));
                log.Report("تم الحفظ: " + outputPath);
            }
            return result;
        }

        // ---------- UnicodeData ----------
        static List<(int cp, string tag, string baseText)> LoadForms(string path)
        {
            var l = new List<(int, string, string)>();
            foreach (var line in File.ReadLines(path))
            {
                var f = line.Split(';');
                if (f.Length < 6) continue;
                int cp = Convert.ToInt32(f[0], 16);
                if (!((cp >= 0xFB50 && cp <= 0xFDFF) || (cp >= 0xFE70 && cp <= 0xFEFF))) continue;
                string d = f[5];
                if (!d.StartsWith("<")) continue;
                int e = d.IndexOf('>');
                var sb = new StringBuilder();
                foreach (var h in d.Substring(e + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    sb.Append(char.ConvertFromUtf32(Convert.ToInt32(h, 16)));
                l.Add((cp, d.Substring(1, e - 1), sb.ToString()));
            }
            return l;
        }

        // ---------- بناء cmap ----------
        static void W16(List<byte> o, int v) { o.Add((byte)(v >> 8)); o.Add((byte)v); }
        static void W32(List<byte> o, uint v) { W16(o, (int)(v >> 16)); W16(o, (int)(v & 0xFFFF)); }

        // الصيغة 4: مقاطع بفارق ثابت (idDelta) فتبقى صغيرة الحجم
        static byte[] Format4(SortedDictionary<int, int> map)
        {
            var segs = new List<(int s, int e, int g)>();
            foreach (var kv in map)
            {
                if (kv.Key >= 0xFFFF) break;
                if (segs.Count > 0)
                {
                    var t = segs[segs.Count - 1];
                    if (t.e + 1 == kv.Key && t.g + (kv.Key - t.s) == kv.Value)
                    {
                        segs[segs.Count - 1] = (t.s, kv.Key, t.g);
                        continue;
                    }
                }
                segs.Add((kv.Key, kv.Key, kv.Value));
            }
            segs.Add((0xFFFF, 0xFFFF, 0));
            int n = segs.Count;
            int pow = 1, log = 0;
            while (pow * 2 <= n) { pow *= 2; log++; }

            var o = new List<byte>();
            W16(o, 4); W16(o, 0); W16(o, 0);
            W16(o, n * 2); W16(o, pow * 2); W16(o, log); W16(o, n * 2 - pow * 2);
            foreach (var s in segs) W16(o, s.e);
            W16(o, 0);
            foreach (var s in segs) W16(o, s.s);
            for (int i = 0; i < n; i++)
            {
                int delta = (i == n - 1) ? 1 : (segs[i].g - segs[i].s);
                W16(o, delta & 0xFFFF);
            }
            for (int i = 0; i < n; i++) W16(o, 0);
            if (o.Count > 65535)
                throw new InvalidOperationException("جدول cmap كبير جداً للصيغة 4. جرّب خطاً أصغر.");
            o[2] = (byte)(o.Count >> 8); o[3] = (byte)o.Count;
            return o.ToArray();
        }

        static byte[] Format12(SortedDictionary<int, int> map)
        {
            var gr = new List<(int s, int e, int g)>();
            foreach (var kv in map)
            {
                if (gr.Count > 0)
                {
                    var t = gr[gr.Count - 1];
                    if (t.e + 1 == kv.Key && t.g + (kv.Key - t.s) == kv.Value)
                    {
                        gr[gr.Count - 1] = (t.s, kv.Key, t.g);
                        continue;
                    }
                }
                gr.Add((kv.Key, kv.Key, kv.Value));
            }
            var o = new List<byte>();
            W16(o, 12); W16(o, 0);
            W32(o, (uint)(16 + gr.Count * 12)); W32(o, 0); W32(o, (uint)gr.Count);
            foreach (var g in gr) { W32(o, (uint)g.s); W32(o, (uint)g.e); W32(o, (uint)g.g); }
            return o.ToArray();
        }

        static byte[] BuildCmap(SortedDictionary<int, int> map)
        {
            byte[] f4 = Format4(map), f12 = Format12(map);
            var o = new List<byte>();
            W16(o, 0); W16(o, 4);
            uint baseOff = 4 + 4 * 8;
            uint off4 = baseOff, off12 = baseOff + (uint)f4.Length;
            W16(o, 0); W16(o, 3); W32(o, off4);
            W16(o, 0); W16(o, 4); W32(o, off12);
            W16(o, 3); W16(o, 1); W32(o, off4);
            W16(o, 3); W16(o, 10); W32(o, off12);
            o.AddRange(f4); o.AddRange(f12);
            return o.ToArray();
        }

        // ---------- إعادة تجميع الخط ----------
        static uint R32(byte[] b, int p) =>
            (uint)(b[p] << 24 | b[p + 1] << 16 | b[p + 2] << 8 | b[p + 3]);

        static uint Sum(byte[] d)
        {
            uint s = 0;
            for (int i = 0; i < d.Length; i += 4)
                for (int k = 0; k < 4; k++)
                    s += (uint)((i + k < d.Length ? d[i + k] : 0) << (24 - 8 * k));
            return s;
        }

        static byte[] Rebuild(byte[] src, byte[] cmap)
        {
            int n = src[4] << 8 | src[5];
            var t = new SortedDictionary<string, byte[]>(StringComparer.Ordinal);
            for (int i = 0; i < n; i++)
            {
                int r = 12 + 16 * i;
                string tag = Encoding.ASCII.GetString(src, r, 4);
                int off = (int)R32(src, r + 8), len = (int)R32(src, r + 12);
                t[tag] = src[off..(off + len)];
            }
            t["cmap"] = cmap;
            if (t.ContainsKey("head"))
                for (int i = 8; i < 12; i++) t["head"][i] = 0;

            int count = t.Count, pow = 1, log = 0;
            while (pow * 2 <= count) { pow *= 2; log++; }

            var o = new List<byte>();
            o.AddRange(src[0..4]);
            W16(o, count); W16(o, pow * 16); W16(o, log); W16(o, count * 16 - pow * 16);

            int dataPos = 12 + 16 * count, headOff = -1;
            var body = new List<byte>();
            foreach (var kv in t)
            {
                o.AddRange(Encoding.ASCII.GetBytes(kv.Key));
                W32(o, Sum(kv.Value));
                W32(o, (uint)(dataPos + body.Count));
                W32(o, (uint)kv.Value.Length);
                if (kv.Key == "head") headOff = dataPos + body.Count;
                body.AddRange(kv.Value);
                while (body.Count % 4 != 0) body.Add(0);
            }
            o.AddRange(body);
            var res = o.ToArray();
            if (headOff >= 0)
            {
                uint adj = 0xB1B0AFBA - Sum(res);
                res[headOff + 8] = (byte)(adj >> 24);
                res[headOff + 9] = (byte)(adj >> 16);
                res[headOff + 10] = (byte)(adj >> 8);
                res[headOff + 11] = (byte)adj;
            }
            return res;
        }
    }
}
