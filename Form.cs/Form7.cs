using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Drive.v3;
using System.Web;
using System.Runtime.InteropServices;
using System.Drawing;
using Guna.UI2.WinForms;
using System.Security.Cryptography;

namespace VKR
{
    public partial class Form7 : Form
    {
        private DriveService _driveService;
        private Form2 _parent;
        private List<object> allAccounts; // содержит ConnectedAccount и строку "Компьютер"
        private ICloudService _cloudServiceLeft, _cloudServiceRight;
        private List<string> selectedFiles = new List<string>();

        public Form7(Form2 parent)
        {
            InitializeComponent();
            _parent = parent;


            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = parent.Font;

            Load += Form7_Load;
        }

        private async void Form7_Load(object sender, EventArgs e)
        {
            try
            {
                allAccounts = new List<object> { "Компьютер" };
                allAccounts.AddRange(AccountStorage.LoadAccounts());

                guna2ComboBox1.Items.AddRange(allAccounts.ToArray());
                guna2ComboBox2.Items.AddRange(allAccounts.ToArray());

                guna2ComboBox1.SelectedIndexChanged += Guna2ComboBox1_SelectedIndexChanged;
                guna2ComboBox2.SelectedIndexChanged += Guna2ComboBox2_SelectedIndexChanged;

                treeView2.Nodes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки аккаунтов: " + ex.Message);
            }

            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            listBox1.MouseDoubleClick += listBox1_MouseDoubleClick;
            treeView2.NodeMouseDoubleClick += treeView2_NodeMouseDoubleClick;
            listBox2.MouseDoubleClick += listBox2_MouseDoubleClick;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            treeView1.ImageList = imageList1;
            treeView2.ImageList = imageList1;


            // Icons
            if (!imageList1.Images.ContainsKey("folder"))
                imageList1.Images.Add("folder", GetFileIcon("C:\\Windows"));

            if (!imageList1.Images.ContainsKey("folder-yellow"))
            {
                string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "folder-yellow.ico");
                imageList1.Images.Add("folder-yellow", new Icon(iconPath));
            }
            if (!imageList1.Images.ContainsKey("drive"))
            {
                string driveIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "drive.ico");
                imageList1.Images.Add("drive", new Icon(driveIconPath));
            }
            if (!imageList1.Images.ContainsKey("computer"))
            {
                string compIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "computer.ico");
                imageList1.Images.Add("computer", new Icon(compIconPath));
            }
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
            SHGetFileInfo(path, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), (uint)(SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes));
            return Icon.FromHandle(shinfo.hIcon);
        }

        private void AddNodeWithIcon(TreeNodeCollection parent, string fullPath)
        {
            string key = Path.GetExtension(fullPath).ToLower();

            if (!imageList1.Images.ContainsKey(key))
            {
                Icon icon = GetFileIcon(fullPath);
                imageList1.Images.Add(key, icon);
            }

            TreeNode node = new TreeNode(Path.GetFileName(fullPath));
            node.ImageKey = key;
            node.SelectedImageKey = key;
            node.Tag = fullPath;
            parent.Add(node);
        }

        private void PopulateLocalTreeView(TreeView tree)
        {
            tree.BeforeExpand -= Cloud_BeforeExpand;
            tree.BeforeExpand -= Local_BeforeExpand;

            tree.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Компьютер")
            {
                ImageKey = "computer",
                SelectedImageKey = "computer"
            };
            tree.Nodes.Add(rootNode);

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.IsReady))
            {
                var driveNode = new TreeNode(drive.Name)
                {
                    Tag = drive.RootDirectory.FullName,
                    ImageKey = "drive",
                    SelectedImageKey = "drive"
                };
                driveNode.Nodes.Add(new TreeNode());
                rootNode.Nodes.Add(driveNode);
            }

            // ВЕШАЕМ ТОЛЬКО ИМЕНОВАННЫЙ!
            tree.BeforeExpand += Local_BeforeExpand;
        }


        private async Task PopulateCloudTreeAsync(object account, TreeView tree)
        {
            if (account is ConnectedAccount acc)
            {
                ICloudService service = await GetCloudServiceAsync(acc);
                if (service == null) return;

                if (tree == treeView1)
                    _cloudServiceLeft = service;
                else if (tree == treeView2)
                    _cloudServiceRight = service;

                tree.Nodes.Clear();
                var list = await service.ListFilesAsync("root");

                foreach (var cf in list)
                {
                    string displayName = cf.Name.EndsWith(".enc") ? $"{cf.Name} (зашифрован)" : cf.Name;

                    TreeNode node = new TreeNode(displayName)
                    {
                        Tag = cf
                    };

                    if (cf.IsFolder)
                    {
                        node.ImageKey = "folder-yellow";
                        node.SelectedImageKey = "folder-yellow";
                        node.Nodes.Add(new TreeNode()); // Lazy load
                    }
                    else
                    {
                        string ext = Path.GetExtension(cf.Name).ToLower();
                        if (!imageList1.Images.ContainsKey(ext))
                            imageList1.Images.Add(ext, GetFileIcon("dummy" + ext)); // или другой универсальный путь

                        node.ImageKey = ext;
                        node.SelectedImageKey = ext;
                    }

                    tree.Nodes.Add(node);
                }



                tree.BeforeExpand -= Cloud_BeforeExpand;
                tree.BeforeExpand += Cloud_BeforeExpand;
            }
        }

        private async Task<ICloudService> GetCloudServiceAsync(object item)
        {
            if (item is ConnectedAccount acc)
            {
                if (acc.Cloud == "Google Drive")
                {
                    var driveService = await GoogleDriveAuth.CreateDriveServiceFromStoreAsync(acc.Email);
                    return new GoogleDriveService(driveService);
                }
                else if (acc.Cloud == "Dropbox")
                {
                    return new DropboxService(acc.AccessToken);
                }
            }
            return null;
        }

        private async void Guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = guna2ComboBox1.SelectedItem;
            if (selected == null) return;

            treeView1.Nodes.Clear();
            _cloudServiceLeft = null;

            // Снимаем Cloud_BeforeExpand и Local_BeforeExpand
            treeView1.BeforeExpand -= Cloud_BeforeExpand;
            treeView1.BeforeExpand -= Local_BeforeExpand;

            // Обновляем второй ComboBox
            var previouslySelected = guna2ComboBox2.SelectedItem;

            guna2ComboBox2.SelectedIndexChanged -= Guna2ComboBox2_SelectedIndexChanged;
            guna2ComboBox2.Items.Clear();
            guna2ComboBox2.Items.AddRange(allAccounts.Where(x => !x.Equals(selected)).ToArray());

            if (previouslySelected != null && !previouslySelected.Equals(selected))
                guna2ComboBox2.SelectedItem = previouslySelected;
            else
                guna2ComboBox2.SelectedIndex = -1;

            guna2ComboBox2.SelectedIndexChanged += Guna2ComboBox2_SelectedIndexChanged;

            // ВАЖНО: тут правильная проверка!
            if (selected is string str && str == "Компьютер")
            {
                PopulateLocalTreeView(treeView1);
            }
            else if (selected is ConnectedAccount acc)
            {
                await PopulateCloudTreeAsync(acc, treeView1);
            }
        }

        private async void Guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = guna2ComboBox2.SelectedItem;
            if (selected == null) return;

            treeView2.Nodes.Clear();
            _cloudServiceRight = null;

            // Снимаем Cloud_BeforeExpand и Local_BeforeExpand
            treeView2.BeforeExpand -= Cloud_BeforeExpand;
            treeView2.BeforeExpand -= Local_BeforeExpand;

            // Обновляем левый ComboBox
            var previouslySelected = guna2ComboBox1.SelectedItem;

            guna2ComboBox1.SelectedIndexChanged -= Guna2ComboBox1_SelectedIndexChanged;
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.AddRange(allAccounts.Where(x => !x.Equals(selected)).ToArray());

            if (previouslySelected != null && !previouslySelected.Equals(selected))
                guna2ComboBox1.SelectedItem = previouslySelected;
            else
                guna2ComboBox1.SelectedIndex = -1;

            guna2ComboBox1.SelectedIndexChanged += Guna2ComboBox1_SelectedIndexChanged;

            // ВАЖНО: тут правильная проверка!
            if (selected is string str && str == "Компьютер")
            {
                PopulateLocalTreeView(treeView2);
            }
            else if (selected is ConnectedAccount acc)
            {
                await PopulateCloudTreeAsync(acc, treeView2);
            }
        }

        private async void Cloud_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var tree = sender as TreeView;
            var node = e.Node;

            if (!(node.Tag is CloudFile cf) || !cf.IsFolder || node.Nodes.Count != 1 || node.Nodes[0].Tag != null)
                return;


            node.Nodes.Clear();

            ICloudService service = null;

            if (tree == treeView1 && _cloudServiceLeft != null)
                service = _cloudServiceLeft;
            else if (tree == treeView2 && _cloudServiceRight != null)
                service = _cloudServiceRight;
            else
                return;

            try
            {
                var children = await service.ListFilesAsync(cf.Id);

                foreach (var child in children)
                {
                    string displayName = child.Name.EndsWith(".enc") ? $"{child.Name} (зашифрован)" : child.Name;

                    TreeNode childNode = new TreeNode(displayName)
                    {
                        Tag = child
                    };

                    if (child.IsFolder)
                    {
                        childNode.ImageKey = "folder-yellow";
                        childNode.SelectedImageKey = "folder-yellow";
                        childNode.Nodes.Add(new TreeNode()); // Lazy load
                    }
                    else
                    {
                        string ext = Path.GetExtension(child.Name).ToLower();
                        if (!imageList1.Images.ContainsKey(ext))
                            imageList1.Images.Add(ext, GetFileIcon("dummy" + ext)); // или любой .exe/.txt путь для иконки

                        childNode.ImageKey = ext;
                        childNode.SelectedImageKey = ext;
                    }

                    node.Nodes.Add(childNode);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при раскрытии папки: " + ex.Message);
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;

            // Локальный компьютер
            if (node.Tag is string path)
            {
                bool alreadyExists = listBox1.Items.Cast<object>().Any(x => x is string localPath && localPath == path);

                if ((File.Exists(path) || Directory.Exists(path)) && !alreadyExists)
                {
                    listBox1.Items.Add(path);
                    selectedFiles.Add(path);  // Это можно оставить если тебе нужно отслеживать выбранные (или вообще убрать — в listBox1 уже будет всё видно)
                }
            }
            // Облако
            else if (node.Tag is CloudFile cf)
            {
                bool alreadyExists = listBox1.Items.Cast<object>().Any(x =>
                    x is CloudFile cloud && cloud.Id == cf.Id);

                if (!alreadyExists)
                {
                    listBox1.Items.Add(cf);
                }
            }
        }

        private void treeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;

            // Если локальный ПК
            if (node.Tag is string path)
            {
                bool alreadyExists = listBox2.Items.Cast<object>().Any(x => x is string localPath && localPath == path);

                if ((File.Exists(path) || Directory.Exists(path)) && !alreadyExists)
                {
                    listBox2.Items.Add(path);
                }
            }
            // Если облако
            else if (node.Tag is CloudFile cf)
            {
                bool alreadyExists = listBox2.Items.Cast<object>().Any(x => x is CloudFile cloud && cloud.Id == cf.Id);

                if (!alreadyExists)
                {
                    listBox2.Items.Add(cf);
                }
            }
        }


        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = listBox1.Items[index];

                // Если ты всё-таки хочешь вести selectedFiles, можно добавить проверку:
                if (item is string path)
                    selectedFiles.Remove(path);

                listBox1.Items.RemoveAt(index);
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox2.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listBox2.Items.RemoveAt(index);
            }
        }

        private TreeNode FindNodeByCloudFile(TreeNodeCollection nodes, CloudFile target)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is CloudFile cf && cf.Id == target.Id)
                    return node;

                TreeNode found = FindNodeByCloudFile(node.Nodes, target);
                if (found != null)
                    return found;
            }
            return null;
        }

        private async Task DeleteSelectedItemsAsync(ListBox listBox, TreeView treeView, ICloudService service)
        {
            if (listBox.Items.Count == 0)
            {
                MessageBox.Show("Сначала выберите объекты для удаления.");
                return;
            }

            var toDelete = listBox.Items.Cast<CloudFile>().ToList();
            string names = string.Join("\n", toDelete.Select(f => f.Name));
            var result = MessageBox.Show($"Вы уверены, что хотите удалить следующие объекты?\n\n{names}",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    foreach (var item in toDelete)
                    {
                        await service.DeleteFileOrFolderAsync(item.Id);
                        TreeNode node = FindNodeByCloudFile(treeView.Nodes, item);
                        node?.Remove();
                    }

                    listBox.Items.Clear();
                    MessageBox.Show("Объекты удалены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message);
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Принудительно повторно установить ImageKey при выборе
            if (e.Node.ImageKey != null)
                e.Node.SelectedImageKey = e.Node.ImageKey;
        }
        private async Task DownloadFromCloudAsync(CloudFile item, string parentPath, ICloudService service)
        {
            string outputPath = Path.Combine(parentPath, item.Name);

            if (item.IsFolder)
            {
                Directory.CreateDirectory(outputPath);
                var children = await service.ListFilesAsync(item.Id);
                foreach (var child in children)
                {
                    await DownloadFromCloudAsync(child, outputPath, service);
                }
            }
            else
            {
                byte[] data = await service.DownloadFileAsync(item.Id);

                if (item.Name.EndsWith(".enc"))
                {
                    string originalName = item.Name.Substring(0, item.Name.Length - 4);
                    string decryptedPath = Path.Combine(parentPath, originalName);
                    EncryptionHelper.DecryptFile(data, decryptedPath);
                }
                else
                {
                    File.WriteAllBytes(outputPath, data);
                }
            }
        }

        private async Task DownloadAllFromListBox1Async()
        {
            if (_cloudServiceLeft == null)
            {
                MessageBox.Show("Скачивание доступно только из облака.");
                return;
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Сначала выберите объекты для скачивания.");
                return;
            }

            string downloadBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NcopyDownloads");
            Directory.CreateDirectory(downloadBase);

            foreach (var item in listBox1.Items)
            {
                if (item is CloudFile cf)
                {
                    await DownloadFromCloudAsync(cf, downloadBase, _cloudServiceLeft);
                }
            }

            listBox1.Items.Clear();
            MessageBox.Show("Скачивание завершено.");
        }

        private async Task DownloadAllFromListBox2Async()
        {
            if (guna2ComboBox2.SelectedItem?.ToString() == "Компьютер" || _cloudServiceRight == null)
            {
                MessageBox.Show("Скачивание доступно только с облачного хранилища.");
                return;
            }

            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Сначала выберите объекты для скачивания.");
                return;
            }

            string downloadBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NcopyDownloads");
            Directory.CreateDirectory(downloadBase);

            foreach (var item in listBox2.Items)
            {
                if (item is CloudFile cf)
                {
                    await DownloadFromCloudAsync(cf, downloadBase, _cloudServiceRight);
                }
            }

            listBox2.Items.Clear();
            MessageBox.Show("Скачивание завершено.");
        }

        private async void Guna2Button6_Click(object sender, EventArgs e)
        {
            await DownloadAllFromListBox2Async();
        }

        private async void Guna2Button11_Click(object sender, EventArgs e)
        {
            await DownloadAllFromListBox1Async();
        }

        private async void Guna2Button7_Click(object sender, EventArgs e)
        {
            if (_cloudServiceRight != null)
                await DeleteSelectedItemsAsync(listBox2, treeView2, _cloudServiceRight);
            else
                MessageBox.Show("Удаление на локальном компьютере не поддерживается.");
        }

        private async void Guna2Button10_Click(object sender, EventArgs e)
        {
            if (_cloudServiceLeft != null)
                await DeleteSelectedItemsAsync(listBox1, treeView1, _cloudServiceLeft);
            else
                MessageBox.Show("Удаление на локальном компьютере не поддерживается.");
        }
        private async void Guna2Button12_Click(object sender, EventArgs e)
        {
            if (_cloudServiceLeft == null)
            {
                MessageBox.Show("Создание папок доступно только для облачных хранилищ.");
                return;
            }

            using (var inputForm = new Form12())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string folderName = inputForm.FolderName?.Trim();
                    if (string.IsNullOrEmpty(folderName))
                    {
                        MessageBox.Show("Введите корректное имя папки.");
                        return;
                    }

                    string parentId = "root";
                    TreeNode parentNode = null;

                    if (listBox1.Items.Count == 1 && listBox1.Items[0] is CloudFile cf && cf.IsFolder)
                    {
                        parentId = cf.Id;
                        parentNode = FindNodeByCloudFile(treeView1.Nodes, cf);
                    }

                    try
                    {
                        var newFolder = await _cloudServiceLeft.CreateFolderAsync(folderName, parentId);

                        TreeNode newNode = new TreeNode(newFolder.Name)
                        {
                            Tag = newFolder,
                            ImageKey = "folder-yellow",
                            SelectedImageKey = "folder-yellow"
                        };
                        newNode.Nodes.Add(new TreeNode()); // Lazy load

                        if (parentNode != null)
                        {
                            parentNode.Nodes.Add(newNode);
                            parentNode.Expand();
                        }
                        else
                        {
                            treeView1.Nodes.Add(newNode);
                        }

                        listBox1.Items.Clear();
                        MessageBox.Show("Папка успешно создана.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при создании папки: " + ex.Message);
                    }
                }
            }
        }

        private async void Guna2Button8_Click(object sender, EventArgs e)
        {
            if (_cloudServiceRight == null)
            {
                MessageBox.Show("Выберите облако для создания папки справа.");
                return;
            }

            using (var form = new Form12())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string folderName = form.FolderName;
                    if (string.IsNullOrWhiteSpace(folderName))
                    {
                        MessageBox.Show("Введите корректное имя папки.");
                        return;
                    }

                    string parentId = "root";
                    TreeNode parentNode = null;

                    if (listBox2.Items.Count == 1 && listBox2.Items[0] is CloudFile selected && selected.IsFolder)
                    {
                        parentId = selected.Id;
                        parentNode = FindNodeByCloudFile(treeView2.Nodes, selected);
                    }

                    try
                    {
                        var newFolder = await _cloudServiceRight.CreateFolderAsync(folderName, parentId);

                        TreeNode newNode = new TreeNode(newFolder.Name)
                        {
                            Tag = newFolder,
                            ImageKey = "folder-yellow",
                            SelectedImageKey = "folder-yellow"
                        };
                        newNode.Nodes.Add(new TreeNode()); // Lazy load

                        if (parentNode != null)
                        {
                            parentNode.Nodes.Add(newNode);
                            parentNode.Expand();
                        }
                        else
                        {
                            treeView2.Nodes.Add(newNode);
                        }

                        listBox2.Items.Clear();
                        MessageBox.Show("Папка успешно создана.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при создании папки: " + ex.Message);
                    }
                }
            }
        }

        private async void Guna2Button9_Click(object sender, EventArgs e)
        {
            if (selectedFiles.Count > 0)
            {
                MessageBox.Show("Сначала очистите список слева, если хотите передавать справа.");
                return;
            }

            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Выберите файлы или папки справа.");
                return;
            }

            if (listBox1.Items.Count != 1 || !(listBox1.Items[0] is CloudFile cloudDest) || !cloudDest.IsFolder)
            {
                MessageBox.Show("Выберите одну папку назначения слева (в облаке).");
                return;
            }

            string targetId = cloudDest.Id;
            string targetPath = cloudDest.Path;

            try
            {
                foreach (var item in listBox2.Items)
                {
                    // Облако → Облако
                    if (item is CloudFile cf)
                    {
                        if (cf.IsFolder)
                        {
                            // Передача всей папки
                            await SyncCloudFolderToCloudAsync(cf, targetId);
                        }
                        else
                        {
                            // Передача одного файла
                            byte[] data = await _cloudServiceRight.DownloadFileAsync(cf.Id);

                            string uploadName = cf.Name;
                            if (checkBoxEncryptUpload.Checked)
                            {
                                data = EncryptionHelper.EncryptData(data);
                                uploadName += ".enc";
                            }

                            using (var ms = new MemoryStream(data))
                            {
                                if (_cloudServiceLeft is GoogleDriveService gds)
                                    await gds.UploadFileAsync(ms, uploadName, targetId);
                                else if (_cloudServiceLeft is DropboxService dbs)
                                    await dbs.UploadFileAsync(ms, uploadName, targetPath);
                                else
                                    await _cloudServiceLeft.UploadFileAsync(ms, uploadName, targetId);
                            }
                        }
                    }
                    // Обнаружено: путь с ПК — запрещаем
                    else if (item is string)
                    {
                        MessageBox.Show("Нельзя передавать напрямую с ПК на ПК.\nДля этого используйте кнопку загрузки слева направо.");
                        return;
                    }
                }

                MessageBox.Show("Передача завершена.");
                listBox2.Items.Clear();

                // Обновляем дерево слева
                if (guna2ComboBox1.SelectedItem is ConnectedAccount acc)
                    await PopulateCloudTreeAsync(acc, treeView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при передаче: " + ex.Message);
            }
        }






        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            if (_parent != null)
                _parent.OpenFormInPanel(new Form6(_parent));
            else
                MessageBox.Show("Форма-родитель не найдена");
        }

        private void Guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        /*private async void Guna2Button2_Click(object sender, EventArgs e)
        {
            if (_cloudServiceRight == null)
            {
                MessageBox.Show("Выберите облако-получатель справа.");
                return;
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Список файлов слева пуст.");
                return;
            }

            if (listBox2.Items.Count > 1)
            {
                MessageBox.Show("Выберите одну папку назначения справа.");
                return;
            }

            // Получаем ID или Path папки-приемника
            string parentId = null;
            string parentPath = null;

            if (listBox2.Items.Count == 1 && listBox2.Items[0] is CloudFile dest && dest.IsFolder)
            {
                parentId = dest.Id;     // для Google Drive
                parentPath = dest.Path; // для Dropbox
            }
            else
            {
                parentId = "root";
                parentPath = "";  // Dropbox: пустая строка или "/"
            }

            try
            {
                foreach (var item in listBox1.Items)
                {
                    // --- Передача из ЛОКАЛЬНОГО ПК в облако ---
                    if (item is string localPath && File.Exists(localPath))
                    {
                        bool encrypt = checkBoxEncryptUpload.Checked;

                        if (_cloudServiceRight is GoogleDriveService gds)
                            await gds.UploadFileAsync(localPath, parentId, encrypt);
                        else if (_cloudServiceRight is DropboxService dbs)
                            await dbs.UploadFileAsync(localPath, parentPath, encrypt);
                        else
                            await _cloudServiceRight.UploadFileAsync(localPath, parentId);
                    }
                    else if (item is string folderPath && Directory.Exists(folderPath))
                    {
                        if (_cloudServiceRight is GoogleDriveService gds)
                            await gds.UploadFolderAsync(folderPath, parentId);
                        else if (_cloudServiceRight is DropboxService dbs)
                            await dbs.UploadFolderAsync(folderPath, parentPath);
                        else
                            await _cloudServiceRight.UploadFolderAsync(folderPath, parentId);
                    }
                    // --- Передача ФАЙЛОВ между облаками ---
                    else if (item is CloudFile cf && !cf.IsFolder && !_cloudServiceLeft.Equals(_cloudServiceRight))
                    {
                        long fileSizeThreshold = 104857600; // 100 MB

                        if (cf.Size < fileSizeThreshold)
                        {
                            // Маленький файл — читаем полностью
                            byte[] data = await _cloudServiceLeft.DownloadFileAsync(cf.Id);

                            bool encrypt = checkBoxEncryptUpload.Checked;
                            string uploadName = cf.Name;

                            if (encrypt)
                            {
                                byte[] encryptedData = EncryptionHelper.EncryptData(data);
                                uploadName += ".enc";
                                data = encryptedData;
                            }

                            using (var ms = new MemoryStream(data))
                            {
                                if (_cloudServiceRight is GoogleDriveService gds)
                                    await gds.UploadFileAsync(ms, uploadName, parentId);
                                else if (_cloudServiceRight is DropboxService dbs)
                                    await dbs.UploadFileAsync(ms, uploadName, parentPath);
                                else
                                    await _cloudServiceRight.UploadFileAsync(ms, uploadName, parentId);
                            }
                        }
                        else
                        {
                            // Большой файл — потоковая передача с прогрессом!
                            using (var downloadStream = await _cloudServiceLeft.OpenDownloadStreamAsync(cf.Id))
                            {
                                Stream uploadStream = null;
                                string uploadName = cf.Name;

                                if (checkBoxEncryptUpload.Checked)
                                {
                                    var encryptor = EncryptionHelper.CreateStreamingEncryptor(out var iv);
                                    uploadStream = new CryptoStream(downloadStream, encryptor, CryptoStreamMode.Read);
                                    uploadName += ".enc";
                                }
                                else
                                {
                                    uploadStream = downloadStream;
                                }

                                // Запускаем прогресс
                                await CopyStreamWithProgressAsync(uploadStream, _cloudServiceRight, uploadName, parentId, parentPath, cf.Size, guna2VProgressBar1, guna2HtmlLabel2);
                            }
                        }
                    }
                }

                // После завершения — скрываем прогресс:
                guna2VProgressBar1.Visible = false;
                guna2HtmlLabel2.Visible = false;

                MessageBox.Show("Загрузка завершена.");
                listBox1.Items.Clear();
                selectedFiles.Clear();

                if (guna2ComboBox2.SelectedItem is ConnectedAccount acc)
                    await PopulateCloudTreeAsync(acc, treeView2);
            }
            catch (Exception ex)
            {
                guna2VProgressBar1.Visible = false;
                guna2HtmlLabel2.Visible = false;
                MessageBox.Show("Ошибка при загрузке: " + ex.Message);
            }
        }*/





        private void TreeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void Guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            // Заглушка (не используется)
        }


        private void Local_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Tag == null)
            {
                node.Nodes.Clear();
                var path = node.Tag as string;
                try
                {
                    foreach (var dir in Directory.GetDirectories(path))
                    {
                        var sub = new TreeNode(Path.GetFileName(dir))
                        {
                            Tag = dir,
                            ImageKey = "folder-yellow",
                            SelectedImageKey = "folder-yellow"
                        };
                        sub.Nodes.Add(new TreeNode());
                        node.Nodes.Add(sub);
                    }
                    foreach (var file in Directory.GetFiles(path))
                    {
                        AddNodeWithIcon(node.Nodes, file);
                    }
                }
                catch { }
            }
        }



        private async Task CopyStreamWithProgressAsync(Stream sourceStream, ICloudService targetService, string uploadName, string parentId, string parentPath, long totalLength, Guna.UI2.WinForms.Guna2ProgressBar progressBar, Guna.UI2.WinForms.Guna2HtmlLabel label)
        {
            progressBar.BringToFront();
            label.BringToFront();

            Console.WriteLine("[DEBUG] progressBar.Visible = true, label.Visible = true");

            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.Visible = true;

            label.Visible = true;
            label.Text = "Подготовка...";

            // Оборачиваем в ProgressStream
            using (var progressStream = new ProgressStream(sourceStream, (progress) =>
            {
                int percent = (int)((progress * 100L) / totalLength);
                progressBar.Invoke((Action)(() => progressBar.Value = percent));

                long uploadedMB = progress / 1024 / 1024;
                long totalMB = totalLength / 1024 / 1024;

                label.Invoke((Action)(() => label.Text = $"Передача: {percent}% ({uploadedMB} MB / {totalMB} MB)"));
            }))
            {
                // Теперь прямо progressStream передаём в Upload
                if (targetService is GoogleDriveService gds)
                {
                    await gds.UploadFileAsync(progressStream, uploadName, parentId);
                }
                else if (targetService is DropboxService dbs)
                {
                    await dbs.UploadFileAsync(progressStream, uploadName, parentPath);
                }
                else
                {
                    await targetService.UploadFileAsync(progressStream, uploadName, parentId);
                }
            }

            progressBar.Value = 100;
            label.Text = "Передача завершена.";

        }

        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            if (_cloudServiceRight == null)
            {
                MessageBox.Show("Выберите облако-назначение справа.");
                return;
            }

            if (listBox1.Items.Count != 1)
            {
                MessageBox.Show("Выберите ОДНУ папку источника слева.");
                return;
            }

            if (listBox2.Items.Count != 1 || !(listBox2.Items[0] is CloudFile destFolder) || !destFolder.IsFolder)
            {
                MessageBox.Show("Выберите ОДНУ папку-приемник справа.");
                return;
            }

            try
            {
                // Включаем прогресс
                guna2ProgressBar1.Visible = true;
                guna2HtmlLabel1.Visible = true;
                guna2ProgressBar1.Minimum = 0;
                guna2ProgressBar1.Maximum = 100;
                guna2ProgressBar1.Value = 0;
                guna2HtmlLabel1.Text = "Подготовка...";

                await SynchronizeAsync(listBox1.Items[0], destFolder);

                MessageBox.Show("Синхронизация завершена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при синхронизации: " + ex.Message);
            }
            finally
            {
                // Скрываем прогресс
                guna2ProgressBar1.Visible = false;
                guna2HtmlLabel1.Visible = false;
            }
        }






        private void CopyDirectory(string sourceDir, string targetDir)
        {
            string destSubDir = Path.Combine(targetDir, Path.GetFileName(sourceDir));
            Directory.CreateDirectory(destSubDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destSubDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite: true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                CopyDirectory(dir, destSubDir);
            }
        }


       /* private async Task SyncLocalFolderToCloudAsync(string localFolderPath, string destFolderId, ref int uploadedCount)
        {
            // Получаем список файлов в целевой папке
            var destFiles = await _cloudServiceRight.ListFilesAsync(destFolderId);

            // Проходим по файлам в локальной папке
            foreach (var filePath in Directory.GetFiles(localFolderPath))
            {
                string fileName = Path.GetFileName(filePath);

                // Проверяем — есть ли уже такой файл в целевой папке
                bool exists = destFiles.Any(f => f.Name == fileName && !f.IsFolder);

                if (!exists)
                {
                    // Загружаем с прогрессом
                    using (var stream = File.OpenRead(filePath))
                    {
                        long fileSize = stream.Length;

                        await CopyStreamWithProgressAsync(stream, _cloudServiceRight, fileName, destFolderId, "", fileSize, guna2VProgressBar1, guna2HtmlLabel2);
                    }

                    uploadedCount++;
                }
            }

            // Рекурсивно обрабатываем подпапки
            foreach (var subDir in Directory.GetDirectories(localFolderPath))
            {
                string subFolderName = Path.GetFileName(subDir);

                // Проверяем — есть ли уже такая папка в целевой папке
                var existingFolder = destFiles.FirstOrDefault(f => f.Name == subFolderName && f.IsFolder);

                CloudFile destSubFolder;
                if (existingFolder == null)
                {
                    // Создаём папку
                    destSubFolder = await _cloudServiceRight.CreateFolderAsync(subFolderName, destFolderId);
                }
                else
                {
                    destSubFolder = existingFolder;
                }

                // Рекурсивный вызов
                await SyncLocalFolderToCloudAsync(subDir, destSubFolder.Id, ref uploadedCount);
            }
        }*/


        private async Task SynchronizeAsync(object sourceFolder, CloudFile destFolder)
        {
            // Получаем список файлов в источнике
            List<string> sourceFiles = new List<string>();

            // Источник - локальный ПК
            if (sourceFolder is string localPath && Directory.Exists(localPath))
            {
                sourceFiles = Directory.GetFiles(localPath).Select(Path.GetFileName).ToList();
            }
            // Источник - облако (CloudFile)
            else if (sourceFolder is CloudFile cfSource && cfSource.IsFolder && _cloudServiceLeft != null)
            {
                var cloudFiles = await ListFilesRecursiveAsync(_cloudServiceLeft, cfSource.Id);
                sourceFiles = cloudFiles.Where(f => !f.IsFolder).Select(f => f.Name).ToList();
            }
            else
            {
                MessageBox.Show("Неподдерживаемый источник.");
                return;
            }

            // Получаем список файлов в целевой папке
            var targetFilesCloud = await ListFilesRecursiveAsync(_cloudServiceRight, destFolder.Id);
            List<string> targetFiles = targetFilesCloud.Where(f => !f.IsFolder).Select(f => f.Name).ToList();

            // Сравниваем - ищем файлы, которых нет в целевой папке
            var filesToUpload = sourceFiles.Except(targetFiles).ToList();

            if (filesToUpload.Count == 0)
            {
                MessageBox.Show("Все файлы уже синхронизированы.");
                return;
            }

            // Настраиваем прогресс по количеству файлов
            int totalFiles = filesToUpload.Count;
            int currentIndex = 0;

            guna2ProgressBar1.Invoke((Action)(() =>
            {
                guna2ProgressBar1.Minimum = 0;
                guna2ProgressBar1.Maximum = totalFiles;
                guna2ProgressBar1.Value = 0;
            }));

            // Загружаем недостающие файлы
            foreach (var fileName in filesToUpload)
            {
                currentIndex++;

                guna2HtmlLabel1.Invoke((Action)(() =>
                {
                    guna2HtmlLabel1.Text = $"Передача: {currentIndex} / {totalFiles} — {fileName}";
                }));

                guna2ProgressBar1.Invoke((Action)(() =>
                {
                    guna2ProgressBar1.Value = currentIndex;
                }));

                // --- Источник ЛОКАЛЬНЫЙ ПК ---
                if (sourceFolder is string localPath2)
                {
                    string fullPath = Path.Combine(localPath2, fileName);

                    if (_cloudServiceRight is GoogleDriveService gds)
                        await gds.UploadFileAsync(fullPath, destFolder.Id, checkBoxEncryptUpload.Checked);
                    else if (_cloudServiceRight is DropboxService dbs)
                        await dbs.UploadFileAsync(fullPath, destFolder.Path, checkBoxEncryptUpload.Checked);
                    else
                        await _cloudServiceRight.UploadFileAsync(fullPath, destFolder.Id);
                }
                // --- Источник ОБЛАКО ---
                else if (sourceFolder is CloudFile cfSource2)
                {
                    // Находим соответствующий CloudFile в source cloud
                    var matchingFile = (await ListFilesRecursiveAsync(_cloudServiceLeft, cfSource2.Id))
                                        .FirstOrDefault(f => !f.IsFolder && f.Name == fileName);

                    if (matchingFile == null)
                        continue;

                    // Скачиваем из source cloud
                    byte[] data = await _cloudServiceLeft.DownloadFileAsync(matchingFile.Id);

                    string uploadFileName = fileName;  // копируем

                    if (checkBoxEncryptUpload.Checked)
                    {
                        data = EncryptionHelper.EncryptData(data);
                        uploadFileName = uploadFileName + ".enc";
                    }

                    // Загружаем в целевое облако
                    using (var ms = new MemoryStream(data))
                    {
                        if (_cloudServiceRight is GoogleDriveService gds)
                            await gds.UploadFileAsync(ms, fileName, destFolder.Id);
                        else if (_cloudServiceRight is DropboxService dbs)
                            await dbs.UploadFileAsync(ms, fileName, destFolder.Path);
                        else
                            await _cloudServiceRight.UploadFileAsync(ms, fileName, destFolder.Id);
                    }
                }
            }

            // Завершаем прогресс
            guna2HtmlLabel1.Invoke((Action)(() =>
            {
                guna2HtmlLabel1.Text = "Синхронизация завершена.";
            }));

            guna2ProgressBar1.Invoke((Action)(() =>
            {
                guna2ProgressBar1.Value = totalFiles;
            }));
        }

        /*private async Task<int> SyncCloudFolderToCloudAsync(CloudFile sourceFolder, string destFolderId)
        {
            int uploadedCount = 0;

            var sourceFiles = await _cloudServiceLeft.ListFilesAsync(sourceFolder.Id);
            var destFiles = await _cloudServiceRight.ListFilesAsync(destFolderId);

            foreach (var srcFile in sourceFiles)
            {
                if (!srcFile.IsFolder)
                {
                    bool exists = destFiles.Any(f => f.Name == srcFile.Name && !f.IsFolder);

                    if (!exists)
                    {
                        using (var downloadStream = await _cloudServiceLeft.OpenDownloadStreamAsync(srcFile.Id))
                        {
                            long fileSize = srcFile.Size;

                            await CopyStreamWithProgressAsync(downloadStream, _cloudServiceRight, srcFile.Name, destFolderId, "", fileSize, guna2VProgressBar1, guna2HtmlLabel2);
                        }

                        uploadedCount++;
                    }
                }
                else
                {
                    // Папка — обрабатываем рекурсивно
                    var existingFolder = destFiles.FirstOrDefault(f => f.Name == srcFile.Name && f.IsFolder);

                    CloudFile destSubFolder;
                    if (existingFolder == null)
                    {
                        destSubFolder = await _cloudServiceRight.CreateFolderAsync(srcFile.Name, destFolderId);
                    }
                    else
                    {
                        destSubFolder = existingFolder;
                    }

                    // 📌 Рекурсивный вызов и добавление результата
                    int subUploaded = await SyncCloudFolderToCloudAsync(srcFile, destSubFolder.Id);
                    uploadedCount += subUploaded;
                }
            }

            return uploadedCount;
        }*/



        private void guna2CircleButtonClose_Click(object sender, EventArgs e)
        {
            using (var confirm = new FormExitConfirm())
            {
                if (confirm.ShowDialog() == DialogResult.Yes)
                {
                    Application.Exit(); // или this.Close()
                }
            }
        }

        /*private async void guna2Button12_Click_1(object sender, EventArgs e)
        {
            if (_cloudServiceLeft == null)
            {
                MessageBox.Show("Создание папок доступно только для облачных хранилищ.");
                return;
            }

            using (var inputForm = new Form12())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string folderName = inputForm.FolderName?.Trim();
                    if (string.IsNullOrEmpty(folderName))
                    {
                        MessageBox.Show("Введите корректное имя папки.");
                        return;
                    }

                    string parentId = "root";
                    TreeNode parentNode = null;

                    if (listBox1.Items.Count == 1 && listBox1.Items[0] is CloudFile cf && cf.IsFolder)
                    {
                        parentId = cf.Id;
                        parentNode = FindNodeByCloudFile(treeView1.Nodes, cf);
                    }

                    try
                    {
                        var newFolder = await _cloudServiceLeft.CreateFolderAsync(folderName, parentId);

                        TreeNode newNode = new TreeNode(newFolder.Name)
                        {
                            Tag = newFolder,
                            ImageKey = "folder-yellow",
                            SelectedImageKey = "folder-yellow"
                        };
                        newNode.Nodes.Add(new TreeNode()); // Lazy load

                        if (parentNode != null)
                        {
                            parentNode.Nodes.Add(newNode);
                            parentNode.Expand();
                        }
                        else
                        {
                            treeView1.Nodes.Add(newNode);
                        }

                        listBox1.Items.Clear();
                        MessageBox.Show("Папка успешно создана.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при создании папки: " + ex.Message);
                    }
                }
            }
        }*/

        /*private async void guna2Button11_Click_1(object sender, EventArgs e)
        {
            await DownloadAllFromListBox1Async();
        }

        private async void guna2Button10_Click_1(object sender, EventArgs e)
        {
            if (_cloudServiceLeft != null)
                await DeleteSelectedItemsAsync(listBox1, treeView1, _cloudServiceLeft);
            else
                MessageBox.Show("Удаление на локальном компьютере не поддерживается.");
        }*/

        /* private async void guna2Button8_Click_1(object sender, EventArgs e)
         {
             if (_cloudServiceRight == null)
             {
                 MessageBox.Show("Выберите облако для создания папки справа.");
                 return;
             }

             using (var form = new Form12())
             {
                 if (form.ShowDialog() == DialogResult.OK)
                 {
                     string folderName = form.FolderName;
                     if (string.IsNullOrWhiteSpace(folderName))
                     {
                         MessageBox.Show("Введите корректное имя папки.");
                         return;
                     }

                     string parentId = "root";
                     TreeNode parentNode = null;

                     if (listBox2.Items.Count == 1 && listBox2.Items[0] is CloudFile selected && selected.IsFolder)
                     {
                         parentId = selected.Id;
                         parentNode = FindNodeByCloudFile(treeView2.Nodes, selected);
                     }

                     try
                     {
                         var newFolder = await _cloudServiceRight.CreateFolderAsync(folderName, parentId);

                         TreeNode newNode = new TreeNode(newFolder.Name)
                         {
                             Tag = newFolder,
                             ImageKey = "folder-yellow",
                             SelectedImageKey = "folder-yellow"
                         };
                         newNode.Nodes.Add(new TreeNode()); // Lazy load

                         if (parentNode != null)
                         {
                             parentNode.Nodes.Add(newNode);
                             parentNode.Expand();
                         }
                         else
                         {
                             treeView2.Nodes.Add(newNode);
                         }

                         listBox2.Items.Clear();
                         MessageBox.Show("Папка успешно создана.");
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show("Ошибка при создании папки: " + ex.Message);
                     }
                 }
             }
         }*/

        /* private async void guna2Button6_Click_1(object sender, EventArgs e)
         {
             await DownloadAllFromListBox2Async();
         }*/

        /*private async void guna2Button7_Click_1(object sender, EventArgs e)
        {
            if (_cloudServiceRight != null)
                await DeleteSelectedItemsAsync(listBox2, treeView2, _cloudServiceRight);
            else
                MessageBox.Show("Удаление на локальном компьютере не поддерживается.");
        }*/

        private async void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (_cloudServiceRight == null)
            {
                MessageBox.Show("Выберите облако-получатель справа.");
                return;
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Список файлов слева пуст.");
                return;
            }

            if (listBox2.Items.Count > 1)
            {
                MessageBox.Show("Выберите одну папку назначения справа.");
                return;
            }

            string parentId = null;
            string parentPath = null;

            if (listBox2.Items.Count == 1 && listBox2.Items[0] is CloudFile dest && dest.IsFolder)
            {
                parentId = dest.Id;
                parentPath = dest.Path;
            }
            else
            {
                parentId = "root";
                parentPath = "";
            }
            // 🔧 ОБЯЗАТЕЛЬНО: если Dropbox и путь равен "root", заменяем его на пустую строку:
            if (_cloudServiceRight is DropboxService && (string.IsNullOrWhiteSpace(parentPath) || parentPath == "root"))
            {
                parentPath = ""; // Настоящий корень Dropbox
            }

            try
            {
                foreach (var item in listBox1.Items)
                {
                    if (item is string localPath && File.Exists(localPath))
                    {
                        bool encrypt = checkBoxEncryptUpload.Checked;
                        bool alreadyEncrypted = localPath.EndsWith(".enc", StringComparison.OrdinalIgnoreCase);

                        if (encrypt && alreadyEncrypted)
                            encrypt = false;  // Не шифруем повторно

                        if (_cloudServiceRight is GoogleDriveService gds)
                            await gds.UploadFileAsync(localPath, parentId, encrypt);
                        else if (_cloudServiceRight is DropboxService dbs)
                            await dbs.UploadFileAsync(localPath, parentPath, encrypt);
                        else
                            await _cloudServiceRight.UploadFileAsync(localPath, parentId);
                    }

                    else if (item is string folderPath && Directory.Exists(folderPath))
                    {
                        bool encrypt = checkBoxEncryptUpload.Checked;

                        if (_cloudServiceRight is GoogleDriveService gds)
                            await gds.UploadFolderAsync(folderPath, parentId, encrypt);
                        else if (_cloudServiceRight is DropboxService dbs)
                            await dbs.UploadFolderAsync(folderPath, parentPath, encrypt);
                        else if (_cloudServiceRight != null)
                            await _cloudServiceRight.UploadFolderAsync(folderPath, parentId); // если есть универсальная реализация
                    }

                    else if (item is CloudFile cf && cf.IsFolder)
                    {
                        await SyncCloudFolderToCloudAsync(cf, parentId);
                    }
                    else if (item is CloudFile file && !file.IsFolder && !_cloudServiceLeft.Equals(_cloudServiceRight))
                    {
                        long fileSizeThreshold = 104857600;

                        if (file.Size < fileSizeThreshold)
                        {
                            byte[] data = await _cloudServiceLeft.DownloadFileAsync(file.Id);

                            bool encrypt = checkBoxEncryptUpload.Checked;
                            string uploadName = file.Name;

                            if (encrypt && !uploadName.EndsWith(".enc", StringComparison.OrdinalIgnoreCase))
                            {
                                data = EncryptionHelper.EncryptData(data);
                                uploadName += ".enc";
                            }


                            using (var ms = new MemoryStream(data))
                            {
                                if (_cloudServiceRight is GoogleDriveService gds)
                                    await gds.UploadFileAsync(ms, uploadName, parentId);
                                else if (_cloudServiceRight is DropboxService dbs)
                                    await dbs.UploadFileAsync(ms, uploadName, parentPath);
                                else
                                    await _cloudServiceRight.UploadFileAsync(ms, uploadName, parentId);
                            }
                        }
                        else
                        {
                            using (var downloadStream = await _cloudServiceLeft.OpenDownloadStreamAsync(file.Id))
                            {
                                Stream uploadStream;
                                string uploadName = file.Name;

                                if (checkBoxEncryptUpload.Checked)
                                {
                                    var encryptor = EncryptionHelper.CreateStreamingEncryptor(out var iv);
                                    uploadStream = new CryptoStream(downloadStream, encryptor, CryptoStreamMode.Read);
                                    uploadName += ".enc";
                                }
                                else
                                {
                                    uploadStream = downloadStream;
                                }

                                await CopyStreamWithProgressAsync(uploadStream, _cloudServiceRight, uploadName, parentId, parentPath, file.Size, guna2ProgressBar1, guna2HtmlLabel1);
                            }
                        }
                    }
                }

                guna2ProgressBar1.Visible = false;
                guna2HtmlLabel1.Visible = false;

                MessageBox.Show("Загрузка завершена.");
                listBox1.Items.Clear();
                selectedFiles.Clear();

                if (guna2ComboBox2.SelectedItem is ConnectedAccount acc)
                    await PopulateCloudTreeAsync(acc, treeView2);
            }
            catch (Exception ex)
            {
                guna2ProgressBar1.Visible = false;
                guna2HtmlLabel1.Visible = false;
                MessageBox.Show("Ошибка при загрузке: " + ex.Message);
            }
        }


        /*private async void guna2Button9_Click_1(object sender, EventArgs e)
        {
            if (selectedFiles.Count > 0)
            {
                MessageBox.Show("Сначала очистите список слева, если хотите передавать справа.");
                return;
            }

            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Выберите файлы из источника справа для передачи.");
                return;
            }

            if (listBox1.Items.Count != 1)
            {
                MessageBox.Show("Выберите одну папку на компьютере как место назначения.");
                return;
            }

            string targetPath = listBox1.Items[0] as string;
            if (!Directory.Exists(targetPath))
            {
                MessageBox.Show("Выбранная папка на ПК не существует.");
                return;
            }

            try
            {
                foreach (var item in listBox2.Items)
                {
                    // --- Если источник — облако (CloudFile)
                    if (item is CloudFile cf)
                    {
                        await DownloadFromCloudAsync(cf, targetPath, _cloudServiceRight);
                    }
                    // --- Если источник — ПК (string path)
                    else if (item is string path)
                    {
                        if (File.Exists(path))
                        {
                            string destFile = Path.Combine(targetPath, Path.GetFileName(path));
                            File.Copy(path, destFile, overwrite: true);
                        }
                        else if (Directory.Exists(path))
                        {
                            CopyDirectory(path, targetPath);
                        }
                    }
                }

                MessageBox.Show("Файлы переданы на компьютер.");
                listBox2.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при передаче: " + ex.Message);
            }
        }*/

        private async Task<List<CloudFile>> ListFilesRecursiveAsync(ICloudService service, string parentId)
        {
            List<CloudFile> result = new List<CloudFile>();

            var currentLevel = await service.ListFilesAsync(parentId);

            foreach (var item in currentLevel)
            {
                result.Add(item);

                if (item.IsFolder)
                {
                    var subItems = await ListFilesRecursiveAsync(service, item.Id);
                    result.AddRange(subItems);
                }
            }

            return result;
        }



        private async Task SyncCloudFolderToCloudAsync(CloudFile sourceFolder, string destinationFolderId)
        {
            MessageBox.Show("Начинается передача файлов...");
            int uploadedCount = 0;
            int totalCount = await CountFilesRecursive(sourceFolder.Id, _cloudServiceLeft);
            if (_cloudServiceLeft == null || _cloudServiceRight == null)
                throw new InvalidOperationException("Сервисы не инициализированы.");

            // ✅ 1. Создаём корневую папку на целевом облаке
            CloudFile rootDestFolder = await _cloudServiceRight.CreateFolderAsync(sourceFolder.Name, destinationFolderId);
            string rootDestFolderId = rootDestFolder.Id;

            // ✅ 2. Запускаем очередь с новой папкой
            var queue = new Queue<(string sourceId, string destId)>();
            queue.Enqueue((sourceFolder.Id, rootDestFolderId));

            while (queue.Count > 0)
            {
                var (currentSourceId, currentDestId) = queue.Dequeue();

                var children = await _cloudServiceLeft.ListFilesAsync(currentSourceId);

                foreach (var item in children)
                {
                    if (item.IsFolder)
                    {
                        // ✅ Создаём папку на целевом облаке
                        CloudFile newFolder = await _cloudServiceRight.CreateFolderAsync(item.Name, currentDestId);

                        // ✅ Добавляем в очередь для рекурсивной обработки
                        queue.Enqueue((item.Id, newFolder.Id));
                    }
                    else
                    {
                        byte[] data = await _cloudServiceLeft.DownloadFileAsync(item.Id);

                        string fileName = item.Name;
                        bool alreadyEncrypted = fileName.EndsWith(".enc", StringComparison.OrdinalIgnoreCase);
                        bool encrypt = checkBoxEncryptUpload.Checked && !alreadyEncrypted;

                        if (encrypt)
                        {
                            data = EncryptionHelper.EncryptData(data);
                            fileName += ".enc";
                        }

                        using (var ms = new MemoryStream(data))
                        {
                            if (_cloudServiceRight is GoogleDriveService gds)
                                await gds.UploadFileAsync(ms, fileName, currentDestId);
                            else if (_cloudServiceRight is DropboxService dbs)
                                await dbs.UploadFileAsync(ms, fileName, currentDestId); // используем destId, а не item.Path
                            else
                                await _cloudServiceRight.UploadFileAsync(ms, fileName, currentDestId);
                        }
                        // 👇 Обновляем прогресс
                        uploadedCount++;
                        int percent = (int)((uploadedCount / (float)totalCount) * 100);
                        guna2ProgressBar1.Value = percent;
                        guna2HtmlLabel1.Text = $"Передано {uploadedCount} из {totalCount} файлов ({percent}%)";
                    }
                }
            }

            guna2HtmlLabel1.Text = "Передача завершена!";
        }

        private async Task<int> CountFilesRecursive(string folderId, ICloudService cloudService)
        {
            int count = 0;
            var children = await cloudService.ListFilesAsync(folderId);

            foreach (var item in children)
            {
                if (item.IsFolder)
                {
                    count += await CountFilesRecursive(item.Id, cloudService);
                }
                else
                {
                    count++;
                }
            }

            return count;
        }


    }
}
