using Database;
using Database.Models;
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

			var currentDate = DateTime.Now;
			AddColumns();

			AddRows();

			void AddColumns()
			{
				dataGridView1.Columns.Add("student", "ФИО");

				for (var i = -16; i <= 1; i++)
				{
					var date = currentDate.AddDays(i);
					dataGridView1.Columns.Add("student", date.ToString("dd.MM.yyyy"));
				}
			}

			void AddRows()
			{
				var schedules = dbContext.Schedules
					.Include(x => x.Student)
					.Where(x => x.DateOfLesson >= currentDate.AddDays(-14) && x.DateOfLesson <= currentDate.AddDays(1))
					.ToList();

				var students = new List<Student>();
				students = schedules.Select(x => x.Student).Distinct().ToList();

				foreach (var schedule in schedules)
				{

				}



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
