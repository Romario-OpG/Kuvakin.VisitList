using Database;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            AddColumns();

            void AddColumns()
            {
                dataGridView1.Columns.Add("id", "Id");
                dataGridView1.Columns.Add("last_name", "Фамилия");
                dataGridView1.Columns.Add("first_name", "Имя");
                dataGridView1.Columns.Add("middle_name", "Отчество");
                dataGridView1.Columns.Add("date_of_birth", "Дата рождения");
            }

            UpdateRows();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new ScheduleForm();

            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AppendForm();

            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var studentId = GetActiveStudentId();

            var form = new DeleteForm(studentId);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var studentId = GetActiveStudentId();

            var form = new ChangeForm(studentId);
            form.ShowDialog();
        }

        private short GetActiveStudentId()
        {
            var currentRow = dataGridView1.CurrentRow;

            var studentId = (short)currentRow.Cells["id"].Value;
            return studentId;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Приложение для учета посещаемости помогает участникам образовательной ситемы удобнее следить за посещаемостью обучающихся");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateRows();
        }

        public void UpdateRows()
        {
            dataGridView1.Rows.Clear();

            AppDbContext dbContext = new();

            var students = dbContext.Students.ToList();
            foreach (var student in students)
            {
                var values = new object[]
                {
                        student.Id,
                        student.LastName,
                        student.FirstName,
                        student.MiddleName,
                        student.DateOfBirth.ToString("dd.MM.yyyy")
                };

                dataGridView1.Rows.Add(values);
            }
        }
    }
}
