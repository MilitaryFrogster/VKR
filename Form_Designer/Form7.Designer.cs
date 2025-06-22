// Designer для Form7: выравнены кнопки над TreeView по правому краю
using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing;

namespace VKR
{
    partial class Form7
    {
        private IContainer components = null;
        private Guna2ComboBox guna2ComboBox1;
        private Guna2ComboBox guna2ComboBox2;
        private PictureBox pictureBoxDown1;
        private PictureBox pictureBoxDown2;
        private TableLayoutPanel layoutRoot;
        private Guna2CircleButton guna2CircleButtonClose;
        private TreeView treeView1;
        private TreeView treeView2;
        private Guna2Button guna2Button12;
        private Guna2Button guna2Button11;
        private Guna2Button guna2Button10;
        private Guna2Button guna2Button8;
        private Guna2Button guna2Button6;
        private Guna2Button guna2Button7;
        private Guna2Button guna2Button2;
        private Guna2Button guna2Button9;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel centerPanel;
        private FlowLayoutPanel leftButtonPanel;
        private FlowLayoutPanel rightButtonPanel;
        private TableLayoutPanel arrowsPanel;
        private ListBox listBox1;
        private ListBox listBox2;
        private CheckBox checkBoxEncryptUpload;
        private Label labelEncrypt;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2ComboBox2 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.leftButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Button10 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button11 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button12 = new Guna.UI2.WinForms.Guna2Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.labelEncrypt = new System.Windows.Forms.Label();
            this.checkBoxEncryptUpload = new System.Windows.Forms.CheckBox();
            this.guna2Button9 = new Guna.UI2.WinForms.Guna2Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.rightButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button6 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button8 = new Guna.UI2.WinForms.Guna2Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.arrowsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxDown1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxDown2 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.guna2CircleButtonClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.layoutRoot.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.leftButtonPanel.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.rightButtonPanel.SuspendLayout();
            this.arrowsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.layoutRoot.SetColumnSpan(this.guna2ComboBox1, 3);
            this.guna2ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Location = new System.Drawing.Point(3, 3);
            this.guna2ComboBox1.Margin = new System.Windows.Forms.Padding(3, 3, 45, 3);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(1005, 36);
            this.guna2ComboBox1.TabIndex = 0;
            // 
            // guna2ComboBox2
            // 
            this.guna2ComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.layoutRoot.SetColumnSpan(this.guna2ComboBox2, 3);
            this.guna2ComboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox2.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.guna2ComboBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox2.ItemHeight = 30;
            this.guna2ComboBox2.Location = new System.Drawing.Point(3, 91);
            this.guna2ComboBox2.Margin = new System.Windows.Forms.Padding(3, 3, 45, 3);
            this.guna2ComboBox2.Name = "guna2ComboBox2";
            this.guna2ComboBox2.Size = new System.Drawing.Size(1005, 36);
            this.guna2ComboBox2.TabIndex = 1;
            // 
            // layoutRoot
            // 
            this.layoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.layoutRoot.ColumnCount = 3;
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutRoot.Controls.Add(this.guna2ComboBox1, 0, 0);
            this.layoutRoot.Controls.Add(this.guna2ComboBox2, 0, 2);
            this.layoutRoot.Controls.Add(this.leftPanel, 0, 3);
            this.layoutRoot.Controls.Add(this.centerPanel, 1, 3);
            this.layoutRoot.Controls.Add(this.rightPanel, 2, 3);
            this.layoutRoot.Controls.Add(this.arrowsPanel, 0, 1);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 639F));
            this.layoutRoot.Size = new System.Drawing.Size(1053, 787);
            this.layoutRoot.TabIndex = 0;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.treeView1);
            this.leftPanel.Controls.Add(this.leftButtonPanel);
            this.leftPanel.Controls.Add(this.listBox1);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 136);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(457, 648);
            this.leftPanel.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 36);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(457, 512);
            this.treeView1.TabIndex = 0;
            // 
            // leftButtonPanel
            // 
            this.leftButtonPanel.Controls.Add(this.guna2Button10);
            this.leftButtonPanel.Controls.Add(this.guna2Button11);
            this.leftButtonPanel.Controls.Add(this.guna2Button12);
            this.leftButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.leftButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.leftButtonPanel.Name = "leftButtonPanel";
            this.leftButtonPanel.Padding = new System.Windows.Forms.Padding(0, 4, 4, 0);
            this.leftButtonPanel.Size = new System.Drawing.Size(457, 36);
            this.leftButtonPanel.TabIndex = 1;
            // 
            // guna2Button10
            // 
            this.guna2Button10.FillColor = System.Drawing.Color.Tan;
            this.guna2Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button10.ForeColor = System.Drawing.Color.White;
            this.guna2Button10.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button10.Image")));
            this.guna2Button10.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button10.Location = new System.Drawing.Point(414, 7);
            this.guna2Button10.Name = "guna2Button10";
            this.guna2Button10.Size = new System.Drawing.Size(36, 28);
            this.guna2Button10.TabIndex = 0;
            this.guna2Button10.Click += new System.EventHandler(this.Guna2Button10_Click);
            // 
            // guna2Button11
            // 
            this.guna2Button11.FillColor = System.Drawing.Color.Tan;
            this.guna2Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button11.ForeColor = System.Drawing.Color.White;
            this.guna2Button11.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button11.Image")));
            this.guna2Button11.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button11.Location = new System.Drawing.Point(372, 7);
            this.guna2Button11.Name = "guna2Button11";
            this.guna2Button11.Size = new System.Drawing.Size(36, 28);
            this.guna2Button11.TabIndex = 1;
            this.guna2Button11.Click += new System.EventHandler(this.Guna2Button11_Click);
            // 
            // guna2Button12
            // 
            this.guna2Button12.FillColor = System.Drawing.Color.Tan;
            this.guna2Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button12.ForeColor = System.Drawing.Color.White;
            this.guna2Button12.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button12.Image")));
            this.guna2Button12.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button12.Location = new System.Drawing.Point(330, 7);
            this.guna2Button12.Name = "guna2Button12";
            this.guna2Button12.Size = new System.Drawing.Size(36, 32);
            this.guna2Button12.TabIndex = 2;
            this.guna2Button12.Click += new System.EventHandler(this.Guna2Button12_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 548);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(457, 100);
            this.listBox1.TabIndex = 2;
            // 
            // centerPanel
            // 
            this.centerPanel.Controls.Add(this.guna2Button2);
            this.centerPanel.Controls.Add(this.labelEncrypt);
            this.centerPanel.Controls.Add(this.checkBoxEncryptUpload);
            this.centerPanel.Controls.Add(this.guna2Button9);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(466, 136);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(120, 648);
            this.centerPanel.TabIndex = 3;
            // 
            // guna2Button2
            // 
            this.guna2Button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Button2.FillColor = System.Drawing.Color.Empty;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button2.Image")));
            this.guna2Button2.ImageSize = new System.Drawing.Size(83, 83);
            this.guna2Button2.Location = new System.Drawing.Point(18, 144);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(85, 82);
            this.guna2Button2.TabIndex = 0;
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click_1);
            // 
            // labelEncrypt
            // 
            this.labelEncrypt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEncrypt.AutoSize = true;
            this.labelEncrypt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelEncrypt.ForeColor = System.Drawing.Color.White;
            this.labelEncrypt.Location = new System.Drawing.Point(14, 482);
            this.labelEncrypt.Name = "labelEncrypt";
            this.labelEncrypt.Size = new System.Drawing.Size(102, 20);
            this.labelEncrypt.TabIndex = 2;
            this.labelEncrypt.Text = "Шифрование";
            // 
            // checkBoxEncryptUpload
            // 
            this.checkBoxEncryptUpload.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxEncryptUpload.Checked = true;
            this.checkBoxEncryptUpload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEncryptUpload.Location = new System.Drawing.Point(46, 515);
            this.checkBoxEncryptUpload.Name = "checkBoxEncryptUpload";
            this.checkBoxEncryptUpload.Size = new System.Drawing.Size(24, 24);
            this.checkBoxEncryptUpload.TabIndex = 3;
            // 
            // guna2Button9
            // 
            this.guna2Button9.FillColor = System.Drawing.Color.Empty;
            this.guna2Button9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button9.ForeColor = System.Drawing.Color.White;
            this.guna2Button9.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button9.Image")));
            this.guna2Button9.ImageSize = new System.Drawing.Size(83, 83);
            this.guna2Button9.Location = new System.Drawing.Point(18, 295);
            this.guna2Button9.Name = "guna2Button9";
            this.guna2Button9.Size = new System.Drawing.Size(85, 87);
            this.guna2Button9.TabIndex = 4;
            this.guna2Button9.Click += new System.EventHandler(this.Guna2Button9_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.guna2HtmlLabel1);
            this.rightPanel.Controls.Add(this.guna2ProgressBar1);
            this.rightPanel.Controls.Add(this.treeView2);
            this.rightPanel.Controls.Add(this.rightButtonPanel);
            this.rightPanel.Controls.Add(this.listBox2);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(592, 136);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.rightPanel.Size = new System.Drawing.Size(458, 648);
            this.rightPanel.TabIndex = 4;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(0, 36);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(448, 512);
            this.treeView2.TabIndex = 0;
            // 
            // rightButtonPanel
            // 
            this.rightButtonPanel.Controls.Add(this.guna2Button7);
            this.rightButtonPanel.Controls.Add(this.guna2Button6);
            this.rightButtonPanel.Controls.Add(this.guna2Button8);
            this.rightButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.rightButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.rightButtonPanel.Name = "rightButtonPanel";
            this.rightButtonPanel.Padding = new System.Windows.Forms.Padding(0, 4, 4, 0);
            this.rightButtonPanel.Size = new System.Drawing.Size(448, 36);
            this.rightButtonPanel.TabIndex = 1;
            // 
            // guna2Button7
            // 
            this.guna2Button7.FillColor = System.Drawing.Color.Tan;
            this.guna2Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button7.ForeColor = System.Drawing.Color.White;
            this.guna2Button7.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button7.Image")));
            this.guna2Button7.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button7.Location = new System.Drawing.Point(405, 7);
            this.guna2Button7.Name = "guna2Button7";
            this.guna2Button7.Size = new System.Drawing.Size(36, 28);
            this.guna2Button7.TabIndex = 0;
            this.guna2Button7.Click += new System.EventHandler(this.Guna2Button7_Click);
            // 
            // guna2Button6
            // 
            this.guna2Button6.FillColor = System.Drawing.Color.Tan;
            this.guna2Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button6.ForeColor = System.Drawing.Color.White;
            this.guna2Button6.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button6.Image")));
            this.guna2Button6.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button6.Location = new System.Drawing.Point(363, 7);
            this.guna2Button6.Name = "guna2Button6";
            this.guna2Button6.Size = new System.Drawing.Size(36, 28);
            this.guna2Button6.TabIndex = 1;
            this.guna2Button6.Click += new System.EventHandler(this.Guna2Button6_Click);
            // 
            // guna2Button8
            // 
            this.guna2Button8.FillColor = System.Drawing.Color.Tan;
            this.guna2Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button8.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button8.Image")));
            this.guna2Button8.ImageSize = new System.Drawing.Size(25, 25);
            this.guna2Button8.Location = new System.Drawing.Point(321, 7);
            this.guna2Button8.Name = "guna2Button8";
            this.guna2Button8.Size = new System.Drawing.Size(36, 28);
            this.guna2Button8.TabIndex = 2;
            this.guna2Button8.Click += new System.EventHandler(this.Guna2Button8_Click);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(0, 548);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(448, 100);
            this.listBox2.TabIndex = 2;
            // 
            // arrowsPanel
            // 
            this.arrowsPanel.ColumnCount = 5;
            this.layoutRoot.SetColumnSpan(this.arrowsPanel, 3);
            this.arrowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.arrowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.arrowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.arrowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.arrowsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.arrowsPanel.Controls.Add(this.pictureBoxDown1, 1, 0);
            this.arrowsPanel.Controls.Add(this.pictureBoxDown2, 3, 0);
            this.arrowsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arrowsPanel.Location = new System.Drawing.Point(3, 45);
            this.arrowsPanel.Name = "arrowsPanel";
            this.arrowsPanel.RowCount = 1;
            this.arrowsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.arrowsPanel.Size = new System.Drawing.Size(1047, 40);
            this.arrowsPanel.TabIndex = 5;
            // 
            // pictureBoxDown1
            // 
            this.pictureBoxDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDown1.Image = global::VKR.Properties.Resources.arrow_down;
            this.pictureBoxDown1.Location = new System.Drawing.Point(340, 3);
            this.pictureBoxDown1.Name = "pictureBoxDown1";
            this.pictureBoxDown1.Size = new System.Drawing.Size(46, 34);
            this.pictureBoxDown1.TabIndex = 0;
            this.pictureBoxDown1.TabStop = false;
            // 
            // pictureBoxDown2
            // 
            this.pictureBoxDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDown2.Image = global::VKR.Properties.Resources.arrow_down;
            this.pictureBoxDown2.Location = new System.Drawing.Point(662, 3);
            this.pictureBoxDown2.Name = "pictureBoxDown2";
            this.pictureBoxDown2.Size = new System.Drawing.Size(44, 34);
            this.pictureBoxDown2.TabIndex = 1;
            this.pictureBoxDown2.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // guna2CircleButtonClose
            // 
            this.guna2CircleButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CircleButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButtonClose.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButtonClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButtonClose.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButtonClose.Image = global::VKR.Properties.Resources.close_icon;
            this.guna2CircleButtonClose.ImageSize = new System.Drawing.Size(35, 35);
            this.guna2CircleButtonClose.Location = new System.Drawing.Point(1013, 5);
            this.guna2CircleButtonClose.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.guna2CircleButtonClose.Name = "guna2CircleButtonClose";
            this.guna2CircleButtonClose.Size = new System.Drawing.Size(37, 35);
            this.guna2CircleButtonClose.TabIndex = 6;
            this.guna2CircleButtonClose.Click += new System.EventHandler(this.guna2CircleButtonClose_Click);
            // 
            // guna2ProgressBar1
            // 
            this.guna2ProgressBar1.Location = new System.Drawing.Point(116, 575);
            this.guna2ProgressBar1.Name = "guna2ProgressBar1";
            this.guna2ProgressBar1.Size = new System.Drawing.Size(300, 30);
            this.guna2ProgressBar1.TabIndex = 5;
            this.guna2ProgressBar1.Text = "guna2ProgressBar1";
            this.guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.YellowGreen;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(126, 611);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(108, 18);
            this.guna2HtmlLabel1.TabIndex = 6;
            this.guna2HtmlLabel1.Text = "guna2HtmlLabel1";
            // 
            // Form7
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1053, 787);
            this.Controls.Add(this.guna2CircleButtonClose);
            this.Controls.Add(this.layoutRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form7";
            this.layoutRoot.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftButtonPanel.ResumeLayout(false);
            this.centerPanel.ResumeLayout(false);
            this.centerPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.rightButtonPanel.ResumeLayout(false);
            this.arrowsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown2)).EndInit();
            this.ResumeLayout(false);

        }

        private ImageList imageList1;
        private Guna2HtmlLabel guna2HtmlLabel1;
        private Guna2ProgressBar guna2ProgressBar1;
    }
}
