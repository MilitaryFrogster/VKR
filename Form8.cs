using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR
{
    public partial class Form8 : Form
    {
        private ICloudService _sourceCloudService;
        private ICloudService _targetCloudService;
        private bool _comboInitialized = false;
        private List<object> allAccounts; // содержит ConnectedAccount и строку "Компьютер"
        private ICloudService _cloudServiceLeft, _cloudServiceRight;
        private List<string> selectedFiles = new List<string>();
        private Form2 _parent;

        public Form8(Form2 parent)
        {
            InitializeComponent();
            Load += Form8_Load;
            _parent = parent;

        }

        private async void Form8_Load(object sender, EventArgs e)
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
            treeView1.ImageList = imageList2;
            treeView2.ImageList = imageList2;

            // Icons
            if (!imageList2.Images.ContainsKey("folder"))
                imageList2.Images.Add("folder", GetFileIcon("C:\\Windows"));

            if (!imageList2.Images.ContainsKey("folder-yellow"))
            {
                string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "folder-yellow.ico");
                imageList2.Images.Add("folder-yellow", new Icon(iconPath));
            }
            if (!imageList2.Images.ContainsKey("drive"))
            {
                string driveIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "drive.ico");
                imageList2.Images.Add("drive", new Icon(driveIconPath));
            }
            if (!imageList2.Images.ContainsKey("computer"))
            {
                string compIconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "computer.ico");
                imageList2.Images.Add("computer", new Icon(compIconPath));
            }
        }

        private void AddDefaultIcons()
        {
            if (!imageList2.Images.ContainsKey("folder-yellow"))
                imageList2.Images.Add("folder-yellow", new Icon("Assets/folder-yellow.ico"));


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

        private async Task<ICloudService> GetCloudServiceAsync(ConnectedAccount acc)
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
            throw new NotSupportedException("Неподдерживаемое облако");
        }

        private async Task PopulateCloudTreeAsync(ICloudService service, string parentId, TreeNode parentNode, TreeView tree)
        {
            if (parentNode == null)
                tree.Nodes.Clear();

            var list = await service.ListFilesAsync(parentId);
            foreach (var cf in list)
            {
                string displayName = cf.Name.EndsWith(".enc") ? cf.Name + " (зашифрован)" : cf.Name;
                var node = new TreeNode(displayName)
                {
                    Tag = cf
                };

                if (cf.IsFolder)
                {
                    node.ImageKey = "folder-yellow";
                    node.SelectedImageKey = "folder-yellow";
                    node.Nodes.Add(new TreeNode());
                }
                else
                {
                    string ext = Path.GetExtension(cf.Name).ToLower();
                    if (!imageList2.Images.ContainsKey(ext))
                        imageList2.Images.Add(ext, GetFileIcon("dummy" + ext));

                    node.ImageKey = ext;
                    node.SelectedImageKey = ext;
                }

                if (parentNode == null)
                    tree.Nodes.Add(node);
                else
                    parentNode.Nodes.Add(node);
            }

            tree.BeforeExpand += async (s, e) =>
            {
                var n = e.Node;
                if (n.Tag is CloudFile cf && cf.IsFolder && n.Nodes.Count == 1 && n.Nodes[0].Tag == null)
                {
                    try
                    {
                        n.Nodes.Clear();
                        await PopulateCloudTreeAsync(service, cf.Id, n, tree);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при раскрытии узла: {ex.Message}");
                    }
                }
            };
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is CloudFile cf && !listBox1.Items.Cast<CloudFile>().Any(x => x.Id == cf.Id))
                listBox1.Items.Add(cf);
        }

        private void TreeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is CloudFile cf && !listBox2.Items.Cast<CloudFile>().Any(x => x.Id == cf.Id))
                listBox2.Items.Add(cf);
        }

        [DllImport("shell32.dll")]
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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private async void Guna2Button7_Click(object sender, EventArgs e)
        {
            if (_sourceCloudService == null || _targetCloudService == null)
            {
                MessageBox.Show("Оба облака должны быть выбраны.");
                return;
            }

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Сначала выберите файлы для передачи.");
                return;
            }

            string targetParentId = "root";
            if (listBox2.Items.Count == 1 && listBox2.Items[0] is CloudFile folder && folder.IsFolder)
                targetParentId = folder.Id;

            bool encryptFlag = checkBoxEncryptUpload.Checked;

            try
            {
                foreach (var item in listBox1.Items)
                {
                    if (item is CloudFile cf)
                    {
                        bool isEncrypted = cf.Name.EndsWith(".enc");

                        if (encryptFlag && !isEncrypted)
                        {
                            // 🧷 Шифруем → передаём
                            byte[] raw = await _sourceCloudService.DownloadFileAsync(cf.Id);
                            byte[] encrypted = EncryptionHelper.EncryptBytes(raw);
                            using (var stream = new MemoryStream(encrypted))
                            {
                                string encryptedName = cf.Name + ".enc";
                                await _targetCloudService.UploadFileAsync(stream, encryptedName, targetParentId);
                            }
                        }
                        else
                        {
                            // 🔁 Передаём как есть (напрямую через память)
                            byte[] data = await _sourceCloudService.DownloadFileAsync(cf.Id);
                            using (var stream = new MemoryStream(data))
                            {
                                await _targetCloudService.UploadFileAsync(stream, cf.Name, targetParentId);
                            }
                        }
                    }
                }

                MessageBox.Show("Передача завершена.");
                listBox1.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при передаче: " + ex.Message);
            }
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
                guna2VProgressBar1.Visible = true;
                guna2HtmlLabel2.Visible = true;
                guna2VProgressBar1.Minimum = 0;
                guna2VProgressBar1.Maximum = 100;
                guna2VProgressBar1.Value = 0;
                guna2HtmlLabel2.Text = "Подготовка...";

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
                guna2VProgressBar1.Visible = false;
                guna2HtmlLabel2.Visible = false;
            }
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

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Принудительно повторно установить ImageKey при выборе
            if (e.Node.ImageKey != null)
                e.Node.SelectedImageKey = e.Node.ImageKey;
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
                    TreeNode childNode = new TreeNode(child.Name)
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
                        if (!imageList2.Images.ContainsKey(ext))
                            imageList2.Images.Add(ext, GetFileIcon("dummy" + ext));
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

            guna2VProgressBar1.Invoke((Action)(() =>
            {
                guna2VProgressBar1.Minimum = 0;
                guna2VProgressBar1.Maximum = totalFiles;
                guna2VProgressBar1.Value = 0;
            }));

            // Загружаем недостающие файлы
            foreach (var fileName in filesToUpload)
            {
                currentIndex++;

                guna2HtmlLabel2.Invoke((Action)(() =>
                {
                    guna2HtmlLabel2.Text = $"Передача: {currentIndex} / {totalFiles} — {fileName}";
                }));

                guna2VProgressBar1.Invoke((Action)(() =>
                {
                    guna2VProgressBar1.Value = currentIndex;
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
            guna2HtmlLabel2.Invoke((Action)(() =>
            {
                guna2HtmlLabel2.Text = "Синхронизация завершена.";
            }));

            guna2VProgressBar1.Invoke((Action)(() =>
            {
                guna2VProgressBar1.Value = totalFiles;
            }));
        }

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

        private void AddNodeWithIcon(TreeNodeCollection parent, string fullPath)
        {
            string key = Path.GetExtension(fullPath).ToLower();

            if (!imageList2.Images.ContainsKey(key))
            {
                Icon icon = GetFileIcon(fullPath);
                imageList2.Images.Add(key, icon);
            }

            TreeNode node = new TreeNode(Path.GetFileName(fullPath));
            node.ImageKey = key;
            node.SelectedImageKey = key;
            node.Tag = fullPath;
            parent.Add(node);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2ButtonToTasks_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(_parent);
            _parent.OpenFormInPanel(form9);
        }

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
                    TreeNode node = new TreeNode(cf.Name)
                    {
                        Tag = cf
                    };

                    if (cf.IsFolder)
                    {
                        node.ImageKey = "folder-yellow";
                        node.SelectedImageKey = "folder-yellow";
                        node.Nodes.Add(new TreeNode());
                    }
                    else
                    {
                        string ext = Path.GetExtension(cf.Name).ToLower();
                        if (!imageList2.Images.ContainsKey(ext))
                            imageList2.Images.Add(ext, GetFileIcon("dummy" + ext));
                        node.ImageKey = ext;
                        node.SelectedImageKey = ext;
                    }

                    tree.Nodes.Add(node);
                }

                tree.BeforeExpand -= Cloud_BeforeExpand;
                tree.BeforeExpand += Cloud_BeforeExpand;
            }
        }

    }
}