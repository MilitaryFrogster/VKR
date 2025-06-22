using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VKR
{
    public partial class FormExitConfirm : Form
    {
        public FormExitConfirm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void guna2ButtonYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void guna2ButtonNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}
