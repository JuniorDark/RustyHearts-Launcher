namespace RHLauncher
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            UsernameLabel = new Label();
            PasswordLabel = new Label();
            UsernameTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            MinimizeButton = new Button();
            imageListMinBtn = new ImageList(components);
            imageListCloseBtn = new ImageList(components);
            CloseButton = new Button();
            imageListLogin = new ImageList(components);
            LoginButton = new Button();
            RegisterButton = new Button();
            imageListRegister = new ImageList(components);
            CheckBoxAutoLogin = new CheckBox();
            CheckBoxSaveUser = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            progressBarLogin = new ProgressBar();
            ForgotPwdLabel = new Label();
            VersionLabel = new Label();
            SuspendLayout();
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.BackColor = Color.Transparent;
            UsernameLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            UsernameLabel.ForeColor = Color.White;
            UsernameLabel.Location = new Point(612, 199);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(76, 25);
            UsernameLabel.TabIndex = 0;
            UsernameLabel.Text = "Username";
            UsernameLabel.UseCompatibleTextRendering = true;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.BackColor = Color.Transparent;
            PasswordLabel.Font = new Font("Segoe UI Black", 11F, FontStyle.Bold, GraphicsUnit.Point);
            PasswordLabel.ForeColor = Color.White;
            PasswordLabel.Location = new Point(612, 248);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(81, 20);
            PasswordLabel.TabIndex = 1;
            PasswordLabel.Text = "Password";
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UsernameTextBox.Location = new Point(612, 222);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(210, 21);
            UsernameTextBox.TabIndex = 2;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.Location = new Point(613, 271);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(210, 21);
            PasswordTextBox.TabIndex = 3;
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
            MinimizeButton.Location = new Point(752, 7);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(25, 26);
            MinimizeButton.TabIndex = 5;
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
            // imageListCloseBtn
            // 
            imageListCloseBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListCloseBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListCloseBtn.ImageStream");
            imageListCloseBtn.TransparentColor = Color.Transparent;
            imageListCloseBtn.Images.SetKeyName(0, "button_close_normal.png");
            imageListCloseBtn.Images.SetKeyName(1, "button_close_active.png");
            imageListCloseBtn.Images.SetKeyName(2, "button_close_down.png");
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
            CloseButton.Location = new Point(827, 12);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(32, 29);
            CloseButton.TabIndex = 7;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseDown += CloseButton_OnMouseDown;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            CloseButton.MouseHover += CloseButton_MouseHover;
            // 
            // imageListLogin
            // 
            imageListLogin.ColorDepth = ColorDepth.Depth32Bit;
            imageListLogin.ImageStream = (ImageListStreamer)resources.GetObject("imageListLogin.ImageStream");
            imageListLogin.TransparentColor = Color.Transparent;
            imageListLogin.Images.SetKeyName(0, "button_login_normal.png");
            imageListLogin.Images.SetKeyName(1, "button_login_active.png");
            imageListLogin.Images.SetKeyName(2, "button_login_down.png");
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Transparent;
            LoginButton.FlatAppearance.BorderSize = 0;
            LoginButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            LoginButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.ImageIndex = 0;
            LoginButton.ImageList = imageListLogin;
            LoginButton.Location = new Point(612, 349);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(210, 60);
            LoginButton.TabIndex = 8;
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            LoginButton.MouseDown += LoginButton_OnMouseDown;
            LoginButton.MouseLeave += LoginButton_MouseLeave;
            LoginButton.MouseHover += LoginButton_MouseHover;
            // 
            // RegisterButton
            // 
            RegisterButton.BackColor = Color.Transparent;
            RegisterButton.FlatAppearance.BorderSize = 0;
            RegisterButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            RegisterButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            RegisterButton.FlatStyle = FlatStyle.Flat;
            RegisterButton.ImageIndex = 0;
            RegisterButton.ImageList = imageListRegister;
            RegisterButton.Location = new Point(666, 433);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(101, 26);
            RegisterButton.TabIndex = 9;
            RegisterButton.UseVisualStyleBackColor = false;
            RegisterButton.Click += RegisterButton_Click;
            RegisterButton.MouseDown += RegisterButton_OnMouseDown;
            RegisterButton.MouseLeave += RegisterButton_MouseLeave;
            RegisterButton.MouseHover += RegisterButton_MouseHover;
            // 
            // imageListRegister
            // 
            imageListRegister.ColorDepth = ColorDepth.Depth32Bit;
            imageListRegister.ImageStream = (ImageListStreamer)resources.GetObject("imageListRegister.ImageStream");
            imageListRegister.TransparentColor = Color.Transparent;
            imageListRegister.Images.SetKeyName(0, "button_register_normal.png");
            imageListRegister.Images.SetKeyName(1, "button_register_active.png");
            imageListRegister.Images.SetKeyName(2, "button_register_down.png");
            // 
            // CheckBoxAutoLogin
            // 
            CheckBoxAutoLogin.AutoSize = true;
            CheckBoxAutoLogin.BackColor = Color.Transparent;
            CheckBoxAutoLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            CheckBoxAutoLogin.ForeColor = Color.White;
            CheckBoxAutoLogin.Location = new Point(612, 325);
            CheckBoxAutoLogin.Name = "CheckBoxAutoLogin";
            CheckBoxAutoLogin.Size = new Size(106, 24);
            CheckBoxAutoLogin.TabIndex = 11;
            CheckBoxAutoLogin.Text = "Auto Login";
            CheckBoxAutoLogin.UseVisualStyleBackColor = false;
            CheckBoxAutoLogin.CheckedChanged += CheckBoxAutoLogin_CheckedChanged;
            // 
            // CheckBoxSaveUser
            // 
            CheckBoxSaveUser.AutoSize = true;
            CheckBoxSaveUser.BackColor = Color.Transparent;
            CheckBoxSaveUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            CheckBoxSaveUser.ForeColor = Color.White;
            CheckBoxSaveUser.ImeMode = ImeMode.NoControl;
            CheckBoxSaveUser.Location = new Point(612, 302);
            CheckBoxSaveUser.Name = "CheckBoxSaveUser";
            CheckBoxSaveUser.Size = new Size(173, 23);
            CheckBoxSaveUser.TabIndex = 12;
            CheckBoxSaveUser.Text = "Remember Username";
            CheckBoxSaveUser.UseVisualStyleBackColor = false;
            CheckBoxSaveUser.CheckedChanged += CheckBoxSaveUser_CheckedChanged;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            // 
            // progressBarLogin
            // 
            progressBarLogin.BackColor = SystemColors.ControlDark;
            progressBarLogin.ForeColor = Color.Transparent;
            progressBarLogin.Location = new Point(788, 369);
            progressBarLogin.Name = "progressBarLogin";
            progressBarLogin.Size = new Size(24, 23);
            progressBarLogin.Style = ProgressBarStyle.Continuous;
            progressBarLogin.TabIndex = 13;
            progressBarLogin.Visible = false;
            // 
            // ForgotPwdLabel
            // 
            ForgotPwdLabel.AutoSize = true;
            ForgotPwdLabel.BackColor = Color.Transparent;
            ForgotPwdLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            ForgotPwdLabel.ForeColor = Color.White;
            ForgotPwdLabel.Location = new Point(652, 410);
            ForgotPwdLabel.Name = "ForgotPwdLabel";
            ForgotPwdLabel.Size = new Size(134, 20);
            ForgotPwdLabel.TabIndex = 14;
            ForgotPwdLabel.Text = "Forgot Password?";
            ForgotPwdLabel.Click += ForgotPwdLabel_Click;
            ForgotPwdLabel.MouseLeave += ForgotPwdLabel_MouseLeave;
            ForgotPwdLabel.MouseHover += ForgotPwdLabel_MouseHover;
            // 
            // VersionLabel
            // 
            VersionLabel.AutoSize = true;
            VersionLabel.BackColor = Color.Transparent;
            VersionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            VersionLabel.ForeColor = Color.White;
            VersionLabel.Location = new Point(8, 471);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(62, 19);
            VersionLabel.TabIndex = 15;
            VersionLabel.Text = "Version:";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Magenta;
            BackgroundImage = Properties.Resources.login_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(874, 497);
            Controls.Add(VersionLabel);
            Controls.Add(ForgotPwdLabel);
            Controls.Add(progressBarLogin);
            Controls.Add(CheckBoxSaveUser);
            Controls.Add(CheckBoxAutoLogin);
            Controls.Add(RegisterButton);
            Controls.Add(LoginButton);
            Controls.Add(CloseButton);
            Controls.Add(MinimizeButton);
            Controls.Add(PasswordTextBox);
            Controls.Add(UsernameTextBox);
            Controls.Add(PasswordLabel);
            Controls.Add(UsernameLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rusty Hearts Launcher";
            TransparencyKey = Color.Magenta;
            FormClosing += LoginForm_FormClosing;
            Load += LoginForm_Load;
            MouseDown += OnMouseDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label UsernameLabel;
        private Label PasswordLabel;
        public TextBox UsernameTextBox;
        private TextBox PasswordTextBox;
        private Button MinimizeButton;
        private ImageList imageListMinBtn;
        private ImageList imageListCloseBtn;
        private Button CloseButton;
        private ImageList imageListLogin;
        private Button LoginButton;
        private Button RegisterButton;
        private ImageList imageListRegister;
        private CheckBox CheckBoxAutoLogin;
        private CheckBox CheckBoxSaveUser;
        private System.Windows.Forms.Timer timer1;
        private ProgressBar progressBarLogin;
        private Label ForgotPwdLabel;
        private Label VersionLabel;
    }
}