using Google.Apis.Drive.v3;
using System;
using System.Windows.Forms;

namespace VKR
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            // ОБЯЗАТЕЛЬНО для вставки в panel3:
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            Load += Form4_Load;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            var f = new Form11(this);
            f.ShowDialog();
        }

        public void RefreshTable()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (var acc in AccountStorage.LoadAccounts())
            {
                guna2DataGridView1.Rows.Add(
                    acc.Email,
                    acc.Cloud,
                    acc.Capacity
                );
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            using (var confirm = new FormExitConfirm())
            {
                if (confirm.ShowDialog() == DialogResult.Yes)
                {
                    Application.Exit(); // или this.Close()
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите аккаунт для удаления.");
                return;
            }

            var selectedRow = guna2DataGridView1.SelectedRows[0];

            // Получаем данные из выбранной строки
            string email = selectedRow.Cells[0].Value?.ToString();
            string provider = selectedRow.Cells[1].Value?.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(provider))
            {
                MessageBox.Show("Не удалось получить данные аккаунта.");
                return;
            }

            // Подтверждение удаления
            var result = MessageBox.Show($"Удалить аккаунт {email} ({provider})?", "Подтверждение удаления", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1️⃣ Удаляем из accounts.json
                    var accounts = AccountStorage.LoadAccounts();
                    accounts.RemoveAll(a => a.Email == email && a.Cloud == provider);
                    AccountStorage.SaveAccounts(accounts);

                    // 2️⃣ Если Google Drive — дополнительно удалить токены
                    if (provider == "Google Drive")
                    {
                        string tokenFolder = System.IO.Path.Combine("googletokens", email);
                        if (System.IO.Directory.Exists(tokenFolder))
                        {
                            System.IO.Directory.Delete(tokenFolder, true);
                        }
                    }

                    // 3️⃣ Обновляем таблицу
                    RefreshTable();

                    MessageBox.Show("Аккаунт успешно удалён.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message);
                }
            }
        }
    }
}