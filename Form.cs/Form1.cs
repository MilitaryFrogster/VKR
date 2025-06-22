using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string passphrase = guna2TextBox1.Text;

            if (string.IsNullOrWhiteSpace(passphrase))
            {
                MessageBox.Show("Введите парольную фразу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            if (string.IsNullOrWhiteSpace(passphrase))
            {
                MessageBox.Show("Введите парольную фразу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                KeyStorage.InitializeKeyFromPassword(passphrase);
                MessageBox.Show("Ключ успешно создан", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form2 mainForm = new Form2();
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

       
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
