using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace VKR
{
    partial class Form10
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2HtmlLabel labelTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelFrom;
        private Guna.UI2.WinForms.Guna2Button buttonFrom;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelTo;
        private Guna.UI2.WinForms.Guna2Button buttonTo;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelSchedule;
        private Guna.UI2.WinForms.Guna2ComboBox comboSchedule;
        private Guna.UI2.WinForms.Guna2HtmlLabel labelEncrypt;
        private Guna2CheckBox checkBoxEncrypt;

        private Guna.UI2.WinForms.Guna2Button buttonBack;
        private Guna.UI2.WinForms.Guna2Button buttonCreate;
        private Guna.UI2.WinForms.Guna2CircleButton buttonClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.labelFrom = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.buttonFrom = new Guna.UI2.WinForms.Guna2Button();
            this.labelTo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.buttonTo = new Guna.UI2.WinForms.Guna2Button();
            this.labelSchedule = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.comboSchedule = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelEncrypt = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.checkBoxEncrypt = new Guna.UI2.WinForms.Guna2CheckBox();
            this.buttonBack = new Guna.UI2.WinForms.Guna2Button();
            this.buttonCreate = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.buttonClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = false;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(165, 75);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(400, 40);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Настройки задачи";
            this.labelTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFrom
            // 
            this.labelFrom.BackColor = System.Drawing.Color.Transparent;
            this.labelFrom.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelFrom.Location = new System.Drawing.Point(169, 235);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(51, 18);
            this.labelFrom.TabIndex = 1;
            this.labelFrom.Text = "Откуда";
            // 
            // buttonFrom
            // 
            this.buttonFrom.FillColor = System.Drawing.Color.Tan;
            this.buttonFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonFrom.ForeColor = System.Drawing.Color.White;
            this.buttonFrom.Location = new System.Drawing.Point(319, 225);
            this.buttonFrom.Name = "buttonFrom";
            this.buttonFrom.Size = new System.Drawing.Size(158, 40);
            this.buttonFrom.TabIndex = 2;
            this.buttonFrom.Click += new System.EventHandler(this.buttonFrom_Click);
            // 
            // labelTo
            // 
            this.labelTo.BackColor = System.Drawing.Color.Transparent;
            this.labelTo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelTo.Location = new System.Drawing.Point(169, 295);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(35, 18);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "Куда";
            // 
            // buttonTo
            // 
            this.buttonTo.FillColor = System.Drawing.Color.Tan;
            this.buttonTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonTo.ForeColor = System.Drawing.Color.White;
            this.buttonTo.Location = new System.Drawing.Point(319, 285);
            this.buttonTo.Name = "buttonTo";
            this.buttonTo.Size = new System.Drawing.Size(158, 40);
            this.buttonTo.TabIndex = 4;
            this.buttonTo.Click += new System.EventHandler(this.buttonTo_Click);
            // 
            // labelSchedule
            // 
            this.labelSchedule.BackColor = System.Drawing.Color.Transparent;
            this.labelSchedule.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelSchedule.Location = new System.Drawing.Point(169, 355);
            this.labelSchedule.Name = "labelSchedule";
            this.labelSchedule.Size = new System.Drawing.Size(82, 18);
            this.labelSchedule.TabIndex = 5;
            this.labelSchedule.Text = "Расписание";
            // 
            // comboSchedule
            // 
            this.comboSchedule.BackColor = System.Drawing.Color.Transparent;
            this.comboSchedule.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchedule.FocusedColor = System.Drawing.Color.Empty;
            this.comboSchedule.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comboSchedule.ItemHeight = 30;
            this.comboSchedule.Location = new System.Drawing.Point(319, 350);
            this.comboSchedule.Name = "comboSchedule";
            this.comboSchedule.Size = new System.Drawing.Size(300, 36);
            this.comboSchedule.TabIndex = 6;
            // 
            // labelEncrypt
            // 
            this.labelEncrypt.BackColor = System.Drawing.Color.Transparent;
            this.labelEncrypt.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelEncrypt.Location = new System.Drawing.Point(169, 415);
            this.labelEncrypt.Name = "labelEncrypt";
            this.labelEncrypt.Size = new System.Drawing.Size(89, 18);
            this.labelEncrypt.TabIndex = 7;
            this.labelEncrypt.Text = "Шифрование";
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxEncrypt.Checked = true;
            this.checkBoxEncrypt.CheckedState.BorderRadius = 0;
            this.checkBoxEncrypt.CheckedState.BorderThickness = 0;
            this.checkBoxEncrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(319, 443);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(20, 20);
            this.checkBoxEncrypt.TabIndex = 8;
            this.checkBoxEncrypt.UncheckedState.BorderRadius = 0;
            this.checkBoxEncrypt.UncheckedState.BorderThickness = 0;
            // 
            // buttonBack
            // 
            this.buttonBack.FillColor = System.Drawing.Color.SlateBlue;
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(165, 543);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 45);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Назад";
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.FillColor = System.Drawing.Color.SlateBlue;
            this.buttonCreate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonCreate.ForeColor = System.Drawing.Color.White;
            this.buttonCreate.Location = new System.Drawing.Point(505, 543);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(150, 45);
            this.buttonCreate.TabIndex = 10;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(155, 173);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(120, 18);
            this.guna2HtmlLabel1.TabIndex = 12;
            this.guna2HtmlLabel1.Text = "Название задачи";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(319, 164);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(399, 33);
            this.guna2TextBox1.TabIndex = 13;
            // 
            // buttonClose
            // 
            this.buttonClose.FillColor = System.Drawing.Color.Transparent;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Image = global::VKR.Properties.Resources.close_icon;
            this.buttonClose.ImageSize = new System.Drawing.Size(30, 30);
            this.buttonClose.Location = new System.Drawing.Point(744, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form10
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.guna2TextBox1);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.buttonFrom);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.buttonTo);
            this.Controls.Add(this.labelSchedule);
            this.Controls.Add(this.comboSchedule);
            this.Controls.Add(this.labelEncrypt);
            this.Controls.Add(this.checkBoxEncrypt);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form10";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna2HtmlLabel guna2HtmlLabel1;
        private Guna2TextBox guna2TextBox1;
    }
}
