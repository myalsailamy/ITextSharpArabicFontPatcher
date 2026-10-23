namespace ArabicFontPatcher
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblInput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.lblUnicode = new System.Windows.Forms.Label();
            this.txtUnicode = new System.Windows.Forms.TextBox();
            this.btnBrowseUnicode = new System.Windows.Forms.Button();
            this.btnDownloadUnicode = new System.Windows.Forms.Button();
            this.btnPatch = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.openFontDialog = new System.Windows.Forms.OpenFileDialog();
            this.openUnicodeDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFontDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInput.Location = new System.Drawing.Point(630, 25);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(110, 23);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "ملف الخط (المدخل):";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(110, 22);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(510, 25);
            this.txtInput.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseInput.Location = new System.Drawing.Point(20, 20);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(80, 29);
            this.btnBrowseInput.TabIndex = 2;
            this.btnBrowseInput.Text = "استعراض...";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutput.Location = new System.Drawing.Point(630, 65);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(110, 23);
            this.lblOutput.TabIndex = 3;
            this.lblOutput.Text = "الخط الناتج:";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(110, 62);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(510, 25);
            this.txtOutput.TabIndex = 4;
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseOutput.Location = new System.Drawing.Point(20, 60);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(80, 29);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "استعراض...";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // lblUnicode
            // 
            this.lblUnicode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnicode.Location = new System.Drawing.Point(630, 105);
            this.lblUnicode.Name = "lblUnicode";
            this.lblUnicode.Size = new System.Drawing.Size(110, 23);
            this.lblUnicode.TabIndex = 6;
            this.lblUnicode.Text = "UnicodeData.txt:";
            // 
            // txtUnicode
            // 
            this.txtUnicode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnicode.Location = new System.Drawing.Point(215, 102);
            this.txtUnicode.Name = "txtUnicode";
            this.txtUnicode.ReadOnly = true;
            this.txtUnicode.Size = new System.Drawing.Size(405, 25);
            this.txtUnicode.TabIndex = 7;
            // 
            // btnBrowseUnicode
            // 
            this.btnBrowseUnicode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseUnicode.Location = new System.Drawing.Point(115, 100);
            this.btnBrowseUnicode.Name = "btnBrowseUnicode";
            this.btnBrowseUnicode.Size = new System.Drawing.Size(95, 29);
            this.btnBrowseUnicode.TabIndex = 8;
            this.btnBrowseUnicode.Text = "استعراض...";
            this.btnBrowseUnicode.UseVisualStyleBackColor = true;
            this.btnBrowseUnicode.Click += new System.EventHandler(this.btnBrowseUnicode_Click);
            // 
            // btnDownloadUnicode
            // 
            this.btnDownloadUnicode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownloadUnicode.Location = new System.Drawing.Point(15, 100);
            this.btnDownloadUnicode.Name = "btnDownloadUnicode";
            this.btnDownloadUnicode.Size = new System.Drawing.Size(95, 29);
            this.btnDownloadUnicode.TabIndex = 9;
            this.btnDownloadUnicode.Text = "تنزيل تلقائي";
            this.btnDownloadUnicode.UseVisualStyleBackColor = true;
            this.btnDownloadUnicode.Click += new System.EventHandler(this.btnDownloadUnicode_Click);
            // 
            // btnPatch
            // 
            this.btnPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPatch.Location = new System.Drawing.Point(570, 150);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(170, 38);
            this.btnPatch.TabIndex = 10;
            this.btnPatch.Text = "ترقيع الخط وحفظه";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(390, 150);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(170, 38);
            this.btnAnalyze.TabIndex = 11;
            this.btnAnalyze.Text = "تحليل فقط (بدون حفظ)";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(20, 202);
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(720, 18);
            this.progressBar.TabIndex = 12;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtLog.Location = new System.Drawing.Point(20, 232);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(720, 268);
            this.txtLog.TabIndex = 13;
            // 
            // openFontDialog
            // 
            this.openFontDialog.Filter = "ملفات الخطوط (*.ttf;*.otf)|*.ttf;*.otf|كل الملفات (*.*)|*.*";
            this.openFontDialog.Title = "اختر ملف الخط";
            // 
            // openUnicodeDialog
            // 
            this.openUnicodeDialog.FileName = "UnicodeData.txt";
            this.openUnicodeDialog.Filter = "UnicodeData.txt|UnicodeData.txt|ملفات نصية (*.txt)|*.txt";
            this.openUnicodeDialog.Title = "اختر ملف UnicodeData.txt";
            // 
            // saveFontDialog
            // 
            this.saveFontDialog.Filter = "ملفات الخطوط (*.ttf;*.otf)|*.ttf;*.otf";
            this.saveFontDialog.Title = "حفظ الخط الناتج";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 520);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.btnDownloadUnicode);
            this.Controls.Add(this.btnBrowseUnicode);
            this.Controls.Add(this.txtUnicode);
            this.Controls.Add(this.lblUnicode);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblInput);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(640, 400);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "مرقّع الخطوط العربية لـ iTextSharp";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label lblUnicode;
        private System.Windows.Forms.TextBox txtUnicode;
        private System.Windows.Forms.Button btnBrowseUnicode;
        private System.Windows.Forms.Button btnDownloadUnicode;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.OpenFileDialog openFontDialog;
        private System.Windows.Forms.OpenFileDialog openUnicodeDialog;
        private System.Windows.Forms.SaveFileDialog saveFontDialog;
    }
}
