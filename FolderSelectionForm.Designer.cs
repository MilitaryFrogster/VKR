using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Drawing;

namespace VKR
{
    partial class FolderSelectionForm
    {
        private IContainer components = null;
        private Guna2ComboBox comboBoxSource;

        private Guna2HtmlLabel labelTitle;
        private TreeView treeViewFolders;
        private Guna2Button buttonCreateFolder;
        private Guna2Button buttonOK;
        private Guna2Button buttonCancel;
        private Guna2BorderlessForm borderlessForm;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxSource = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.treeViewFolders = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonCreateFolder = new Guna.UI2.WinForms.Guna2Button();
            this.buttonOK = new Guna.UI2.WinForms.Guna2Button();
            this.buttonCancel = new Guna.UI2.WinForms.Guna2Button();
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.BackColor = System.Drawing.Color.Transparent;
            this.comboBoxSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSource.FocusedColor = System.Drawing.Color.Empty;
            this.comboBoxSource.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxSource.ForeColor = System.Drawing.Color.Black;
            this.comboBoxSource.ItemHeight = 30;
            this.comboBoxSource.Location = new System.Drawing.Point(12, 49);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(460, 36);
            this.comboBoxSource.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(12, 91);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(109, 25);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Обзор папок";
            // 
            // treeViewFolders
            // 
            this.treeViewFolders.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.treeViewFolders.ImageIndex = 0;
            this.treeViewFolders.ImageList = this.imageList1;
            this.treeViewFolders.Location = new System.Drawing.Point(12, 127);
            this.treeViewFolders.Name = "treeViewFolders";
            this.treeViewFolders.SelectedImageIndex = 0;
            this.treeViewFolders.Size = new System.Drawing.Size(460, 300);
            this.treeViewFolders.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonCreateFolder
            // 
            this.buttonCreateFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonCreateFolder.ForeColor = System.Drawing.Color.White;
            this.buttonCreateFolder.Location = new System.Drawing.Point(12, 437);
            this.buttonCreateFolder.Name = "buttonCreateFolder";
            this.buttonCreateFolder.Size = new System.Drawing.Size(147, 35);
            this.buttonCreateFolder.TabIndex = 3;
            this.buttonCreateFolder.Text = "Создать папку";
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(272, 437);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(95, 35);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(377, 437);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(95, 35);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // borderlessForm
            // 
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.TransparentWhileDrag = true;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(12, 12);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(169, 25);
            this.guna2HtmlLabel1.TabIndex = 6;
            this.guna2HtmlLabel1.Text = "Выберите источник";
            // 
            // FolderSelectionForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(484, 487);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.comboBoxSource);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.treeViewFolders);
            this.Controls.Add(this.buttonCreateFolder);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FolderSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FolderSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Guna2HtmlLabel guna2HtmlLabel1;
    }
}
