using System;
using System.Linq;
using System.Windows.Forms;

namespace VKR
{
    public partial class Form11 : Form
    {
        private Form4 parentForm;

        public Form11(Form4 parent)
        {
            InitializeComponent();
            this.parentForm = parent;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                var (driveService, email) = await GoogleDriveAuth.AuthorizeAndGetDriveServiceAsync();

                var existing = AccountStorage.LoadAccounts();
                if (existing.Any(a => a.Email == email && a.Cloud == "Google Drive"))
                {
                    MessageBox.Show("Этот аккаунт уже подключён.");
                    this.Close();
                    return;
                }

                string capacity = await GoogleDriveAuth.GetDriveCapacityAsync(driveService);

                var acc = new ConnectedAccount
                {
                    Email = email,
                    Cloud = "Google Drive",
                    Capacity = capacity
                };
                AccountStorage.Add(acc);

                parentForm.RefreshTable();
                MessageBox.Show("Аккаунт успешно подключён.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка авторизации Google Drive:\n" + ex.Message);
            }
        }

        private async void guna2PictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {
                var (accessToken, email, capacity) = await DropboxAuth.FullAuthorizeAsync();

                var existing = AccountStorage.LoadAccounts();
                if (existing.Any(a => a.Email == email && a.Cloud == "Dropbox"))
                {
                    MessageBox.Show("Этот Dropbox-аккаунт уже подключён.");
                    this.Close();
                    return;
                }

                var acc = new ConnectedAccount
                {
                    Email = email,
                    Cloud = "Dropbox",
                    Capacity = capacity,
                    AccessToken = accessToken
                };
                AccountStorage.Add(acc);

                parentForm.RefreshTable();
                this.Close();
                MessageBox.Show("Dropbox-аккаунт успешно подключён.");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении Dropbox:\n" + ex.Message);
            }
        }

        private void guna2PictureBox3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Авторизация OneDrive пока не реализована.");
        }
    }
}
