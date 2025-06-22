using System.Windows.Forms;

namespace VKR
{
    partial class Form4
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();

            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();

            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cloud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Capacity = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // tableLayoutPanelMain
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.guna2GroupBox1, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1053, 787);
            this.tableLayoutPanelMain.TabIndex = 0;

            // tableLayoutPanelTop
            this.tableLayoutPanelTop.ColumnCount = 2;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelTop.Controls.Add(this.flowLayoutPanelTop, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.guna2CircleButton1, 1, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1047, 74);
            this.tableLayoutPanelTop.TabIndex = 0;

            // flowLayoutPanelTop
            this.flowLayoutPanelTop.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelTop.Controls.Add(this.guna2Button1);
            this.flowLayoutPanelTop.Controls.Add(this.guna2Button2);
            this.flowLayoutPanelTop.Controls.Add(this.label1);
            this.flowLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTop.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.flowLayoutPanelTop.WrapContents = false;
            this.flowLayoutPanelTop.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            this.flowLayoutPanelTop.Size = new System.Drawing.Size(994, 68);
            this.flowLayoutPanelTop.TabIndex = 0;

            // guna2Button1
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Size = new System.Drawing.Size(114, 44);
            this.guna2Button1.Text = "Добавить";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);

            // guna2Button2
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Size = new System.Drawing.Size(114, 44);
            this.guna2Button2.Text = "Удалить";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Text = "Подключенные хранилища";
            this.label1.Margin = new System.Windows.Forms.Padding(20, 15, 0, 0);

            // guna2CircleButton1
            this.guna2CircleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CircleButton1.Image")));
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(35, 35);
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);

            // guna2GroupBox1
            this.guna2GroupBox1.Controls.Add(this.guna2DataGridView1);
            this.guna2GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(125, 137, 149);
            this.guna2GroupBox1.Text = "Список подключенных";

            // guna2DataGridView1
            this.guna2DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2DataGridView1.ReadOnly = true;
            this.guna2DataGridView1.RowHeadersVisible = false;

            this.guna2DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                this.Email, this.Cloud, this.Capacity
            });

            // Email
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;

            // Cloud
            this.Cloud.HeaderText = "Облако";
            this.Cloud.MinimumWidth = 6;
            this.Cloud.Name = "Cloud";
            this.Cloud.ReadOnly = true;

            // Capacity
            this.Capacity.HeaderText = "Занято/Всего";
            this.Capacity.MinimumWidth = 6;
            this.Capacity.Name = "Capacity";
            this.Capacity.ReadOnly = true;

            // Form4
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(52, 52, 52);
            this.ClientSize = new System.Drawing.Size(1053, 787);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTop;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cloud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Capacity;
    }
}
