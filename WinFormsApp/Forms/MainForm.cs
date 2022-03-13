using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
	public partial class MainForm : Form
	{
		private readonly AppDbContext dbContext = new();

		public MainForm()
		{
			InitializeComponent();

			AddColumns();
			AddRows();

			void AddColumns()
			{
				dataGridView1.Columns.Add("id", "Id");
				dataGridView1.Columns.Add("last_name", "Фамилия");
				dataGridView1.Columns.Add("first_name", "Имя");
				dataGridView1.Columns.Add("middle_name", "Отчество");
				dataGridView1.Columns.Add("date_of_birth", "Дата рождения");
			}
			void AddRows()
			{
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
			var form = new DeleteForm();
			form.ShowDialog();
		}

        private void button2_Click(object sender, EventArgs e)
        {
			var form = new ChangeForm();
			form.ShowDialog();
        }
    }
}
