namespace RHLauncher
{
    partial class LauncherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
            CloseButton = new Button();
            imageListCloseBtn = new ImageList(components);
            MinimizeButton = new Button();
            imageListMinBtn = new ImageList(components);
            LaunchButton = new Button();
            imageListLaunch = new ImageList(components);
            NameLabel = new Label();
            CharPictureBox = new PictureBox();
            getUpdatePanel = new Panel();
            StopButton = new Button();
            imageListStopBtn = new ImageList(components);
            FileCountLabel = new Label();
            FileSizeLabel = new Label();
            SpeedLabel = new Label();
            TimeLabel = new Label();
            PercentLabel = new Label();
            DownloadingLabel = new Label();
            FileNameLabel = new Label();
            progressBar1 = new ProgressBar();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            LabelNews = new Label();
            imageListButton = new ImageList(components);
            AccOptionsButton = new Button();
            AccPanel = new Panel();
            LogoutButton = new Button();
            imageListMenuButton = new ImageList(components);
            ChangePwdButton = new Button();
            LaunchOptionsButton = new Button();
            imageListLaunchOpt = new ImageList(components);
            LaunchPanel = new Panel();
            OpenSettingsButton = new Button();
            OpenInstallDirButton = new Button();
            ManageButton = new Button();
            CheckUpdateButton = new Button();
            InstallButton = new Button();
            InstallPanel = new Panel();
            UninstallButton = new Button();
            notifyIcon = new NotifyIcon(components);
            ((System.ComponentModel.ISupportInitialize)CharPictureBox).BeginInit();
            getUpdatePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            AccPanel.SuspendLayout();
            LaunchPanel.SuspendLayout();
            InstallPanel.SuspendLayout();
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
            CloseButton.Location = new Point(1203, 8);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(32, 29);
            CloseButton.TabIndex = 9;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseDown += CloseButton_OnMouseDown;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            CloseButton.MouseHover += CloseButton_MouseHover;
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
            // MinimizeButton
            // 
            MinimizeButton.BackColor = Color.Transparent;
            MinimizeButton.FlatAppearance.BorderColor = Color.Black;
            MinimizeButton.FlatAppearance.BorderSize = 0;
            MinimizeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            MinimizeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            MinimizeButton.FlatStyle = FlatStyle.Flat;
            MinimizeButton.ForeColor = Color.Transparent;
            MinimizeButton.ImageIndex = 0;
            MinimizeButton.ImageList = imageListMinBtn;
            MinimizeButton.ImeMode = ImeMode.NoControl;
            MinimizeButton.Location = new Point(1176, 31);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(25, 26);
            MinimizeButton.TabIndex = 8;
            MinimizeButton.UseVisualStyleBackColor = false;
            MinimizeButton.Click += MinimizeButton_Click;
            MinimizeButton.MouseDown += MinimizeButton_OnMouseDown;
            MinimizeButton.MouseLeave += MinimizeButton_MouseLeave;
            MinimizeButton.MouseHover += MinimizeButton_MouseHover;
            // 
            // imageListMinBtn
            // 
            imageListMinBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListMinBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListMinBtn.ImageStream");
            imageListMinBtn.TransparentColor = Color.Transparent;
            imageListMinBtn.Images.SetKeyName(0, "button_minimize_normal.png");
            imageListMinBtn.Images.SetKeyName(1, "button_minimize_active.png");
            imageListMinBtn.Images.SetKeyName(2, "button_minimize_down.png");
            // 
            // LaunchButton
            // 
            LaunchButton.BackColor = Color.Transparent;
            LaunchButton.FlatAppearance.BorderSize = 0;
            LaunchButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            LaunchButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            LaunchButton.FlatStyle = FlatStyle.Flat;
            LaunchButton.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            LaunchButton.ForeColor = Color.White;
            LaunchButton.ImageIndex = 0;
            LaunchButton.ImageList = imageListLaunch;
            LaunchButton.ImeMode = ImeMode.NoControl;
            LaunchButton.Location = new Point(997, 590);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new Size(158, 52);
            LaunchButton.TabIndex = 10;
            LaunchButton.Text = "Launch";
            LaunchButton.UseVisualStyleBackColor = false;
            LaunchButton.MouseDown += LaunchButton_OnMouseDown;
            LaunchButton.MouseLeave += LaunchButton_MouseLeave;
            LaunchButton.MouseHover += LaunchButton_MouseHover;
            // 
            // imageListLaunch
            // 
            imageListLaunch.ColorDepth = ColorDepth.Depth32Bit;
            imageListLaunch.ImageStream = (ImageListStreamer)resources.GetObject("imageListLaunch.ImageStream");
            imageListLaunch.TransparentColor = Color.Transparent;
            imageListLaunch.Images.SetKeyName(0, "launchbutton_bkg_normal.png");
            imageListLaunch.Images.SetKeyName(1, "launchbutton_bkg_active.png");
            imageListLaunch.Images.SetKeyName(2, "launchbutton_bkg_down.png");
            // 
            // NameLabel
            // 
            NameLabel.BackColor = Color.Transparent;
            NameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            NameLabel.ForeColor = Color.White;
            NameLabel.ImeMode = ImeMode.NoControl;
            NameLabel.Location = new Point(822, 32);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(191, 15);
            NameLabel.TabIndex = 13;
            NameLabel.Text = "Welcome, {_windyCode}";
            NameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CharPictureBox
            // 
            CharPictureBox.BackColor = Color.Transparent;
            CharPictureBox.Image = Properties.Resources.character_select_cut_roselle;
            CharPictureBox.Location = new Point(23, 165);
            CharPictureBox.Name = "CharPictureBox";
            CharPictureBox.Size = new Size(367, 497);
            CharPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            CharPictureBox.TabIndex = 17;
            CharPictureBox.TabStop = false;
            CharPictureBox.Click += CharPictureBox_Click;
            // 
            // getUpdatePanel
            // 
            getUpdatePanel.BackColor = Color.Transparent;
            getUpdatePanel.BackgroundImageLayout = ImageLayout.Stretch;
            getUpdatePanel.BorderStyle = BorderStyle.FixedSingle;
            getUpdatePanel.Controls.Add(StopButton);
            getUpdatePanel.Controls.Add(FileCountLabel);
            getUpdatePanel.Controls.Add(FileSizeLabel);
            getUpdatePanel.Controls.Add(SpeedLabel);
            getUpdatePanel.Controls.Add(TimeLabel);
            getUpdatePanel.Controls.Add(PercentLabel);
            getUpdatePanel.Controls.Add(DownloadingLabel);
            getUpdatePanel.Controls.Add(FileNameLabel);
            getUpdatePanel.Controls.Add(progressBar1);
            getUpdatePanel.Location = new Point(397, 570);
            getUpdatePanel.Name = "getUpdatePanel";
            getUpdatePanel.Size = new Size(594, 93);
            getUpdatePanel.TabIndex = 18;
            // 
            // StopButton
            // 
            StopButton.BackColor = Color.Transparent;
            StopButton.FlatAppearance.BorderColor = Color.Black;
            StopButton.FlatAppearance.BorderSize = 0;
            StopButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            StopButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            StopButton.FlatStyle = FlatStyle.Flat;
            StopButton.ForeColor = Color.Transparent;
            StopButton.ImageIndex = 0;
            StopButton.ImageList = imageListStopBtn;
            StopButton.ImeMode = ImeMode.NoControl;
            StopButton.Location = new Point(562, 36);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(25, 25);
            StopButton.TabIndex = 28;
            StopButton.UseVisualStyleBackColor = false;
            StopButton.Click += StopButton_Click;
            StopButton.MouseDown += StopButton_OnMouseDown;
            StopButton.MouseLeave += StopButton_MouseLeave;
            StopButton.MouseHover += StopButton_MouseHover;
            // 
            // imageListStopBtn
            // 
            imageListStopBtn.ColorDepth = ColorDepth.Depth8Bit;
            imageListStopBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListStopBtn.ImageStream");
            imageListStopBtn.TransparentColor = Color.Transparent;
            imageListStopBtn.Images.SetKeyName(0, "button_stop_normal.png");
            imageListStopBtn.Images.SetKeyName(1, "button_stop_active.png");
            imageListStopBtn.Images.SetKeyName(2, "button_stop_down.png");
            // 
            // FileCountLabel
            // 
            FileCountLabel.Anchor = AnchorStyles.Top;
            FileCountLabel.AutoSize = true;
            FileCountLabel.BackColor = Color.Transparent;
            FileCountLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            FileCountLabel.ForeColor = Color.White;
            FileCountLabel.ImeMode = ImeMode.NoControl;
            FileCountLabel.Location = new Point(130, 15);
            FileCountLabel.Name = "FileCountLabel";
            FileCountLabel.Size = new Size(32, 13);
            FileCountLabel.TabIndex = 20;
            FileCountLabel.Text = "(0/0)";
            FileCountLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // FileSizeLabel
            // 
            FileSizeLabel.Anchor = AnchorStyles.Top;
            FileSizeLabel.AutoSize = true;
            FileSizeLabel.BackColor = Color.Transparent;
            FileSizeLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            FileSizeLabel.ForeColor = Color.White;
            FileSizeLabel.ImeMode = ImeMode.NoControl;
            FileSizeLabel.Location = new Point(251, 15);
            FileSizeLabel.Name = "FileSizeLabel";
            FileSizeLabel.Size = new Size(31, 13);
            FileSizeLabel.TabIndex = 19;
            FileSizeLabel.Text = "0MB";
            FileSizeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SpeedLabel
            // 
            SpeedLabel.Anchor = AnchorStyles.Top;
            SpeedLabel.AutoSize = true;
            SpeedLabel.BackColor = Color.Transparent;
            SpeedLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            SpeedLabel.ForeColor = Color.White;
            SpeedLabel.ImeMode = ImeMode.NoControl;
            SpeedLabel.Location = new Point(381, 15);
            SpeedLabel.Name = "SpeedLabel";
            SpeedLabel.Size = new Size(41, 13);
            SpeedLabel.TabIndex = 17;
            SpeedLabel.Text = "0MB/s";
            SpeedLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // TimeLabel
            // 
            TimeLabel.Anchor = AnchorStyles.Top;
            TimeLabel.AutoSize = true;
            TimeLabel.BackColor = Color.Transparent;
            TimeLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            TimeLabel.ForeColor = Color.White;
            TimeLabel.ImeMode = ImeMode.NoControl;
            TimeLabel.Location = new Point(455, 15);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(49, 13);
            TimeLabel.TabIndex = 16;
            TimeLabel.Text = "00:00:00";
            TimeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // PercentLabel
            // 
            PercentLabel.Anchor = AnchorStyles.Top;
            PercentLabel.AutoSize = true;
            PercentLabel.BackColor = Color.Transparent;
            PercentLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            PercentLabel.ForeColor = Color.White;
            PercentLabel.ImeMode = ImeMode.NoControl;
            PercentLabel.Location = new Point(527, 44);
            PercentLabel.Name = "PercentLabel";
            PercentLabel.Size = new Size(23, 13);
            PercentLabel.TabIndex = 15;
            PercentLabel.Text = "0%";
            PercentLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // DownloadingLabel
            // 
            DownloadingLabel.Anchor = AnchorStyles.Top;
            DownloadingLabel.AutoSize = true;
            DownloadingLabel.BackColor = Color.Transparent;
            DownloadingLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            DownloadingLabel.ForeColor = Color.White;
            DownloadingLabel.ImeMode = ImeMode.NoControl;
            DownloadingLabel.Location = new Point(12, 15);
            DownloadingLabel.Name = "DownloadingLabel";
            DownloadingLabel.Size = new Size(78, 13);
            DownloadingLabel.TabIndex = 14;
            DownloadingLabel.Text = "Downloading";
            DownloadingLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // FileNameLabel
            // 
            FileNameLabel.AutoSize = true;
            FileNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FileNameLabel.ForeColor = Color.White;
            FileNameLabel.Location = new Point(10, 70);
            FileNameLabel.Name = "FileNameLabel";
            FileNameLabel.Size = new Size(56, 15);
            FileNameLabel.TabIndex = 1;
            FileNameLabel.Text = "filename";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(11, 38);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(502, 23);
            progressBar1.TabIndex = 0;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.BackColor = Color.RoyalBlue;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(412, 101);
            webView21.Name = "webView21";
            webView21.Size = new Size(475, 405);
            webView21.TabIndex = 21;
            webView21.ZoomFactor = 1D;
            // 
            // LabelNews
            // 
            LabelNews.AutoSize = true;
            LabelNews.BackColor = Color.Transparent;
            LabelNews.FlatStyle = FlatStyle.Flat;
            LabelNews.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            LabelNews.ForeColor = Color.FromArgb(246, 239, 247);
            LabelNews.Location = new Point(599, 64);
            LabelNews.Name = "LabelNews";
            LabelNews.Size = new Size(95, 37);
            LabelNews.TabIndex = 22;
            LabelNews.Text = "NEWS";
            // 
            // imageListButton
            // 
            imageListButton.ColorDepth = ColorDepth.Depth32Bit;
            imageListButton.ImageStream = (ImageListStreamer)resources.GetObject("imageListButton.ImageStream");
            imageListButton.TransparentColor = Color.Transparent;
            imageListButton.Images.SetKeyName(0, "list_button_normal.png");
            imageListButton.Images.SetKeyName(1, "list_button_hover.png");
            // 
            // AccOptionsButton
            // 
            AccOptionsButton.BackColor = Color.Transparent;
            AccOptionsButton.FlatAppearance.BorderColor = Color.Black;
            AccOptionsButton.FlatAppearance.BorderSize = 0;
            AccOptionsButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            AccOptionsButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            AccOptionsButton.FlatStyle = FlatStyle.Flat;
            AccOptionsButton.ForeColor = Color.Transparent;
            AccOptionsButton.ImageIndex = 0;
            AccOptionsButton.ImageList = imageListButton;
            AccOptionsButton.ImeMode = ImeMode.NoControl;
            AccOptionsButton.Location = new Point(1018, 26);
            AccOptionsButton.Name = "AccOptionsButton";
            AccOptionsButton.Size = new Size(25, 26);
            AccOptionsButton.TabIndex = 23;
            AccOptionsButton.UseVisualStyleBackColor = false;
            AccOptionsButton.Click += AccOptionsButton_Click;
            AccOptionsButton.MouseLeave += AccOptionsButton_MouseLeave;
            AccOptionsButton.MouseHover += AccOptionsButton_MouseHover;
            // 
            // AccPanel
            // 
            AccPanel.BackColor = Color.RoyalBlue;
            AccPanel.BackgroundImage = Properties.Resources.help_panel_bkg;
            AccPanel.Controls.Add(LogoutButton);
            AccPanel.Controls.Add(ChangePwdButton);
            AccPanel.Location = new Point(969, 52);
            AccPanel.Name = "AccPanel";
            AccPanel.Size = new Size(124, 62);
            AccPanel.TabIndex = 24;
            AccPanel.Visible = false;
            // 
            // LogoutButton
            // 
            LogoutButton.BackColor = Color.Transparent;
            LogoutButton.FlatAppearance.BorderSize = 0;
            LogoutButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            LogoutButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            LogoutButton.FlatStyle = FlatStyle.Flat;
            LogoutButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            LogoutButton.ForeColor = Color.White;
            LogoutButton.ImageIndex = 0;
            LogoutButton.ImageList = imageListMenuButton;
            LogoutButton.ImeMode = ImeMode.NoControl;
            LogoutButton.Location = new Point(3, 31);
            LogoutButton.Name = "LogoutButton";
            LogoutButton.Size = new Size(118, 28);
            LogoutButton.TabIndex = 29;
            LogoutButton.Text = "Logout";
            LogoutButton.UseVisualStyleBackColor = false;
            LogoutButton.Click += LogoutButton_Click;
            LogoutButton.MouseDown += MenuButton_MouseDown;
            LogoutButton.MouseLeave += MenuButton_MouseLeave;
            LogoutButton.MouseHover += MenuButton_MouseHover;
            // 
            // imageListMenuButton
            // 
            imageListMenuButton.ColorDepth = ColorDepth.Depth32Bit;
            imageListMenuButton.ImageStream = (ImageListStreamer)resources.GetObject("imageListMenuButton.ImageStream");
            imageListMenuButton.TransparentColor = Color.Transparent;
            imageListMenuButton.Images.SetKeyName(0, "menubutton_bkg_normal.png");
            imageListMenuButton.Images.SetKeyName(1, "menubutton_bkg_active.png");
            imageListMenuButton.Images.SetKeyName(2, "menubutton_bkg_down.png");
            // 
            // ChangePwdButton
            // 
            ChangePwdButton.BackColor = Color.Transparent;
            ChangePwdButton.FlatAppearance.BorderSize = 0;
            ChangePwdButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ChangePwdButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ChangePwdButton.FlatStyle = FlatStyle.Flat;
            ChangePwdButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            ChangePwdButton.ForeColor = Color.White;
            ChangePwdButton.ImageIndex = 0;
            ChangePwdButton.ImageList = imageListMenuButton;
            ChangePwdButton.ImeMode = ImeMode.NoControl;
            ChangePwdButton.Location = new Point(3, 3);
            ChangePwdButton.Name = "ChangePwdButton";
            ChangePwdButton.Size = new Size(118, 28);
            ChangePwdButton.TabIndex = 28;
            ChangePwdButton.Text = "Change Password";
            ChangePwdButton.UseVisualStyleBackColor = false;
            ChangePwdButton.Click += ChangePwdButton_Click;
            ChangePwdButton.MouseDown += MenuButton_MouseDown;
            ChangePwdButton.MouseLeave += MenuButton_MouseLeave;
            ChangePwdButton.MouseHover += MenuButton_MouseHover;
            // 
            // LaunchOptionsButton
            // 
            LaunchOptionsButton.BackColor = Color.Transparent;
            LaunchOptionsButton.FlatAppearance.BorderSize = 0;
            LaunchOptionsButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            LaunchOptionsButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            LaunchOptionsButton.FlatStyle = FlatStyle.Flat;
            LaunchOptionsButton.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            LaunchOptionsButton.ForeColor = Color.White;
            LaunchOptionsButton.ImageIndex = 0;
            LaunchOptionsButton.ImageList = imageListLaunchOpt;
            LaunchOptionsButton.ImeMode = ImeMode.NoControl;
            LaunchOptionsButton.Location = new Point(1155, 590);
            LaunchOptionsButton.Name = "LaunchOptionsButton";
            LaunchOptionsButton.Size = new Size(34, 52);
            LaunchOptionsButton.TabIndex = 25;
            LaunchOptionsButton.UseVisualStyleBackColor = false;
            LaunchOptionsButton.Click += LaunchOptionsButton_Click;
            LaunchOptionsButton.MouseDown += LaunchOptionsButton_OnMouseDown;
            LaunchOptionsButton.MouseLeave += LaunchOptionsButton_MouseLeave;
            LaunchOptionsButton.MouseHover += LaunchOptionsButton_MouseHover;
            // 
            // imageListLaunchOpt
            // 
            imageListLaunchOpt.ColorDepth = ColorDepth.Depth32Bit;
            imageListLaunchOpt.ImageStream = (ImageListStreamer)resources.GetObject("imageListLaunchOpt.ImageStream");
            imageListLaunchOpt.TransparentColor = Color.Transparent;
            imageListLaunchOpt.Images.SetKeyName(0, "launchoptionbutton_normal.png");
            imageListLaunchOpt.Images.SetKeyName(1, "launchoptionbutton_active.png");
            imageListLaunchOpt.Images.SetKeyName(2, "launchoptionbutton_down.png");
            // 
            // LaunchPanel
            // 
            LaunchPanel.BackColor = Color.RoyalBlue;
            LaunchPanel.BackgroundImage = Properties.Resources.help_panel_bkg;
            LaunchPanel.Controls.Add(OpenSettingsButton);
            LaunchPanel.Controls.Add(OpenInstallDirButton);
            LaunchPanel.Controls.Add(ManageButton);
            LaunchPanel.Controls.Add(CheckUpdateButton);
            LaunchPanel.Location = new Point(1066, 469);
            LaunchPanel.Name = "LaunchPanel";
            LaunchPanel.Size = new Size(124, 120);
            LaunchPanel.TabIndex = 26;
            LaunchPanel.Visible = false;
            // 
            // OpenSettingsButton
            // 
            OpenSettingsButton.BackColor = Color.Transparent;
            OpenSettingsButton.FlatAppearance.BorderSize = 0;
            OpenSettingsButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            OpenSettingsButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            OpenSettingsButton.FlatStyle = FlatStyle.Flat;
            OpenSettingsButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            OpenSettingsButton.ForeColor = Color.White;
            OpenSettingsButton.ImageIndex = 0;
            OpenSettingsButton.ImageList = imageListMenuButton;
            OpenSettingsButton.ImeMode = ImeMode.NoControl;
            OpenSettingsButton.Location = new Point(3, 2);
            OpenSettingsButton.Name = "OpenSettingsButton";
            OpenSettingsButton.Size = new Size(118, 28);
            OpenSettingsButton.TabIndex = 31;
            OpenSettingsButton.Text = "Game Settings";
            OpenSettingsButton.UseVisualStyleBackColor = false;
            OpenSettingsButton.Click += OpenSettingsButton_Click;
            OpenSettingsButton.MouseDown += MenuButton_MouseDown;
            OpenSettingsButton.MouseLeave += MenuButton_MouseLeave;
            OpenSettingsButton.MouseHover += MenuButton_MouseHover;
            // 
            // OpenInstallDirButton
            // 
            OpenInstallDirButton.BackColor = Color.Transparent;
            OpenInstallDirButton.FlatAppearance.BorderSize = 0;
            OpenInstallDirButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            OpenInstallDirButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            OpenInstallDirButton.FlatStyle = FlatStyle.Flat;
            OpenInstallDirButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            OpenInstallDirButton.ForeColor = Color.White;
            OpenInstallDirButton.ImageIndex = 0;
            OpenInstallDirButton.ImageList = imageListMenuButton;
            OpenInstallDirButton.ImeMode = ImeMode.NoControl;
            OpenInstallDirButton.Location = new Point(3, 60);
            OpenInstallDirButton.Name = "OpenInstallDirButton";
            OpenInstallDirButton.Size = new Size(118, 28);
            OpenInstallDirButton.TabIndex = 28;
            OpenInstallDirButton.Text = "Open Install Dir";
            OpenInstallDirButton.UseVisualStyleBackColor = false;
            OpenInstallDirButton.Click += OpenInstallDirButton_Click;
            OpenInstallDirButton.MouseDown += MenuButton_MouseDown;
            OpenInstallDirButton.MouseLeave += MenuButton_MouseLeave;
            OpenInstallDirButton.MouseHover += MenuButton_MouseHover;
            // 
            // ManageButton
            // 
            ManageButton.BackColor = Color.Transparent;
            ManageButton.FlatAppearance.BorderSize = 0;
            ManageButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ManageButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ManageButton.FlatStyle = FlatStyle.Flat;
            ManageButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            ManageButton.ForeColor = Color.White;
            ManageButton.ImageIndex = 0;
            ManageButton.ImageList = imageListMenuButton;
            ManageButton.ImeMode = ImeMode.NoControl;
            ManageButton.Location = new Point(3, 89);
            ManageButton.Name = "ManageButton";
            ManageButton.Size = new Size(118, 28);
            ManageButton.TabIndex = 30;
            ManageButton.Text = "< Manage";
            ManageButton.UseVisualStyleBackColor = false;
            ManageButton.Click += ManageButton_Click;
            ManageButton.MouseDown += MenuButton_MouseDown;
            ManageButton.MouseLeave += MenuButton_MouseLeave;
            ManageButton.MouseHover += MenuButton_MouseHover;
            // 
            // CheckUpdateButton
            // 
            CheckUpdateButton.BackColor = Color.Transparent;
            CheckUpdateButton.FlatAppearance.BorderSize = 0;
            CheckUpdateButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            CheckUpdateButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            CheckUpdateButton.FlatStyle = FlatStyle.Flat;
            CheckUpdateButton.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            CheckUpdateButton.ForeColor = Color.White;
            CheckUpdateButton.ImageIndex = 0;
            CheckUpdateButton.ImageList = imageListMenuButton;
            CheckUpdateButton.ImeMode = ImeMode.NoControl;
            CheckUpdateButton.Location = new Point(3, 31);
            CheckUpdateButton.Name = "CheckUpdateButton";
            CheckUpdateButton.Size = new Size(118, 28);
            CheckUpdateButton.TabIndex = 27;
            CheckUpdateButton.Text = "Check Update";
            CheckUpdateButton.UseVisualStyleBackColor = false;
            CheckUpdateButton.Click += UpdateCheckButton_Click;
            CheckUpdateButton.MouseDown += MenuButton_MouseDown;
            CheckUpdateButton.MouseLeave += MenuButton_MouseLeave;
            CheckUpdateButton.MouseHover += MenuButton_MouseHover;
            // 
            // InstallButton
            // 
            InstallButton.BackColor = Color.Transparent;
            InstallButton.FlatAppearance.BorderSize = 0;
            InstallButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            InstallButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            InstallButton.FlatStyle = FlatStyle.Flat;
            InstallButton.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point);
            InstallButton.ForeColor = Color.White;
            InstallButton.ImageIndex = 0;
            InstallButton.ImageList = imageListMenuButton;
            InstallButton.ImeMode = ImeMode.NoControl;
            InstallButton.Location = new Point(3, 2);
            InstallButton.Name = "InstallButton";
            InstallButton.Size = new Size(118, 28);
            InstallButton.TabIndex = 28;
            InstallButton.Text = "Install Location";
            InstallButton.UseVisualStyleBackColor = false;
            InstallButton.Click += InstallButton_Click;
            InstallButton.MouseDown += MenuButton_MouseDown;
            InstallButton.MouseLeave += MenuButton_MouseLeave;
            InstallButton.MouseHover += MenuButton_MouseHover;
            // 
            // InstallPanel
            // 
            InstallPanel.BackColor = Color.RoyalBlue;
            InstallPanel.BackgroundImage = Properties.Resources.help_panel_bkg;
            InstallPanel.Controls.Add(UninstallButton);
            InstallPanel.Controls.Add(InstallButton);
            InstallPanel.Location = new Point(941, 469);
            InstallPanel.Name = "InstallPanel";
            InstallPanel.Size = new Size(124, 62);
            InstallPanel.TabIndex = 27;
            InstallPanel.Visible = false;
            // 
            // UninstallButton
            // 
            UninstallButton.BackColor = Color.Transparent;
            UninstallButton.FlatAppearance.BorderSize = 0;
            UninstallButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            UninstallButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            UninstallButton.FlatStyle = FlatStyle.Flat;
            UninstallButton.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point);
            UninstallButton.ForeColor = Color.Red;
            UninstallButton.ImageIndex = 0;
            UninstallButton.ImageList = imageListMenuButton;
            UninstallButton.ImeMode = ImeMode.NoControl;
            UninstallButton.Location = new Point(3, 31);
            UninstallButton.Name = "UninstallButton";
            UninstallButton.Size = new Size(118, 28);
            UninstallButton.TabIndex = 29;
            UninstallButton.Text = "Uninstall";
            UninstallButton.UseVisualStyleBackColor = false;
            UninstallButton.Click += UninstallButton_Click;
            UninstallButton.MouseDown += MenuButton_MouseDown;
            UninstallButton.MouseLeave += MenuButton_MouseLeave;
            UninstallButton.MouseHover += MenuButton_MouseHover;
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Rusty Hearts";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += notifyIcon_MouseClick;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            BackgroundImage = Properties.Resources.launcher_bg;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1239, 700);
            Controls.Add(InstallPanel);
            Controls.Add(LaunchPanel);
            Controls.Add(LaunchOptionsButton);
            Controls.Add(AccPanel);
            Controls.Add(AccOptionsButton);
            Controls.Add(LabelNews);
            Controls.Add(webView21);
            Controls.Add(getUpdatePanel);
            Controls.Add(CharPictureBox);
            Controls.Add(NameLabel);
            Controls.Add(LaunchButton);
            Controls.Add(CloseButton);
            Controls.Add(MinimizeButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LauncherForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rusty Hearts Launcher";
            TransparencyKey = Color.Magenta;
            FormClosing += LauncherForm_FormClosing;
            Load += LauncherForm_Load;
            MouseDown += OnMouseDown;
            Resize += LauncherForm_Resize;
            ((System.ComponentModel.ISupportInitialize)CharPictureBox).EndInit();
            getUpdatePanel.ResumeLayout(false);
            getUpdatePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            AccPanel.ResumeLayout(false);
            LaunchPanel.ResumeLayout(false);
            InstallPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CloseButton;
        private ImageList imageListCloseBtn;
        private Button MinimizeButton;
        private ImageList imageListMinBtn;
        private ImageList imageListLaunch;
        private Label NameLabel;
        private PictureBox CharPictureBox;
        private Panel getUpdatePanel;
        private ProgressBar progressBar1;
        private Label FileSizeLabel;
        private Label SpeedLabel;
        private Label TimeLabel;
        private Label PercentLabel;
        private Label DownloadingLabel;
        private Label FileNameLabel;
        private Button LaunchButton;
        private Label FileCountLabel;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Label LabelNews;
        private ImageList imageListButton;
        private Button AccOptionsButton;
        private Panel AccPanel;
        private Button LaunchOptionsButton;
        private ImageList imageListLaunchOpt;
        private Panel LaunchPanel;
        private Button CheckUpdateButton;
        private ImageList imageListMenuButton;
        private Button LogoutButton;
        private Button ChangePwdButton;
        private Button InstallButton;
        private Button ManageButton;
        private Panel InstallPanel;
        private Button UninstallButton;
        private Button OpenInstallDirButton;
        private Button OpenSettingsButton;
        private ImageList imageListStopBtn;
        private Button StopButton;
        private NotifyIcon notifyIcon;
    }
}