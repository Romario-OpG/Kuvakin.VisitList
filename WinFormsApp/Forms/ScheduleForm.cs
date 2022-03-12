using Database;
using Microsoft.EntityFrameworkCore;
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
	public partial class ScheduleForm : Form
	{
		private readonly AppDbContext dbContext = new();

		public ScheduleForm()
		{
			InitializeComponent();

			AddColumns();

			AddRows();

			void AddColumns()
			{
				dataGridView1.Columns.Add("student", "ФИО");

				var currentDate = DateTime.Now;
				for (var i = -16; i <= 1; i++)
				{
					var date = currentDate.AddDays(i);
					dataGridView1.Columns.Add("student", date.ToString("dd.MM.yyyy"));
				}
			}

			void AddRows()
			{
				var schedules = dbContext.Schedules.ToList();

				var students = dbContext.Students.ToList();

				foreach(var student in students)
				{
					var fio = $"{student.LastName} {student.FirstName} {student.MiddleName}";

					var values = new List<object>()
					{
						fio
					};

					foreach(var schedul in schedules)
					{
						if(schedul.StudentId == student.Id)
						{
							values.Add("Да");
						}
						else
						{
							values.Add("Нет");
						}
					}

					dataGridView1.Rows.Add(values.ToArray());
				}
			}
		}
	}
}
