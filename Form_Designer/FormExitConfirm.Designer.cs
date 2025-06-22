using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace VKR
{
    partial class FormExitConfirm
    {
        private System.ComponentModel.IContainer components = null;
        private Guna2HtmlLabel guna2HtmlLabel1;
        private Guna2Button guna2ButtonYes;
        private Guna2Button guna2ButtonNo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ButtonYes = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ButtonNo = new Guna.UI2.WinForms.Guna2Button();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(20, 20);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(360, 54);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Вы действительно хотите выйти из программы?";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2ButtonYes
            // 
            this.guna2ButtonYes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonYes.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonYes.Location = new System.Drawing.Point(60, 80);
            this.guna2ButtonYes.Name = "guna2ButtonYes";
            this.guna2ButtonYes.Size = new System.Drawing.Size(100, 40);
            this.guna2ButtonYes.TabIndex = 1;
            this.guna2ButtonYes.Text = "Да";
            this.guna2ButtonYes.Click += new System.EventHandler(this.guna2ButtonYes_Click);
            // 
            // guna2ButtonNo
            // 
            this.guna2ButtonNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonNo.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonNo.Location = new System.Drawing.Point(220, 80);
            this.guna2ButtonNo.Name = "guna2ButtonNo";
            this.guna2ButtonNo.Size = new System.Drawing.Size(100, 40);
            this.guna2ButtonNo.TabIndex = 2;
            this.guna2ButtonNo.Text = "Нет";
            this.guna2ButtonNo.Click += new System.EventHandler(this.guna2ButtonNo_Click);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // FormExitConfirm
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(400, 140);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.guna2ButtonYes);
            this.Controls.Add(this.guna2ButtonNo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExitConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подтверждение выхода";
            this.ResumeLayout(false);

        }

        private Guna2BorderlessForm guna2BorderlessForm1;
    }
}
