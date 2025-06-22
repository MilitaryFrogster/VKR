using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VKR;



namespace VKR
{

    
    public partial class Form9 : Form
    
    {
        private Form2 parentForm;

        public Form9(Form2 parent)
        {
            InitializeComponent();
            this.parentForm = parent;
            this.guna2ButtonBack.Click += new System.EventHandler(this.guna2ButtonBack_Click);
           
            this.Load += Form9_Load;



        }
        private void Form9_Load(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Columns.Count == 0)
            {
                guna2DataGridView1.Columns.Add("Name", "Имя");
                guna2DataGridView1.Columns.Add("Source", "Источник");
                guna2DataGridView1.Columns.Add("Destination", "Назначение");
                guna2DataGridView1.Columns.Add("Schedule", "Расписание");
                guna2DataGridView1.Columns.Add("NextRunIn", "До запуска");
                guna2DataGridView1.Columns.Add("Encryption", "Шифрование");
            }

            SyncApp.TaskManager.LoadAllTasks();
            LoadTasks();
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

       

        private void layoutRoot_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ButtonCreate_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(parentForm); // если Form10 тоже принимает ссылку
            parentForm.OpenFormInPanel(form10);
        }
        public void LoadTasks()
        {
            var tasks = SyncApp.TaskManager.GetTasks();
            guna2DataGridView1.Rows.Clear();
            foreach (var t in tasks)
            {
                guna2DataGridView1.Rows.Add(
                    t.Name,
                    PathUtils.FormatPath(t.SourcePath),
                    PathUtils.FormatPath(t.DestinationPath),
                    t.Schedule,
                    (t.NextRun - DateTime.Now).ToString(@"hh\:mm"),
                    t.UseEncryption ? "✓" : ""
                    );

            }
        }

        private void guna2ButtonDelete_Click(object sender, EventArgs e)
        {

            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                var name = guna2DataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    var result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить задачу \"{name}\"?",
                        "Подтверждение удаления",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Удаляем из менеджера
                        SyncApp.TaskManager.RemoveTask(name);

                        // Обновляем таблицу
                        LoadTasks();
                    }
                }
            }
        }

        private void guna2ButtonBack_Click(object sender, EventArgs e)
        {
            parentForm.OpenFormInPanel(new Form8(parentForm));
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoadTasks(); // всегда при показе формы
        }

    }
}
