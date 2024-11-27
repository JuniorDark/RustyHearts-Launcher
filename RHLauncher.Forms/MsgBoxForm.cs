using RHLauncher.RHLauncher.i8n;
using System.ComponentModel;

namespace RHLauncher
{
    public partial class MsgBoxForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DialogResult DialogResult { get; private set; }

        public MsgBoxForm()
        {
            InitializeComponent();

            Text = LocalizedStrings.MsgBoxFormTitle;
            YesButton.Text = LocalizedStrings.Yes;
            NoButton.Text = LocalizedStrings.No;
        }

        #region Form Events
        private void MsgBoxForm_Load(object sender, EventArgs e)
        {
            TitleLabel.Left = (ClientSize.Width - TitleLabel.Width) / 2;
            OkButton.Left = (ClientSize.Width - OkButton.Width) / 2;
        }

        private void MsgBoxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }
        #endregion

        #region Button Click Events
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Methods
        public static void Show(string message, string title)
        {
            MsgBoxForm msgBox = new();
            msgBox.tbMessage.Visible = false;
            msgBox.YesButton.Visible = false;
            msgBox.NoButton.Visible = false;
            msgBox.TextLabel.Text = message;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();
        }

        public static void ShowST(string message, string title, string stacktrace)
        {
            MsgBoxForm msgBox = new();
            msgBox.tbMessage.Visible = true;
            msgBox.YesButton.Visible = false;
            msgBox.NoButton.Visible = false;
            msgBox.TextLabel.Text = message;
            msgBox.tbMessage.Text = stacktrace;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();
        }

        public static DialogResult ShowYN(string message, string title)
        {
            MsgBoxForm msgBox = new();
            msgBox.tbMessage.Visible = false;
            msgBox.OkButton.Visible = false;
            msgBox.YesButton.Visible = true;
            msgBox.NoButton.Visible = true;
            msgBox.TextLabel.Text = message;
            msgBox.TitleLabel.Text = title;
            msgBox.ShowDialog();

            return msgBox.DialogResult;
        }
        #endregion

        #region Button Events
        private void Button_MouseHover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 1;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 0;
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 2;
            }
        }
        #endregion
    }
}
