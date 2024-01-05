namespace RHLauncher
{
    partial class MsgBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBoxForm));
            CloseButton = new Button();
            imageListCloseBtn = new ImageList(components);
            OkButton = new Button();
            imageListOKBtn = new ImageList(components);
            TitleLabel = new Label();
            TextLabel = new Label();
            tbMessage = new TextBox();
            YesButton = new Button();
            imageListBtn = new ImageList(components);
            NoButton = new Button();
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
            OkButton.Location = new Point(268, 224);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(110, 44);
            OkButton.TabIndex = 9;
            OkButton.UseVisualStyleBackColor = false;
            OkButton.Click += OkButton_Click;
            OkButton.MouseDown += Button_MouseDown;
            OkButton.MouseLeave += Button_MouseLeave;
            OkButton.MouseHover += Button_MouseHover;
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
            TitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            TitleLabel.ForeColor = Color.White;
            TitleLabel.ImageAlign = ContentAlignment.TopRight;
            TitleLabel.ImeMode = ImeMode.NoControl;
            TitleLabel.Location = new Point(285, 20);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(40, 20);
            TitleLabel.TabIndex = 10;
            TitleLabel.Text = "Title";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TextLabel
            // 
            TextLabel.Anchor = AnchorStyles.Left;
            TextLabel.AutoEllipsis = true;
            TextLabel.AutoSize = true;
            TextLabel.BackColor = Color.Transparent;
            TextLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TextLabel.ForeColor = Color.White;
            TextLabel.ImeMode = ImeMode.NoControl;
            TextLabel.Location = new Point(39, 62);
            TextLabel.MaximumSize = new Size(550, 0);
            TextLabel.MinimumSize = new Size(50, 0);
            TextLabel.Name = "TextLabel";
            TextLabel.Size = new Size(50, 15);
            TextLabel.TabIndex = 11;
            TextLabel.Text = "Text";
            // 
            // tbMessage
            // 
            tbMessage.Location = new Point(106, 88);
            tbMessage.Multiline = true;
            tbMessage.Name = "tbMessage";
            tbMessage.ReadOnly = true;
            tbMessage.ScrollBars = ScrollBars.Vertical;
            tbMessage.Size = new Size(441, 129);
            tbMessage.TabIndex = 12;
            tbMessage.WordWrap = false;
            // 
            // YesButton
            // 
            YesButton.BackColor = Color.Transparent;
            YesButton.FlatAppearance.BorderSize = 0;
            YesButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            YesButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            YesButton.FlatStyle = FlatStyle.Flat;
            YesButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            YesButton.ForeColor = Color.White;
            YesButton.ImageIndex = 0;
            YesButton.ImageList = imageListBtn;
            YesButton.ImeMode = ImeMode.NoControl;
            YesButton.Location = new Point(164, 224);
            YesButton.Name = "YesButton";
            YesButton.Size = new Size(110, 44);
            YesButton.TabIndex = 13;
            YesButton.Text = "Yes";
            YesButton.UseVisualStyleBackColor = false;
            YesButton.Click += YesButton_Click;
            YesButton.MouseDown += Button_MouseDown;
            YesButton.MouseLeave += Button_MouseLeave;
            YesButton.MouseHover += Button_MouseHover;
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
            // NoButton
            // 
            NoButton.BackColor = Color.Transparent;
            NoButton.FlatAppearance.BorderSize = 0;
            NoButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            NoButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            NoButton.FlatStyle = FlatStyle.Flat;
            NoButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            NoButton.ForeColor = Color.White;
            NoButton.ImageIndex = 0;
            NoButton.ImageList = imageListBtn;
            NoButton.ImeMode = ImeMode.NoControl;
            NoButton.Location = new Point(373, 224);
            NoButton.Name = "NoButton";
            NoButton.Size = new Size(110, 44);
            NoButton.TabIndex = 14;
            NoButton.Text = "No";
            NoButton.UseVisualStyleBackColor = false;
            NoButton.Click += NoButton_Click;
            NoButton.MouseDown += Button_MouseDown;
            NoButton.MouseLeave += Button_MouseLeave;
            NoButton.MouseHover += Button_MouseHover;
            // 
            // MsgBoxForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(646, 272);
            Controls.Add(NoButton);
            Controls.Add(YesButton);
            Controls.Add(tbMessage);
            Controls.Add(TextLabel);
            Controls.Add(TitleLabel);
            Controls.Add(OkButton);
            Controls.Add(CloseButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MsgBoxForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Message";
            TransparencyKey = Color.Magenta;
            FormClosing += MsgBoxForm_FormClosing;
            Load += MsgBoxForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CloseButton;
        private ImageList imageListCloseBtn;
        private Button OkButton;
        private ImageList imageListOKBtn;
        private Label TitleLabel;
        private Label TextLabel;
        private TextBox tbMessage;
        private Button YesButton;
        private ImageList imageListBtn;
        private Button NoButton;
    }
}