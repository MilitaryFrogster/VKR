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
    public partial class Form6 : Form
    {
        private Form2 mainForm;
        public Form6(Form2 form)
        {
            InitializeComponent();
            this.mainForm = form;

            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(mainForm); // передаём ссылку на Form2
            mainForm.OpenFormInPanel(form7);
        /*    Form7 form7 = new Form7(mainForm);
            form7.Show();*/

        }


        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           /* Form8 form8 = new Form8(); // без параметров
            form8.Show();*/
        }
    }
}