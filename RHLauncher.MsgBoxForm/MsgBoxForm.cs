using RHLauncher.RHLauncher.Helper;

namespace RHLauncher
{
    public partial class MsgBoxForm : Form
    {
        public new DialogResult DialogResult { get; private set; }

        public MsgBoxForm()
        {
            InitializeComponent();


            YesButton.Click += YesButton_Click;
            NoButton.Click += NoButton_Click;
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        public static void Show(string message, string title)
        {
            MsgBoxForm msgBox = new();
            msgBox.textBox1.Visible = false;
            msgBox.YesButton.Visible = false;
            msgBox.NoButton.Visible = false;
            msgBox.TextLabel.Text = message;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();
        }

        public static void ShowST(string message, string title, string stacktrace)
        {
            MsgBoxForm msgBox = new();
            msgBox.textBox1.Visible = true;
            msgBox.YesButton.Visible = false;
            msgBox.NoButton.Visible = false;
            msgBox.TextLabel.Text = message;
            msgBox.textBox1.Text = stacktrace;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();
        }

        public static DialogResult ShowYN(string message, string title)
        {
            MsgBoxForm msgBox = new();
            msgBox.textBox1.Visible = false;
            msgBox.OkButton.Visible = false;
            msgBox.YesButton.Visible = true;
            msgBox.NoButton.Visible = true;
            msgBox.TextLabel.Text = message;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();

            return msgBox.DialogResult;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            CloseButton.ImageIndex = 1;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ImageIndex = 0;
        }
        private void CloseButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            CloseButton.ImageIndex = 2;
        }

        private void OkButton_MouseHover(object sender, EventArgs e)
        {
            OkButton.ImageIndex = 1;
        }

        private void OkButton_MouseLeave(object sender, EventArgs e)
        {
            OkButton.ImageIndex = 0;
        }

        private void OkButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            OkButton.ImageIndex = 2;
        }

        private void YesButton_MouseHover(object sender, EventArgs e)
        {
            YesButton.ImageIndex = 1;
        }

        private void YesButton_MouseLeave(object sender, EventArgs e)
        {
            YesButton.ImageIndex = 0;
        }
        private void YesButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            YesButton.ImageIndex = 2;
        }

        private void NoButton_MouseHover(object sender, EventArgs e)
        {
            NoButton.ImageIndex = 1;
        }

        private void NoButton_MouseLeave(object sender, EventArgs e)
        {
            NoButton.ImageIndex = 0;
        }
        private void NoButton_OnMouseDown(object sender, MouseEventArgs e)
        {
            NoButton.ImageIndex = 2;
        }

        private void MsgBoxForm_Load(object sender, EventArgs e)
        {
            TitleLabel.Left = (ClientSize.Width - TitleLabel.Width) / 2;
            OkButton.Left = (ClientSize.Width - OkButton.Width) / 2;
        }

        private void MsgBoxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
