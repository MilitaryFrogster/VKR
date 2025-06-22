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
    
    public partial class Form10 : Form
    {
        private Form2 parentForm;
        private string selectedSourcePath;
        private string selectedDestinationPath;
       
        private string selectedSourceAbsolutePath; // полный путь

       
        private string selectedDestinationAbsolutePath;




        public Form10(Form2 parent)
        {
            InitializeComponent();
            this.parentForm = parent;
            this.Load += new System.EventHandler(this.Form10_Load);


        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            comboSchedule.Items.Clear();
            comboSchedule.Items.AddRange(new string[]
            {
        "Каждый час",
        "Каждые 3 часа",
        "Каждый день",
        "Каждую неделю",
        "Каждое первое число месяца"
            });

            comboSchedule.SelectedIndex = 0; // по умолчанию первый вариант
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {

            var task = new SyncTask
            {
                Name = guna2TextBox1.Text,
                SourcePath = selectedSourcePath,
                SourceAbsolutePath = selectedSourceAbsolutePath,
                DestinationPath = selectedDestinationPath,
                DestinationAbsolutePath = selectedDestinationAbsolutePath,
                Schedule = comboSchedule.SelectedItem?.ToString(),
                UseEncryption = checkBoxEncrypt.Checked,
                LastRun = DateTime.Now
            };

            await SyncEngine.ExecuteTaskAsync(task);

            SyncApp.TaskManager.AddTask(task); // глобальный менеджер
            

            parentForm.OpenFormInPanel(new Form9(parentForm));
        }

        private void buttonFrom_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderSelectionForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedSourcePath = dlg.SelectedPath; // отображаемый путь (Компьютер:...)
                    selectedSourceAbsolutePath = dlg.SelectedAbsolutePath; // полный путь на диске
                    buttonFrom.Text = selectedSourcePath;
                }
            }
        }

        private void buttonTo_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderSelectionForm())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedDestinationPath = dlg.SelectedPath;
                    selectedDestinationAbsolutePath = dlg.SelectedAbsolutePath;
                    buttonTo.Text = selectedDestinationPath;
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            parentForm.OpenFormInPanel(new Form9(parentForm));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            using (var confirm = new FormExitConfirm())
            {
                if (confirm.ShowDialog() == DialogResult.Yes)
                {
                    Application.Exit(); // или this.Close()
                }
            }
        }
    }
}
