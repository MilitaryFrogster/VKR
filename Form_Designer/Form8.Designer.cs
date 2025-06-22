// Designer для Form7: выравнены кнопки над TreeView по правому краю
using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing;

namespace VKR
{
    partial class Form8
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
        private TableLayoutPanel rightPanel;
        private Panel centerPanel;
        private FlowLayoutPanel leftButtonPanel;
        private FlowLayoutPanel rightButtonPanel;
        private TableLayoutPanel arrowsPanel;
        private ListBox listBox1;
        private ListBox listBox2;
        private CheckBox checkBoxEncryptUpload;
        private Label labelEncrypt;
        private Guna.UI2.WinForms.Guna2VProgressBar guna2VProgressBar1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna2Button guna2ButtonToTasks;
        //private Panel bottomLeftPanel;
        private Panel bottomRightPanel;
        private Panel labelPanel;
        private Panel progressPanel;
        private TableLayoutPanel leftPanel;
        private FlowLayoutPanel progressBarAndButtonPanel;
        private TableLayoutPanel bottomRightInnerPanel;





        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2ComboBox2 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.layoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.labelEncrypt = new System.Windows.Forms.Label();
            this.checkBoxEncryptUpload = new System.Windows.Forms.CheckBox();
            this.guna2VProgressBar1 = new Guna.UI2.WinForms.Guna2VProgressBar();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.rightPanel = new System.Windows.Forms.TableLayoutPanel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.bottomRightInnerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.guna2ButtonToTasks = new Guna.UI2.WinForms.Guna2Button();
            this.arrowsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxDown1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxDown2 = new System.Windows.Forms.PictureBox();
            this.leftPanel = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.leftButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBarAndButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPanel = new System.Windows.Forms.Panel();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.bottomRightPanel = new System.Windows.Forms.Panel();
            this.rightButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Button10 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button11 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button12 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button6 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button8 = new Guna.UI2.WinForms.Guna2Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.guna2CircleButtonClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.layoutRoot.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.bottomRightInnerPanel.SuspendLayout();
            this.arrowsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown2)).BeginInit();
            this.leftPanel.SuspendLayout();
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
            this.guna2ComboBox2.Location = new System.Drawing.Point(3, 100);
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
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.layoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutRoot.Controls.Add(this.guna2ComboBox1, 0, 0);
            this.layoutRoot.Controls.Add(this.guna2ComboBox2, 0, 2);
            this.layoutRoot.Controls.Add(this.centerPanel, 1, 3);
            this.layoutRoot.Controls.Add(this.rightPanel, 2, 3);
            this.layoutRoot.Controls.Add(this.arrowsPanel, 0, 1);
            this.layoutRoot.Controls.Add(this.leftPanel, 0, 3);
            this.layoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutRoot.Name = "layoutRoot";
            this.layoutRoot.RowCount = 4;
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 639F));
            this.layoutRoot.Size = new System.Drawing.Size(1053, 787);
            this.layoutRoot.TabIndex = 0;
            // 
            // centerPanel
            // 
            this.centerPanel.Controls.Add(this.guna2Button2);
            this.centerPanel.Controls.Add(this.labelEncrypt);
            this.centerPanel.Controls.Add(this.checkBoxEncryptUpload);
            this.centerPanel.Controls.Add(this.guna2VProgressBar1);
            this.centerPanel.Controls.Add(this.guna2HtmlLabel2);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(450, 165);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(153, 619);
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
            this.guna2Button2.Location = new System.Drawing.Point(31, 157);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(98, 90);
            this.guna2Button2.TabIndex = 0;
            // 
            // labelEncrypt
            // 
            this.labelEncrypt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEncrypt.AutoSize = true;
            this.labelEncrypt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelEncrypt.ForeColor = System.Drawing.Color.White;
            this.labelEncrypt.Location = new System.Drawing.Point(30, 368);
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
            this.checkBoxEncryptUpload.Location = new System.Drawing.Point(68, 391);
            this.checkBoxEncryptUpload.Name = "checkBoxEncryptUpload";
            this.checkBoxEncryptUpload.Size = new System.Drawing.Size(24, 24);
            this.checkBoxEncryptUpload.TabIndex = 3;
            // 
            // guna2VProgressBar1
            // 
            this.guna2VProgressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2VProgressBar1.Location = new System.Drawing.Point(-3, 445);
            this.guna2VProgressBar1.Name = "guna2VProgressBar1";
            this.guna2VProgressBar1.Size = new System.Drawing.Size(157, 20);
            this.guna2VProgressBar1.TabIndex = 7;
            this.guna2VProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2VProgressBar1.Visible = false;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(17, 480);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(22, 18);
            this.guna2HtmlLabel2.TabIndex = 8;
            this.guna2HtmlLabel2.Text = "0%";
            this.guna2HtmlLabel2.Visible = false;
            // 
            // rightPanel
            // 
            this.rightPanel.ColumnCount = 1;
            this.rightPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightPanel.Controls.Add(this.treeView2, 0, 0);
            this.rightPanel.Controls.Add(this.listBox2, 0, 1);
            this.rightPanel.Controls.Add(this.bottomRightInnerPanel, 0, 2);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(609, 165);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.rightPanel.RowCount = 3;
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.rightPanel.Size = new System.Drawing.Size(441, 619);
            this.rightPanel.TabIndex = 4;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(3, 3);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(425, 420);
            this.treeView2.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(3, 429);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(425, 103);
            this.listBox2.TabIndex = 2;
            // 
            // bottomRightInnerPanel
            // 
            this.bottomRightInnerPanel.ColumnCount = 3;
            this.bottomRightInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.bottomRightInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 288F));
            this.bottomRightInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.bottomRightInnerPanel.Controls.Add(this.guna2ButtonToTasks, 1, 1);
            this.bottomRightInnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomRightInnerPanel.Location = new System.Drawing.Point(3, 538);
            this.bottomRightInnerPanel.Name = "bottomRightInnerPanel";
            this.bottomRightInnerPanel.RowCount = 3;
            this.bottomRightInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.bottomRightInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.bottomRightInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.bottomRightInnerPanel.Size = new System.Drawing.Size(425, 78);
            this.bottomRightInnerPanel.TabIndex = 3;
            // 
            // guna2ButtonToTasks
            // 
            this.guna2ButtonToTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ButtonToTasks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2ButtonToTasks.ForeColor = System.Drawing.Color.White;
            this.guna2ButtonToTasks.Location = new System.Drawing.Point(216, 27);
            this.guna2ButtonToTasks.Name = "guna2ButtonToTasks";
            this.guna2ButtonToTasks.Size = new System.Drawing.Size(137, 35);
            this.guna2ButtonToTasks.TabIndex = 2;
            this.guna2ButtonToTasks.Text = "К задачам";
            this.guna2ButtonToTasks.Click += new System.EventHandler(this.guna2ButtonToTasks_Click);
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
            this.arrowsPanel.Size = new System.Drawing.Size(1047, 49);
            this.arrowsPanel.TabIndex = 5;
            // 
            // pictureBoxDown1
            // 
            this.pictureBoxDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDown1.Image = global::VKR.Properties.Resources.arrow_down;
            this.pictureBoxDown1.Location = new System.Drawing.Point(340, 3);
            this.pictureBoxDown1.Name = "pictureBoxDown1";
            this.pictureBoxDown1.Size = new System.Drawing.Size(46, 43);
            this.pictureBoxDown1.TabIndex = 0;
            this.pictureBoxDown1.TabStop = false;
            // 
            // pictureBoxDown2
            // 
            this.pictureBoxDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDown2.Image = global::VKR.Properties.Resources.arrow_down;
            this.pictureBoxDown2.Location = new System.Drawing.Point(662, 3);
            this.pictureBoxDown2.Name = "pictureBoxDown2";
            this.pictureBoxDown2.Size = new System.Drawing.Size(44, 43);
            this.pictureBoxDown2.TabIndex = 1;
            this.pictureBoxDown2.TabStop = false;
            // 
            // leftPanel
            // 
            this.leftPanel.ColumnCount = 1;
            this.leftPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftPanel.Controls.Add(this.listBox1, 0, 1);
            this.leftPanel.Controls.Add(this.treeView1, 0, 0);
            this.leftPanel.Controls.Add(this.leftButtonPanel, 0, 2);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(3, 165);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.RowCount = 2;
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.leftPanel.Size = new System.Drawing.Size(441, 619);
            this.leftPanel.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(3, 432);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(435, 100);
            this.listBox1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(435, 423);
            this.treeView1.TabIndex = 0;
            // 
            // leftButtonPanel
            // 
            this.leftButtonPanel.Location = new System.Drawing.Point(3, 539);
            this.leftButtonPanel.Name = "leftButtonPanel";
            this.leftButtonPanel.Size = new System.Drawing.Size(203, 64);
            this.leftButtonPanel.TabIndex = 1;
            // 
            // progressBarAndButtonPanel
            // 
            this.progressBarAndButtonPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBarAndButtonPanel.AutoSize = true;
            this.progressBarAndButtonPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.progressBarAndButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarAndButtonPanel.Location = new System.Drawing.Point(108, 47);
            this.progressBarAndButtonPanel.Name = "progressBarAndButtonPanel";
            this.progressBarAndButtonPanel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.progressBarAndButtonPanel.Size = new System.Drawing.Size(0, 10);
            this.progressBarAndButtonPanel.TabIndex = 0;
            this.progressBarAndButtonPanel.WrapContents = false;
            // 
            // labelPanel
            // 
            this.labelPanel.Location = new System.Drawing.Point(0, 0);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(200, 100);
            this.labelPanel.TabIndex = 0;
            // 
            // progressPanel
            // 
            this.progressPanel.Location = new System.Drawing.Point(0, 0);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(200, 100);
            this.progressPanel.TabIndex = 0;
            // 
            // bottomRightPanel
            // 
            this.bottomRightPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomRightPanel.Location = new System.Drawing.Point(0, 311);
            this.bottomRightPanel.Name = "bottomRightPanel";
            this.bottomRightPanel.Size = new System.Drawing.Size(428, 100);
            this.bottomRightPanel.TabIndex = 0;
            // 
            // rightButtonPanel
            // 
            this.rightButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.rightButtonPanel.Name = "rightButtonPanel";
            this.rightButtonPanel.Size = new System.Drawing.Size(200, 100);
            this.rightButtonPanel.TabIndex = 1;
            // 
            // guna2Button10
            // 
            this.guna2Button10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button10.ForeColor = System.Drawing.Color.White;
            this.guna2Button10.Location = new System.Drawing.Point(414, 7);
            this.guna2Button10.Name = "guna2Button10";
            this.guna2Button10.Size = new System.Drawing.Size(36, 28);
            this.guna2Button10.TabIndex = 0;
            this.guna2Button10.Text = "✖";
            // 
            // guna2Button11
            // 
            this.guna2Button11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button11.ForeColor = System.Drawing.Color.White;
            this.guna2Button11.Location = new System.Drawing.Point(372, 7);
            this.guna2Button11.Name = "guna2Button11";
            this.guna2Button11.Size = new System.Drawing.Size(36, 28);
            this.guna2Button11.TabIndex = 1;
            this.guna2Button11.Text = "↓";
            // 
            // guna2Button12
            // 
            this.guna2Button12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button12.ForeColor = System.Drawing.Color.White;
            this.guna2Button12.Location = new System.Drawing.Point(330, 7);
            this.guna2Button12.Name = "guna2Button12";
            this.guna2Button12.Size = new System.Drawing.Size(36, 32);
            this.guna2Button12.TabIndex = 2;
            this.guna2Button12.Text = "+";
            // 
            // guna2Button7
            // 
            this.guna2Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button7.ForeColor = System.Drawing.Color.White;
            this.guna2Button7.Location = new System.Drawing.Point(405, 7);
            this.guna2Button7.Name = "guna2Button7";
            this.guna2Button7.Size = new System.Drawing.Size(36, 28);
            this.guna2Button7.TabIndex = 0;
            this.guna2Button7.Text = "✖";
            // 
            // guna2Button6
            // 
            this.guna2Button6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button6.ForeColor = System.Drawing.Color.White;
            this.guna2Button6.Location = new System.Drawing.Point(363, 7);
            this.guna2Button6.Name = "guna2Button6";
            this.guna2Button6.Size = new System.Drawing.Size(36, 28);
            this.guna2Button6.TabIndex = 1;
            this.guna2Button6.Text = "↓";
            // 
            // guna2Button8
            // 
            this.guna2Button8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button8.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.Location = new System.Drawing.Point(321, 7);
            this.guna2Button8.Name = "guna2Button8";
            this.guna2Button8.Size = new System.Drawing.Size(36, 28);
            this.guna2Button8.TabIndex = 2;
            this.guna2Button8.Text = "+";
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
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
            // Form8
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1053, 787);
            this.Controls.Add(this.guna2CircleButtonClose);
            this.Controls.Add(this.layoutRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form8";
            this.layoutRoot.ResumeLayout(false);
            this.centerPanel.ResumeLayout(false);
            this.centerPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.bottomRightInnerPanel.ResumeLayout(false);
            this.arrowsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown2)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ImageList imageList2;
        
    }
}
