using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VKR;

namespace VKR
{
    public partial class FolderSelectionForm : Form
    {
        private List<object> allAccounts;
        private Dictionary<string, ICloudService> cloudServices = new Dictionary<string, ICloudService>();
        
        private System.Windows.Forms.ImageList imageList1;


        public string SelectedPath { get; private set; }
       
        public string SelectedAbsolutePath { get; private set; } // Например: "C:\Users\nikit\Downloads\Новая папка"


        public FolderSelectionForm()
        {
            InitializeComponent();
            this.Load += FolderSelectionForm_Load;
            comboBoxSource.SelectedIndexChanged += ComboBoxSource_SelectedIndexChanged;
            this.treeViewFolders.BeforeExpand += treeViewFolders_BeforeExpand;

        }

        private void FolderSelectionForm_Load(object sender, EventArgs e)
        {
            try
            {
                allAccounts = new List<object> { "Компьютер" };
                allAccounts.AddRange(AccountStorage.LoadAccounts());

                comboBoxSource.Items.Clear();
                comboBoxSource.Items.AddRange(allAccounts.ToArray());
                comboBoxSource.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки аккаунтов: " + ex.Message);
            }
            imageList1 = new ImageList();
            treeViewFolders.ImageList = imageList1;
            LoadIcons();
        }

        private async void ComboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeViewFolders.Nodes.Clear();

            var selected = comboBoxSource.SelectedItem;
            if (selected is string str && str == "Компьютер")
            {
                LoadLocalFolders();
            }
            else if (selected is ConnectedAccount acc)
            {
                if (!cloudServices.ContainsKey(acc.Email))
                {
                    try
                    {
                        var service = CloudServiceFactory.Create(acc);
                        cloudServices[acc.Email] = service;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка подключения к облаку: " + ex.Message);
                        return;
                    }
                }

                var cloudService = cloudServices[acc.Email];
                var rootItems = await cloudService.ListFilesAsync("root");

                foreach (var item in rootItems)
                {
                    var node = new TreeNode(item.Name)
                    {
                        Tag = item
                    };

                    if (item.IsFolder)
                    {
                        node.ImageKey = "folder-yellow";
                        node.SelectedImageKey = "folder-yellow";
                        node.Nodes.Add("...");
                    }
                    else
                    {
                        string ext = Path.GetExtension(item.Name).ToLower();
                        if (!imageList1.Images.ContainsKey(ext))
                            imageList1.Images.Add(ext, GetFileIcon("dummy" + ext)); // Или любой .txt, .pdf — даже несуществующий
                        node.ImageKey = ext;
                        node.SelectedImageKey = ext;
                    }

                    treeViewFolders.Nodes.Add(node);
                }
            }

        }

