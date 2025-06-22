using System;
using System.Windows.Forms;

namespace VKR
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void OpenFormInPanel(Form childForm)
        {

            if (panel3.Controls.Count > 0)
            {
                var oldForm = panel3.Controls[0] as Form;
                if (oldForm != null)
                {
                    oldForm.Close();  // Закроет форму
                    oldForm.Dispose(); // Удалит форму из памяти
                }
            }

            panel3.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel3.Controls.Add(childForm);
            childForm.Show();
            /*panel3.Controls.Clear(); // Очистить старую форму, если была
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Font = this.Font;
            childForm.AutoScaleMode = AutoScaleMode.None;
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.Show();*/
        }
        private void LoadFormIntoPanel(Form form)
        {
            panel3.Controls.Clear(); // Очистка панели

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;


   

            // Возвращение крестика после закрытия формы
            form.FormClosed += (s, e) =>
            {
                RestoreDefaultPanelContent();
            };

            panel3.Controls.Add(form);
            form.Show();
        }

        // Восстановление панели после закрытия вложенной формы
        private void RestoreDefaultPanelContent()
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(guna2CircleButton1); // Добавляем кнопку закрытия
            guna2CircleButton1.BringToFront(); // Поверх остальных элементов
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form4 authForm = new Form4();
            LoadFormIntoPanel(authForm);
        }





        private void guna2Button2_Click(object sender, EventArgs e)
        {
            /*OpenFormInPanel(new Form6(this));*/

            Form7 CloudDowhload = new Form7(this);  // передаём ссылку на Form2
            LoadFormIntoPanel(CloudDowhload);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Form8 syncForm = new Form8(this);
            OpenFormInPanel(syncForm);

        }
    }
}
