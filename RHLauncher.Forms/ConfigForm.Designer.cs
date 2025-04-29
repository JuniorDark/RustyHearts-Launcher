namespace RHLauncher
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            CloseButton = new Button();
            imageListCloseBtn = new ImageList(components);
            imageListOKBtn = new ImageList(components);
            TitleLabel = new Label();
            imageListBtn = new ImageList(components);
            cbLauncherLanguage = new ComboBox();
            LanguageLabel = new Label();
            OkButton = new Button();
            VersionLabel = new Label();
            toolTip = new ToolTip(components);
            ServiceLabel = new Label();
            cbLauncherService = new ComboBox();
            SuspendLayout();
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.Transparent;
            CloseButton.FlatAppearance.BorderColor = Color.Black;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            CloseButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.ForeColor = Color.Transparent;
            CloseButton.ImageIndex = 0;
            CloseButton.ImageList = imageListCloseBtn;
            CloseButton.ImeMode = ImeMode.NoControl;
            CloseButton.Location = new Point(607, 9);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(32, 29);
            CloseButton.TabIndex = 8;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseDown += Button_MouseDown;
            CloseButton.MouseLeave += Button_MouseLeave;
            CloseButton.MouseHover += Button_MouseHover;
            // 
            // imageListCloseBtn
            // 
            imageListCloseBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListCloseBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListCloseBtn.ImageStream");
            imageListCloseBtn.TransparentColor = Color.Transparent;
            imageListCloseBtn.Images.SetKeyName(0, "button_close_normal.png");
            imageListCloseBtn.Images.SetKeyName(1, "button_close_active.png");
            imageListCloseBtn.Images.SetKeyName(2, "button_close_down.png");
            // 
            // imageListOKBtn
            // 
            imageListOKBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListOKBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListOKBtn.ImageStream");
            imageListOKBtn.TransparentColor = Color.Transparent;
            imageListOKBtn.Images.SetKeyName(0, "messagewnd.button.ok.normal.png");
            imageListOKBtn.Images.SetKeyName(1, "messagewnd.button.ok.active.png");
            imageListOKBtn.Images.SetKeyName(2, "messagewnd.button.ok.down.png");
            // 
            // TitleLabel
            // 
            TitleLabel.Anchor = AnchorStyles.None;
            TitleLabel.AutoEllipsis = true;
            TitleLabel.AutoSize = true;
            TitleLabel.BackColor = Color.Transparent;
            TitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            TitleLabel.ForeColor = Color.White;
            TitleLabel.ImageAlign = ContentAlignment.TopRight;
            TitleLabel.ImeMode = ImeMode.NoControl;
            TitleLabel.Location = new Point(287, 18);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(66, 20);
            TitleLabel.TabIndex = 10;
            TitleLabel.Text = "Settings";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // imageListBtn
            // 
            imageListBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListBtn.ImageStream");
            imageListBtn.TransparentColor = Color.Transparent;
            imageListBtn.Images.SetKeyName(0, "button_normal.png");
            imageListBtn.Images.SetKeyName(1, "button_active.png");
            imageListBtn.Images.SetKeyName(2, "button_down.png");
            // 
            // cbLauncherLanguage
            // 
            cbLauncherLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLauncherLanguage.FormattingEnabled = true;
            cbLauncherLanguage.Items.AddRange(new object[] { "English", "한국어" });
            cbLauncherLanguage.Location = new Point(254, 91);
            cbLauncherLanguage.Name = "cbLauncherLanguage";
            cbLauncherLanguage.Size = new Size(145, 23);
            cbLauncherLanguage.TabIndex = 12;
            cbLauncherLanguage.SelectedIndexChanged += CbLauncherLanguage_SelectedIndexChanged;
            // 
            // LanguageLabel
            // 
            LanguageLabel.Anchor = AnchorStyles.None;
            LanguageLabel.AutoEllipsis = true;
            LanguageLabel.AutoSize = true;
            LanguageLabel.BackColor = Color.Transparent;
            LanguageLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            LanguageLabel.ForeColor = Color.White;
            LanguageLabel.ImageAlign = ContentAlignment.TopRight;
            LanguageLabel.ImeMode = ImeMode.NoControl;
            LanguageLabel.Location = new Point(254, 68);
            LanguageLabel.Name = "LanguageLabel";
            LanguageLabel.Size = new Size(145, 20);
            LanguageLabel.TabIndex = 13;
            LanguageLabel.Text = "Launcher Language";
            LanguageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OkButton
            // 
            OkButton.BackColor = Color.Transparent;
            OkButton.FlatAppearance.BorderSize = 0;
            OkButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            OkButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            OkButton.FlatStyle = FlatStyle.Flat;
            OkButton.ImageIndex = 0;
            OkButton.ImageList = imageListOKBtn;
            OkButton.ImeMode = ImeMode.NoControl;
            OkButton.Location = new Point(268, 218);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(110, 44);
            OkButton.TabIndex = 14;
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            OkButton.MouseDown += Button_MouseDown;
            OkButton.MouseLeave += Button_MouseLeave;
            OkButton.MouseHover += Button_MouseHover;
            // 
            // VersionLabel
            // 
            VersionLabel.AutoSize = true;
            VersionLabel.BackColor = Color.Transparent;
            VersionLabel.Cursor = Cursors.Hand;
            VersionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            VersionLabel.ForeColor = Color.White;
            VersionLabel.Location = new Point(5, 248);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(62, 19);
            VersionLabel.TabIndex = 16;
            VersionLabel.Text = "Version:";
            toolTip.SetToolTip(VersionLabel, "Click to open github repository");
            VersionLabel.Click += VersionLabel_Click;
            // 
            // ServiceLabel
            // 
            ServiceLabel.Anchor = AnchorStyles.None;
            ServiceLabel.AutoEllipsis = true;
            ServiceLabel.AutoSize = true;
            ServiceLabel.BackColor = Color.Transparent;
            ServiceLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            ServiceLabel.ForeColor = Color.White;
            ServiceLabel.ImageAlign = ContentAlignment.TopRight;
            ServiceLabel.ImeMode = ImeMode.NoControl;
            ServiceLabel.Location = new Point(294, 119);
            ServiceLabel.Name = "ServiceLabel";
            ServiceLabel.Size = new Size(59, 20);
            ServiceLabel.TabIndex = 18;
            ServiceLabel.Text = "Service";
            ServiceLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbLauncherService
            // 
            cbLauncherService.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLauncherService.FormattingEnabled = true;
            cbLauncherService.Items.AddRange(new object[] { "JPN", "USA", "CHN", "JPN_BETA", "USA_BETA", "CHN_BETA" });
            cbLauncherService.Location = new Point(254, 142);
            cbLauncherService.Name = "cbLauncherService";
            cbLauncherService.Size = new Size(145, 23);
            cbLauncherService.TabIndex = 17;
            cbLauncherService.SelectedIndexChanged += CbLauncherService_SelectedIndexChanged;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(646, 272);
            Controls.Add(ServiceLabel);
            Controls.Add(cbLauncherService);
            Controls.Add(VersionLabel);
            Controls.Add(OkButton);
            Controls.Add(LanguageLabel);
            Controls.Add(cbLauncherLanguage);
            Controls.Add(TitleLabel);
            Controls.Add(CloseButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConfigForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Message";
            TransparencyKey = Color.Magenta;
            FormClosing += ConfigForm_FormClosing;
            Load += ConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CloseButton;
        private ImageList imageListCloseBtn;
        private ImageList imageListOKBtn;
        private Label TitleLabel;
        private ImageList imageListBtn;
        private ComboBox cbLauncherLanguage;
        private Label LanguageLabel;
        private Button OkButton;
        private Label VersionLabel;
        private ToolTip toolTip;
        private Label ServiceLabel;
        private ComboBox cbLauncherService;
    }
}