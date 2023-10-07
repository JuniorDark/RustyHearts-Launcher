namespace RHLauncher
{
    partial class RegForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegForm));
            CloseButton = new Button();
            imageListCloseBtn = new ImageList(components);
            MinimizeButton = new Button();
            imageListMinBtn = new ImageList(components);
            CodeLabel = new Label();
            imageListContinueBtn = new ImageList(components);
            ContinueButtonS1 = new Button();
            PasswordLabel = new Label();
            RepeatPasswordLabel = new Label();
            SendEmailButton = new Button();
            imageListSendEmailBtn = new ImageList(components);
            CodeTextBox = new TextBox();
            Stage1Panel = new Panel();
            EmailPictureBox = new PictureBox();
            EmailDescLabel = new Label();
            EmailTextBox = new TextBox();
            EmailLabel = new Label();
            TimerLabel = new Label();
            CodePictureBox = new PictureBox();
            CodeDescLabel = new Label();
            SubTitleLabelS1 = new Label();
            TitleLabelS1 = new Label();
            Stage2Panel = new Panel();
            PwdStrengthLabel = new Label();
            AgreementLabel = new Label();
            AgreeCheckBox = new CheckBox();
            NamePictureBox = new PictureBox();
            NameTextBox = new TextBox();
            NameDescLabel = new Label();
            NameLabel = new Label();
            PwdConfirmPictureBox = new PictureBox();
            PwdPictureBox = new PictureBox();
            PwdConfirmDescLabel = new Label();
            PwdDescLabel = new Label();
            EmailLabelS2 = new Label();
            ReturnLabelS2 = new Label();
            ContinueButtonS2 = new Button();
            PasswordTextBox = new TextBox();
            RepeatPasswordTextBox = new TextBox();
            SubTitleLabelS2 = new Label();
            TitleLabelS2 = new Label();
            imageListTips = new ImageList(components);
            imageListOkBtn = new ImageList(components);
            imageListSendEmailBtn_ko = new ImageList(components);
            imageListOKBtn_ko = new ImageList(components);
            imageListContinueBtn_ko = new ImageList(components);
            Stage1Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EmailPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CodePictureBox).BeginInit();
            Stage2Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NamePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PwdConfirmPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PwdPictureBox).BeginInit();
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
            CloseButton.Location = new Point(756, 12);
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
            MinimizeButton.Location = new Point(681, 7);
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
            // CodeLabel
            // 
            CodeLabel.Anchor = AnchorStyles.Top;
            CodeLabel.AutoEllipsis = true;
            CodeLabel.AutoSize = true;
            CodeLabel.BackColor = Color.Transparent;
            CodeLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            CodeLabel.ForeColor = Color.White;
            CodeLabel.ImeMode = ImeMode.NoControl;
            CodeLabel.Location = new Point(85, 171);
            CodeLabel.Name = "CodeLabel";
            CodeLabel.Size = new Size(124, 19);
            CodeLabel.TabIndex = 15;
            CodeLabel.Text = "Verification Code";
            CodeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // imageListContinueBtn
            // 
            imageListContinueBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListContinueBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListContinueBtn.ImageStream");
            imageListContinueBtn.TransparentColor = Color.Transparent;
            imageListContinueBtn.Images.SetKeyName(0, "Registerwnd.button.continue.normal.png");
            imageListContinueBtn.Images.SetKeyName(1, "Registerwnd.button.continue.active.png");
            imageListContinueBtn.Images.SetKeyName(2, "Registerwnd.button.continue.down.png");
            // 
            // ContinueButtonS1
            // 
            ContinueButtonS1.BackColor = Color.Transparent;
            ContinueButtonS1.FlatAppearance.BorderSize = 0;
            ContinueButtonS1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ContinueButtonS1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ContinueButtonS1.FlatStyle = FlatStyle.Flat;
            ContinueButtonS1.ImageIndex = 0;
            ContinueButtonS1.ImageList = imageListContinueBtn;
            ContinueButtonS1.ImeMode = ImeMode.NoControl;
            ContinueButtonS1.Location = new Point(85, 242);
            ContinueButtonS1.Name = "ContinueButtonS1";
            ContinueButtonS1.Size = new Size(110, 44);
            ContinueButtonS1.TabIndex = 16;
            ContinueButtonS1.UseVisualStyleBackColor = false;
            ContinueButtonS1.Click += ContinueButtonS1_Click;
            ContinueButtonS1.MouseDown += ContinueButtonS1_OnMouseDown;
            ContinueButtonS1.MouseLeave += ContinueButtonS1_MouseLeave;
            ContinueButtonS1.MouseHover += ContinueButtonS1_MouseHover;
            // 
            // PasswordLabel
            // 
            PasswordLabel.Anchor = AnchorStyles.Top;
            PasswordLabel.AutoEllipsis = true;
            PasswordLabel.AutoSize = true;
            PasswordLabel.BackColor = Color.Transparent;
            PasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            PasswordLabel.ForeColor = Color.White;
            PasswordLabel.ImeMode = ImeMode.NoControl;
            PasswordLabel.Location = new Point(98, 195);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(136, 19);
            PasswordLabel.TabIndex = 19;
            PasswordLabel.Text = "Enter the password";
            PasswordLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RepeatPasswordLabel
            // 
            RepeatPasswordLabel.Anchor = AnchorStyles.Top;
            RepeatPasswordLabel.AutoEllipsis = true;
            RepeatPasswordLabel.AutoSize = true;
            RepeatPasswordLabel.BackColor = Color.Transparent;
            RepeatPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            RepeatPasswordLabel.ForeColor = Color.White;
            RepeatPasswordLabel.ImeMode = ImeMode.NoControl;
            RepeatPasswordLabel.Location = new Point(95, 262);
            RepeatPasswordLabel.Name = "RepeatPasswordLabel";
            RepeatPasswordLabel.Size = new Size(160, 19);
            RepeatPasswordLabel.TabIndex = 20;
            RepeatPasswordLabel.Text = "Re-enter the password";
            RepeatPasswordLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SendEmailButton
            // 
            SendEmailButton.BackColor = Color.Transparent;
            SendEmailButton.FlatAppearance.BorderSize = 0;
            SendEmailButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            SendEmailButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            SendEmailButton.FlatStyle = FlatStyle.Flat;
            SendEmailButton.ImageIndex = 0;
            SendEmailButton.ImageList = imageListSendEmailBtn;
            SendEmailButton.ImeMode = ImeMode.NoControl;
            SendEmailButton.Location = new Point(254, 242);
            SendEmailButton.Name = "SendEmailButton";
            SendEmailButton.Size = new Size(110, 44);
            SendEmailButton.TabIndex = 21;
            SendEmailButton.UseVisualStyleBackColor = false;
            SendEmailButton.Click += SendEmailButton_Click;
            SendEmailButton.MouseDown += SendEmailButton_OnMouseDown;
            SendEmailButton.MouseLeave += SendEmailButton_MouseLeave;
            SendEmailButton.MouseHover += SendEmailButton_MouseHover;
            // 
            // imageListSendEmailBtn
            // 
            imageListSendEmailBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListSendEmailBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListSendEmailBtn.ImageStream");
            imageListSendEmailBtn.TransparentColor = Color.Transparent;
            imageListSendEmailBtn.Images.SetKeyName(0, "ChangePwwnd.button.email.normal.png");
            imageListSendEmailBtn.Images.SetKeyName(1, "ChangePwwnd.button.email.active.png");
            imageListSendEmailBtn.Images.SetKeyName(2, "ChangePwwnd.button.email.down.png");
            // 
            // CodeTextBox
            // 
            CodeTextBox.Location = new Point(85, 191);
            CodeTextBox.Name = "CodeTextBox";
            CodeTextBox.Size = new Size(279, 23);
            CodeTextBox.TabIndex = 22;
            CodeTextBox.TextChanged += CodeTextBox_TextChanged;
            // 
            // Stage1Panel
            // 
            Stage1Panel.BackColor = Color.Transparent;
            Stage1Panel.Controls.Add(EmailPictureBox);
            Stage1Panel.Controls.Add(EmailDescLabel);
            Stage1Panel.Controls.Add(EmailTextBox);
            Stage1Panel.Controls.Add(EmailLabel);
            Stage1Panel.Controls.Add(TimerLabel);
            Stage1Panel.Controls.Add(CodePictureBox);
            Stage1Panel.Controls.Add(CodeDescLabel);
            Stage1Panel.Controls.Add(SubTitleLabelS1);
            Stage1Panel.Controls.Add(TitleLabelS1);
            Stage1Panel.Controls.Add(SendEmailButton);
            Stage1Panel.Controls.Add(CodeTextBox);
            Stage1Panel.Controls.Add(ContinueButtonS1);
            Stage1Panel.Controls.Add(CodeLabel);
            Stage1Panel.Location = new Point(0, 79);
            Stage1Panel.Name = "Stage1Panel";
            Stage1Panel.Size = new Size(800, 450);
            Stage1Panel.TabIndex = 23;
            // 
            // EmailPictureBox
            // 
            EmailPictureBox.BackColor = Color.Transparent;
            EmailPictureBox.Location = new Point(377, 108);
            EmailPictureBox.Name = "EmailPictureBox";
            EmailPictureBox.Size = new Size(14, 14);
            EmailPictureBox.TabIndex = 46;
            EmailPictureBox.TabStop = false;
            // 
            // EmailDescLabel
            // 
            EmailDescLabel.Anchor = AnchorStyles.Top;
            EmailDescLabel.AutoSize = true;
            EmailDescLabel.BackColor = Color.Transparent;
            EmailDescLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            EmailDescLabel.ForeColor = Color.White;
            EmailDescLabel.ImeMode = ImeMode.NoControl;
            EmailDescLabel.Location = new Point(393, 108);
            EmailDescLabel.Name = "EmailDescLabel";
            EmailDescLabel.Size = new Size(0, 13);
            EmailDescLabel.TabIndex = 45;
            EmailDescLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // EmailTextBox
            // 
            EmailTextBox.Location = new Point(85, 103);
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(279, 23);
            EmailTextBox.TabIndex = 44;
            EmailTextBox.TextChanged += EmailTextBox_TextChanged;
            // 
            // EmailLabel
            // 
            EmailLabel.Anchor = AnchorStyles.Top;
            EmailLabel.AutoEllipsis = true;
            EmailLabel.AutoSize = true;
            EmailLabel.BackColor = Color.Transparent;
            EmailLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            EmailLabel.ForeColor = Color.White;
            EmailLabel.ImeMode = ImeMode.NoControl;
            EmailLabel.Location = new Point(88, 82);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(96, 17);
            EmailLabel.TabIndex = 43;
            EmailLabel.Text = "Account Email";
            EmailLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // TimerLabel
            // 
            TimerLabel.Anchor = AnchorStyles.Top;
            TimerLabel.AutoSize = true;
            TimerLabel.BackColor = Color.Transparent;
            TimerLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            TimerLabel.ForeColor = Color.White;
            TimerLabel.ImeMode = ImeMode.NoControl;
            TimerLabel.Location = new Point(366, 255);
            TimerLabel.Name = "TimerLabel";
            TimerLabel.Size = new Size(0, 20);
            TimerLabel.TabIndex = 42;
            TimerLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // CodePictureBox
            // 
            CodePictureBox.BackColor = Color.Transparent;
            CodePictureBox.Location = new Point(375, 196);
            CodePictureBox.Name = "CodePictureBox";
            CodePictureBox.Size = new Size(14, 14);
            CodePictureBox.TabIndex = 41;
            CodePictureBox.TabStop = false;
            // 
            // CodeDescLabel
            // 
            CodeDescLabel.Anchor = AnchorStyles.Top;
            CodeDescLabel.AutoSize = true;
            CodeDescLabel.BackColor = Color.Transparent;
            CodeDescLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            CodeDescLabel.ForeColor = Color.White;
            CodeDescLabel.ImeMode = ImeMode.NoControl;
            CodeDescLabel.Location = new Point(391, 196);
            CodeDescLabel.Name = "CodeDescLabel";
            CodeDescLabel.Size = new Size(0, 13);
            CodeDescLabel.TabIndex = 40;
            CodeDescLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // SubTitleLabelS1
            // 
            SubTitleLabelS1.Anchor = AnchorStyles.Top;
            SubTitleLabelS1.AutoEllipsis = true;
            SubTitleLabelS1.AutoSize = true;
            SubTitleLabelS1.BackColor = Color.Transparent;
            SubTitleLabelS1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            SubTitleLabelS1.ForeColor = Color.White;
            SubTitleLabelS1.ImeMode = ImeMode.NoControl;
            SubTitleLabelS1.Location = new Point(101, 44);
            SubTitleLabelS1.Name = "SubTitleLabelS1";
            SubTitleLabelS1.Size = new Size(93, 19);
            SubTitleLabelS1.TabIndex = 24;
            SubTitleLabelS1.Text = "Rusty Hearts";
            SubTitleLabelS1.TextAlign = ContentAlignment.TopCenter;
            // 
            // TitleLabelS1
            // 
            TitleLabelS1.Anchor = AnchorStyles.Top;
            TitleLabelS1.AutoEllipsis = true;
            TitleLabelS1.AutoSize = true;
            TitleLabelS1.BackColor = Color.Transparent;
            TitleLabelS1.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            TitleLabelS1.ForeColor = Color.White;
            TitleLabelS1.ImeMode = ImeMode.NoControl;
            TitleLabelS1.Location = new Point(85, 2);
            TitleLabelS1.Name = "TitleLabelS1";
            TitleLabelS1.Size = new Size(235, 37);
            TitleLabelS1.TabIndex = 23;
            TitleLabelS1.Text = "Register Account";
            TitleLabelS1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Stage2Panel
            // 
            Stage2Panel.BackColor = Color.Transparent;
            Stage2Panel.Controls.Add(PwdStrengthLabel);
            Stage2Panel.Controls.Add(AgreementLabel);
            Stage2Panel.Controls.Add(AgreeCheckBox);
            Stage2Panel.Controls.Add(NamePictureBox);
            Stage2Panel.Controls.Add(NameTextBox);
            Stage2Panel.Controls.Add(NameDescLabel);
            Stage2Panel.Controls.Add(NameLabel);
            Stage2Panel.Controls.Add(PwdConfirmPictureBox);
            Stage2Panel.Controls.Add(PwdPictureBox);
            Stage2Panel.Controls.Add(PwdConfirmDescLabel);
            Stage2Panel.Controls.Add(PwdDescLabel);
            Stage2Panel.Controls.Add(EmailLabelS2);
            Stage2Panel.Controls.Add(ReturnLabelS2);
            Stage2Panel.Controls.Add(ContinueButtonS2);
            Stage2Panel.Controls.Add(PasswordTextBox);
            Stage2Panel.Controls.Add(RepeatPasswordTextBox);
            Stage2Panel.Controls.Add(SubTitleLabelS2);
            Stage2Panel.Controls.Add(TitleLabelS2);
            Stage2Panel.Controls.Add(PasswordLabel);
            Stage2Panel.Controls.Add(RepeatPasswordLabel);
            Stage2Panel.Location = new Point(0, 79);
            Stage2Panel.Name = "Stage2Panel";
            Stage2Panel.Size = new Size(800, 450);
            Stage2Panel.TabIndex = 25;
            // 
            // PwdStrengthLabel
            // 
            PwdStrengthLabel.Anchor = AnchorStyles.Top;
            PwdStrengthLabel.AutoSize = true;
            PwdStrengthLabel.BackColor = Color.Transparent;
            PwdStrengthLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            PwdStrengthLabel.ForeColor = Color.White;
            PwdStrengthLabel.ImeMode = ImeMode.NoControl;
            PwdStrengthLabel.Location = new Point(93, 242);
            PwdStrengthLabel.Name = "PwdStrengthLabel";
            PwdStrengthLabel.Size = new Size(0, 13);
            PwdStrengthLabel.TabIndex = 42;
            PwdStrengthLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // AgreementLabel
            // 
            AgreementLabel.Anchor = AnchorStyles.Top;
            AgreementLabel.AutoEllipsis = true;
            AgreementLabel.AutoSize = true;
            AgreementLabel.BackColor = Color.Transparent;
            AgreementLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            AgreementLabel.ForeColor = Color.Gainsboro;
            AgreementLabel.ImeMode = ImeMode.NoControl;
            AgreementLabel.Location = new Point(259, 321);
            AgreementLabel.Name = "AgreementLabel";
            AgreementLabel.Size = new Size(132, 19);
            AgreementLabel.TabIndex = 41;
            AgreementLabel.Text = "\"User Agreement\"";
            AgreementLabel.TextAlign = ContentAlignment.TopCenter;
            AgreementLabel.Click += AgreementLabel_Click;
            AgreementLabel.MouseLeave += AgreementLabel_MouseLeave;
            AgreementLabel.MouseHover += AgreementLabel_MouseHover;
            // 
            // AgreeCheckBox
            // 
            AgreeCheckBox.AutoSize = true;
            AgreeCheckBox.BackColor = Color.Transparent;
            AgreeCheckBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            AgreeCheckBox.ForeColor = Color.White;
            AgreeCheckBox.Location = new Point(95, 320);
            AgreeCheckBox.Name = "AgreeCheckBox";
            AgreeCheckBox.Size = new Size(171, 23);
            AgreeCheckBox.TabIndex = 40;
            AgreeCheckBox.Text = "Agree and accept the";
            AgreeCheckBox.UseVisualStyleBackColor = false;
            AgreeCheckBox.CheckedChanged += AgreeCheckBox_CheckedChanged;
            // 
            // NamePictureBox
            // 
            NamePictureBox.BackColor = Color.Transparent;
            NamePictureBox.Location = new Point(379, 151);
            NamePictureBox.Name = "NamePictureBox";
            NamePictureBox.Size = new Size(14, 14);
            NamePictureBox.TabIndex = 39;
            NamePictureBox.TabStop = false;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(93, 147);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(279, 23);
            NameTextBox.TabIndex = 38;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // NameDescLabel
            // 
            NameDescLabel.Anchor = AnchorStyles.Top;
            NameDescLabel.AutoEllipsis = true;
            NameDescLabel.AutoSize = true;
            NameDescLabel.BackColor = Color.Transparent;
            NameDescLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            NameDescLabel.ForeColor = Color.White;
            NameDescLabel.ImeMode = ImeMode.NoControl;
            NameDescLabel.Location = new Point(395, 152);
            NameDescLabel.MaximumSize = new Size(250, 0);
            NameDescLabel.Name = "NameDescLabel";
            NameDescLabel.Size = new Size(158, 13);
            NameDescLabel.TabIndex = 37;
            NameDescLabel.Text = "6-16 alphanumeric characters";
            NameDescLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // NameLabel
            // 
            NameLabel.Anchor = AnchorStyles.Top;
            NameLabel.AutoSize = true;
            NameLabel.BackColor = Color.Transparent;
            NameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            NameLabel.ForeColor = Color.White;
            NameLabel.ImeMode = ImeMode.NoControl;
            NameLabel.Location = new Point(104, 126);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(107, 19);
            NameLabel.TabIndex = 36;
            NameLabel.Text = "Account Name";
            NameLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // PwdConfirmPictureBox
            // 
            PwdConfirmPictureBox.BackColor = Color.Transparent;
            PwdConfirmPictureBox.Location = new Point(379, 284);
            PwdConfirmPictureBox.Name = "PwdConfirmPictureBox";
            PwdConfirmPictureBox.Size = new Size(14, 14);
            PwdConfirmPictureBox.TabIndex = 35;
            PwdConfirmPictureBox.TabStop = false;
            // 
            // PwdPictureBox
            // 
            PwdPictureBox.BackColor = Color.Transparent;
            PwdPictureBox.Location = new Point(379, 220);
            PwdPictureBox.Name = "PwdPictureBox";
            PwdPictureBox.Size = new Size(14, 14);
            PwdPictureBox.TabIndex = 34;
            PwdPictureBox.TabStop = false;
            // 
            // PwdConfirmDescLabel
            // 
            PwdConfirmDescLabel.Anchor = AnchorStyles.Top;
            PwdConfirmDescLabel.AutoSize = true;
            PwdConfirmDescLabel.BackColor = Color.Transparent;
            PwdConfirmDescLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            PwdConfirmDescLabel.ForeColor = Color.White;
            PwdConfirmDescLabel.ImeMode = ImeMode.NoControl;
            PwdConfirmDescLabel.Location = new Point(395, 284);
            PwdConfirmDescLabel.MaximumSize = new Size(250, 0);
            PwdConfirmDescLabel.Name = "PwdConfirmDescLabel";
            PwdConfirmDescLabel.Size = new Size(116, 13);
            PwdConfirmDescLabel.TabIndex = 33;
            PwdConfirmDescLabel.Text = "Repeat the password";
            PwdConfirmDescLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // PwdDescLabel
            // 
            PwdDescLabel.Anchor = AnchorStyles.Top;
            PwdDescLabel.AutoSize = true;
            PwdDescLabel.BackColor = Color.Transparent;
            PwdDescLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            PwdDescLabel.ForeColor = Color.White;
            PwdDescLabel.ImeMode = ImeMode.NoControl;
            PwdDescLabel.Location = new Point(395, 221);
            PwdDescLabel.MaximumSize = new Size(250, 0);
            PwdDescLabel.Name = "PwdDescLabel";
            PwdDescLabel.Size = new Size(84, 13);
            PwdDescLabel.TabIndex = 32;
            PwdDescLabel.Text = "6-16 characters";
            PwdDescLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // EmailLabelS2
            // 
            EmailLabelS2.AutoSize = true;
            EmailLabelS2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            EmailLabelS2.ForeColor = Color.DodgerBlue;
            EmailLabelS2.Location = new Point(101, 74);
            EmailLabelS2.Name = "EmailLabelS2";
            EmailLabelS2.Size = new Size(47, 20);
            EmailLabelS2.TabIndex = 28;
            EmailLabelS2.Text = "Email";
            // 
            // ReturnLabelS2
            // 
            ReturnLabelS2.Anchor = AnchorStyles.Top;
            ReturnLabelS2.AutoEllipsis = true;
            ReturnLabelS2.AutoSize = true;
            ReturnLabelS2.BackColor = Color.Transparent;
            ReturnLabelS2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            ReturnLabelS2.ForeColor = Color.Gainsboro;
            ReturnLabelS2.ImeMode = ImeMode.NoControl;
            ReturnLabelS2.Location = new Point(98, 420);
            ReturnLabelS2.Name = "ReturnLabelS2";
            ReturnLabelS2.Size = new Size(67, 19);
            ReturnLabelS2.TabIndex = 27;
            ReturnLabelS2.Text = "< Return";
            ReturnLabelS2.TextAlign = ContentAlignment.TopCenter;
            ReturnLabelS2.Click += ReturnLabel_Click;
            ReturnLabelS2.MouseLeave += ReturnLabelS2_MouseLeave;
            ReturnLabelS2.MouseHover += ReturnLabelS2_MouseHover;
            // 
            // ContinueButtonS2
            // 
            ContinueButtonS2.BackColor = Color.Transparent;
            ContinueButtonS2.FlatAppearance.BorderSize = 0;
            ContinueButtonS2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ContinueButtonS2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ContinueButtonS2.FlatStyle = FlatStyle.Flat;
            ContinueButtonS2.ImageIndex = 0;
            ContinueButtonS2.ImageList = imageListContinueBtn;
            ContinueButtonS2.ImeMode = ImeMode.NoControl;
            ContinueButtonS2.Location = new Point(95, 351);
            ContinueButtonS2.Name = "ContinueButtonS2";
            ContinueButtonS2.Size = new Size(110, 44);
            ContinueButtonS2.TabIndex = 26;
            ContinueButtonS2.UseVisualStyleBackColor = false;
            ContinueButtonS2.Click += ContinueButtonS2_Click;
            ContinueButtonS2.MouseLeave += ContinueButtonS2_MouseLeave;
            ContinueButtonS2.MouseHover += ContinueButtonS2_MouseHover;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(93, 215);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(279, 23);
            PasswordTextBox.TabIndex = 25;
            PasswordTextBox.UseSystemPasswordChar = true;
            PasswordTextBox.TextChanged += PasswordTextBox_TextChanged;
            // 
            // RepeatPasswordTextBox
            // 
            RepeatPasswordTextBox.Location = new Point(93, 281);
            RepeatPasswordTextBox.Name = "RepeatPasswordTextBox";
            RepeatPasswordTextBox.Size = new Size(279, 23);
            RepeatPasswordTextBox.TabIndex = 24;
            RepeatPasswordTextBox.UseSystemPasswordChar = true;
            RepeatPasswordTextBox.TextChanged += RepeatPasswordTextBox_TextChanged;
            // 
            // SubTitleLabelS2
            // 
            SubTitleLabelS2.Anchor = AnchorStyles.Top;
            SubTitleLabelS2.AutoEllipsis = true;
            SubTitleLabelS2.AutoSize = true;
            SubTitleLabelS2.BackColor = Color.Transparent;
            SubTitleLabelS2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            SubTitleLabelS2.ForeColor = Color.White;
            SubTitleLabelS2.ImeMode = ImeMode.NoControl;
            SubTitleLabelS2.Location = new Point(101, 42);
            SubTitleLabelS2.Name = "SubTitleLabelS2";
            SubTitleLabelS2.Size = new Size(63, 19);
            SubTitleLabelS2.TabIndex = 22;
            SubTitleLabelS2.Text = "Account";
            SubTitleLabelS2.TextAlign = ContentAlignment.TopCenter;
            // 
            // TitleLabelS2
            // 
            TitleLabelS2.Anchor = AnchorStyles.Top;
            TitleLabelS2.AutoEllipsis = true;
            TitleLabelS2.AutoSize = true;
            TitleLabelS2.BackColor = Color.Transparent;
            TitleLabelS2.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            TitleLabelS2.ForeColor = Color.White;
            TitleLabelS2.ImeMode = ImeMode.NoControl;
            TitleLabelS2.Location = new Point(85, 3);
            TitleLabelS2.Name = "TitleLabelS2";
            TitleLabelS2.Size = new Size(218, 37);
            TitleLabelS2.TabIndex = 21;
            TitleLabelS2.Text = "Account Details";
            TitleLabelS2.TextAlign = ContentAlignment.TopCenter;
            // 
            // imageListTips
            // 
            imageListTips.ColorDepth = ColorDepth.Depth32Bit;
            imageListTips.ImageStream = (ImageListStreamer)resources.GetObject("imageListTips.ImageStream");
            imageListTips.TransparentColor = Color.Transparent;
            imageListTips.Images.SetKeyName(0, "tips_error.png");
            imageListTips.Images.SetKeyName(1, "tips_ok.png");
            // 
            // imageListOkBtn
            // 
            imageListOkBtn.ColorDepth = ColorDepth.Depth32Bit;
            imageListOkBtn.ImageStream = (ImageListStreamer)resources.GetObject("imageListOkBtn.ImageStream");
            imageListOkBtn.TransparentColor = Color.Transparent;
            imageListOkBtn.Images.SetKeyName(0, "messagewnd.button.ok.normal.png");
            imageListOkBtn.Images.SetKeyName(1, "messagewnd.button.ok.active.png");
            imageListOkBtn.Images.SetKeyName(2, "messagewnd.button.ok.down.png");
            // 
            // imageListSendEmailBtn_ko
            // 
            imageListSendEmailBtn_ko.ColorDepth = ColorDepth.Depth32Bit;
            imageListSendEmailBtn_ko.ImageStream = (ImageListStreamer)resources.GetObject("imageListSendEmailBtn_ko.ImageStream");
            imageListSendEmailBtn_ko.TransparentColor = Color.Transparent;
            imageListSendEmailBtn_ko.Images.SetKeyName(0, "ChangePwwnd_button_email_normal_ko.png");
            imageListSendEmailBtn_ko.Images.SetKeyName(1, "ChangePwwnd_button_email_active_ko.png");
            imageListSendEmailBtn_ko.Images.SetKeyName(2, "ChangePwwnd_button_email_down_ko.png");
            // 
            // imageListOKBtn_ko
            // 
            imageListOKBtn_ko.ColorDepth = ColorDepth.Depth32Bit;
            imageListOKBtn_ko.ImageStream = (ImageListStreamer)resources.GetObject("imageListOKBtn_ko.ImageStream");
            imageListOKBtn_ko.TransparentColor = Color.Transparent;
            imageListOKBtn_ko.Images.SetKeyName(0, "messagewnd.button.ok.normal.png");
            imageListOKBtn_ko.Images.SetKeyName(1, "messagewnd.button.ok.active.png");
            imageListOKBtn_ko.Images.SetKeyName(2, "messagewnd.button.ok.down.png");
            // 
            // imageListContinueBtn_ko
            // 
            imageListContinueBtn_ko.ColorDepth = ColorDepth.Depth32Bit;
            imageListContinueBtn_ko.ImageStream = (ImageListStreamer)resources.GetObject("imageListContinueBtn_ko.ImageStream");
            imageListContinueBtn_ko.TransparentColor = Color.Transparent;
            imageListContinueBtn_ko.Images.SetKeyName(0, "Registerwnd_button_continue_normal_ko.png");
            imageListContinueBtn_ko.Images.SetKeyName(1, "Registerwnd_button_continue_active_ko.png");
            imageListContinueBtn_ko.Images.SetKeyName(2, "Registerwnd_button_continue_down_ko.png");
            // 
            // RegForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(800, 571);
            Controls.Add(CloseButton);
            Controls.Add(MinimizeButton);
            Controls.Add(Stage1Panel);
            Controls.Add(Stage2Panel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "RegForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register Account";
            FormClosing += RegForm_FormClosing;
            Load += RegForm_Load;
            Stage1Panel.ResumeLayout(false);
            Stage1Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EmailPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)CodePictureBox).EndInit();
            Stage2Panel.ResumeLayout(false);
            Stage2Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NamePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PwdConfirmPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PwdPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button CloseButton;
        private ImageList imageListCloseBtn;
        private Button MinimizeButton;
        private ImageList imageListMinBtn;
        private Label CodeLabel;
        private ImageList imageListContinueBtn;
        private Button ContinueButtonS1;
        private Label PasswordLabel;
        private Label RepeatPasswordLabel;
        private Button SendEmailButton;
        private TextBox CodeTextBox;
        private Label SubTitleLabelS1;
        private Label TitleLabelS1;
        private Panel Stage1Panel;
        private Panel Stage2Panel;
        private Button ContinueButtonS2;
        private TextBox PasswordTextBox;
        private TextBox RepeatPasswordTextBox;
        private Label SubTitleLabelS2;
        private Label TitleLabelS2;
        private Label ReturnLabelS2;
        private Label EmailLabelS2;
        private ImageList imageListSendEmailBtn;
        private PictureBox PwdConfirmPictureBox;
        private PictureBox PwdPictureBox;
        private Label PwdConfirmDescLabel;
        private Label PwdDescLabel;
        private ImageList imageListTips;
        private PictureBox CodePictureBox;
        private Label CodeDescLabel;
        private Label TimerLabel;
        private PictureBox EmailPictureBox;
        private Label EmailDescLabel;
        private TextBox EmailTextBox;
        private Label EmailLabel;
        private ImageList imageListOkBtn;
        private PictureBox NamePictureBox;
        private TextBox NameTextBox;
        private Label NameDescLabel;
        private Label NameLabel;
        private CheckBox AgreeCheckBox;
        private Label AgreementLabel;
        private Label PwdStrengthLabel;
        private ImageList imageListSendEmailBtn_ko;
        private ImageList imageListOKBtn_ko;
        private ImageList imageListContinueBtn_ko;
    }
}