        private void LoadLocalFolders()
        {
            treeViewFolders.Nodes.Clear();

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                var node = new TreeNode(drive.Name)
                {
                    Tag = drive.Name,
                    ImageKey = "drive",
                    SelectedImageKey = "drive"
                };
                node.Nodes.Add("...");
                treeViewFolders.Nodes.Add(node);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (treeViewFolders.SelectedNode != null)
            {
                var selectedNode = treeViewFolders.SelectedNode;

                // Проверка: если выбран файл (а не папка)
                if (selectedNode.Tag is CloudFile cloudFile && !cloudFile.IsFolder)
                {
                    MessageBox.Show("Нельзя выбрать файл для синхронизации.\nВыберите папку.\nДля передачи одного файла используйте загрузку вручную.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (selectedNode.Tag is string localPath && File.Exists(localPath))
                {
                    MessageBox.Show("Нельзя выбрать файл для синхронизации.\nВыберите папку.\nДля передачи одного файла используйте загрузку вручную.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Всё прошло: это точно папка
                if (selectedNode.Tag is CloudFile cf)
                {
                    if (comboBoxSource.SelectedItem is ConnectedAccount acc)
                    {
                        SelectedPath = $"{acc.Cloud}:{cf.Name}";
                        SelectedAbsolutePath = cf.Id; // <<<<<<<<<< ОБЯЗАТЕЛЬНО!
                    }
                    else
                    {
                        SelectedPath = $"Облако:?|{cf.Name}";
                        SelectedAbsolutePath = cf.Id; // <<<<<<<<<< fallback тоже
                    }
                }

                else if (selectedNode.Tag is string path)
                {
                    SelectedPath = $"Компьютер:{Path.GetFileName(path)}";
                    SelectedAbsolutePath = path;
                }
                else
                {
                    SelectedPath = selectedNode.Text;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }



        private async void treeViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "...")
            {
                node.Nodes.Clear();

                // Локальные папки
                if (node.Tag is string path && Directory.Exists(path))
                {
                    try
                    {
                        foreach (var dir in Directory.GetDirectories(path))
                        {
                            var subNode = new TreeNode(Path.GetFileName(dir))
                            {
                                Tag = dir,
                                ImageKey = "folder-yellow",
                                SelectedImageKey = "folder-yellow"
                            };
                            subNode.Nodes.Add("...");
                            node.Nodes.Add(subNode);
                        }

                        foreach (var file in Directory.GetFiles(path))
                        {
                            string ext = Path.GetExtension(file).ToLower();
                            if (!imageList1.Images.ContainsKey(ext))
                                imageList1.Images.Add(ext, GetFileIcon(file));

                            var fileNode = new TreeNode(Path.GetFileName(file))
                            {
                                Tag = file,
                                ImageKey = ext,
                                SelectedImageKey = ext
                            };
                            node.Nodes.Add(fileNode);
                        }
                    }
                    catch { }
                }

                // Облачные папки
                else if (node.Tag is CloudFile cf && cf.IsFolder)
                {
                    var selected = comboBoxSource.SelectedItem;
                    if (selected is ConnectedAccount acc && cloudServices.TryGetValue(acc.Email, out var cloudService))
                    {
                        try
                        {
                            var children = await cloudService.ListFilesAsync(cf.Id);

                            foreach (var child in children)
                            {
                                var subNode = new TreeNode(child.Name)
                                {
                                    Tag = child
                                };

                                if (child.IsFolder)
                                {
                                    subNode.ImageKey = "folder-yellow";
                                    subNode.SelectedImageKey = "folder-yellow";
                                    subNode.Nodes.Add("...");
                                }
                                else
                                {
                                    string ext = Path.GetExtension(child.Name).ToLower();
                                    if (!imageList1.Images.ContainsKey(ext))
                                        imageList1.Images.Add(ext, GetFileIcon("dummy" + ext));
                                    subNode.ImageKey = ext;
                                    subNode.SelectedImageKey = ext;
                                }

                                node.Nodes.Add(subNode);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка загрузки содержимого: " + ex.Message);
                        }
                    }
                }
            }
        }


        private void LoadIcons()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            imageList1.Images.Clear();

            // Иконка жёлтой папки
            string folderIconPath = Path.Combine(basePath, "Assets", "folder-yellow.ico");
            if (File.Exists(folderIconPath))
                imageList1.Images.Add("folder-yellow", new Icon(folderIconPath));
            else
                imageList1.Images.Add("folder-yellow", SystemIcons.Application);

            // Иконка диска
            string driveIconPath = Path.Combine(basePath, "Assets", "drive.ico");
            if (File.Exists(driveIconPath))
                imageList1.Images.Add("drive", new Icon(driveIconPath));
            else
                imageList1.Images.Add("drive", SystemIcons.WinLogo);

            // Иконка компьютера
            string computerIconPath = Path.Combine(basePath, "Assets", "computer.ico");
            if (File.Exists(computerIconPath))
                imageList1.Images.Add("computer", new Icon(computerIconPath));
            else
                imageList1.Images.Add("computer", SystemIcons.Shield);
        }


        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [Flags]
        public enum SHGFI : uint
        {
            Icon = 0x000000100,
            SmallIcon = 0x000000001,
            UseFileAttributes = 0x000000010
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private Icon GetFileIcon(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                (uint)(SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes));
            return Icon.FromHandle(shinfo.hIcon);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
