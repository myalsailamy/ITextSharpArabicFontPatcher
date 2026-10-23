using System.ComponentModel;

namespace ArabicFontPatcher
{
    public partial class MainForm : Form
    {
        private const string UnicodeDataUrl = "https://www.unicode.org/Public/UCD/latest/ucd/UnicodeData.txt";

        public MainForm()
        {
            InitializeComponent();

            // لا نلمس الملفات أو الشبكة داخل محرر التصميم
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            string local1 = Path.Combine(AppContext.BaseDirectory, "UnicodeData.txt");
            if (File.Exists(local1)) txtUnicode.Text = local1;
            else if (File.Exists(AppDataUnicodePath())) txtUnicode.Text = AppDataUnicodePath();
            else AppendLog("ملف UnicodeData.txt غير موجود. اضغط \"تنزيل تلقائي\" أو اختره يدوياً.");
        }

        private static string AppDataUnicodePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ArabicFontPatcher", "UnicodeData.txt");
        }

        private void AppendLog(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
        }

        private void SetBusy(bool busy)
        {
            btnPatch.Enabled = !busy;
            btnAnalyze.Enabled = !busy;
            btnBrowseInput.Enabled = !busy;
            btnBrowseOutput.Enabled = !busy;
            btnBrowseUnicode.Enabled = !busy;
            btnDownloadUnicode.Enabled = !busy;
            progressBar.Style = busy ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            UseWaitCursor = busy;
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            if (openFontDialog.ShowDialog(this) != DialogResult.OK) return;
            txtInput.Text = openFontDialog.FileName;
            string dir = Path.GetDirectoryName(openFontDialog.FileName);
            string name = Path.GetFileNameWithoutExtension(openFontDialog.FileName);
            string ext = Path.GetExtension(openFontDialog.FileName);
            txtOutput.Text = Path.Combine(dir, name + "-fixed" + ext);
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            if (txtOutput.Text.Length > 0)
            {
                saveFontDialog.InitialDirectory = Path.GetDirectoryName(txtOutput.Text);
                saveFontDialog.FileName = Path.GetFileName(txtOutput.Text);
            }
            if (saveFontDialog.ShowDialog(this) == DialogResult.OK)
                txtOutput.Text = saveFontDialog.FileName;
        }

        private void btnBrowseUnicode_Click(object sender, EventArgs e)
        {
            if (openUnicodeDialog.ShowDialog(this) == DialogResult.OK)
                txtUnicode.Text = openUnicodeDialog.FileName;
        }

        private async void btnDownloadUnicode_Click(object sender, EventArgs e)
        {
            SetBusy(true);
            try
            {
                AppendLog("تنزيل UnicodeData.txt من unicode.org ...");
                using (var http = new HttpClient())
                {
                    http.Timeout = TimeSpan.FromSeconds(60);
                    byte[] data = await http.GetByteArrayAsync(UnicodeDataUrl);
                    string path = AppDataUnicodePath();
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    await File.WriteAllBytesAsync(path, data);
                    txtUnicode.Text = path;
                    AppendLog("تم التنزيل: " + path);
                }
            }
            catch (Exception ex)
            {
                AppendLog("فشل التنزيل: " + ex.Message);
                MessageBox.Show(this, "تعذّر التنزيل:\n" + ex.Message + "\n\nحمّل الملف يدوياً من:\n" + UnicodeDataUrl,
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            await RunAsync(true);
        }

        private async void btnPatch_Click(object sender, EventArgs e)
        {
            await RunAsync(false);
        }

        private async Task RunAsync(bool dryRun)
        {
            string input = txtInput.Text;
            string output = txtOutput.Text;
            string ucd = txtUnicode.Text;

            if (!File.Exists(input)) { MessageBox.Show(this, "اختر ملف الخط أولاً.", "تنبيه"); return; }
            if (!File.Exists(ucd)) { MessageBox.Show(this, "ملف UnicodeData.txt غير موجود.", "تنبيه"); return; }
            if (!dryRun)
            {
                if (output.Length == 0) { MessageBox.Show(this, "حدد مسار الخط الناتج.", "تنبيه"); return; }
                if (string.Equals(Path.GetFullPath(input), Path.GetFullPath(output), StringComparison.OrdinalIgnoreCase))
                { MessageBox.Show(this, "يجب أن يختلف الخط الناتج عن الأصلي.", "تنبيه"); return; }
            }

            txtLog.Clear();
            SetBusy(true);
            try
            {
                var progress = new Progress<string>(AppendLog);
                PatchResult r = await Task.Run(() => FontPatcher.Run(input, output, ucd, dryRun, progress));
                if (dryRun)
                    MessageBox.Show(this,
                        "الرموز المعرّفة: " + r.TotalForms + "\nموجودة أصلاً: " + r.AlreadyMapped +
                        "\nيمكن ملؤها: " + r.Added + "\nتعذّر ملؤها: " + r.Skipped,
                        "نتيجة التحليل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (r.Added > 0)
                    MessageBox.Show(this, "تم الترقيع بنجاح (" + r.Added + " رمز).\n" + output,
                        "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "لا حاجة للترقيع، الخط مكتمل أو لا يدعم التشكيل عبر HarfBuzz.",
                        "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppendLog("خطأ: " + ex.Message);
                MessageBox.Show(this, ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }
    }
}
