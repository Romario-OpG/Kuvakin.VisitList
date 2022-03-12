using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
	public partial class ScheduleForm : Form
	{
		private readonly AppDbContext dbContext = new();

		private readonly DateTime currentDate = DateTime.Now;

		private const int MinRange = -14; // 2 недели от текущей даты
		private const int MaxRange = 1; // 1 день от текущей даты
		private const int SkipColumns = 1; // кол-во пропускаемых колонок

		public ScheduleForm()
		{
			InitializeComponent();

			AddColumns();

			AddRows();
		}

		private void AddColumns()
		{
			dataGridView1.Columns.Add("student", "ФИО");

			for (var i = MinRange; i <= MaxRange; i++)
			{
				var date = currentDate.AddDays(i);
				dataGridView1.Columns.Add($"day[i]", date.ToString("dd.MM.yyyy"));
			}
		}

		private void AddRows()
		{
			var schedules = dbContext.Schedules
				.Include(x => x.Student)
				.Where(x => x.DateOfLesson >= currentDate.AddDays(MinRange) && x.DateOfLesson <= currentDate.AddDays(MaxRange))
				.OrderBy(x => x.DateOfLesson)
				.ToList();

			var students = schedules.Select(x => x.Student).Distinct().ToList();
			foreach (var student in students)
			{
				var values = new List<object>()
				{
					$"Жмых пожiлой {student.ToFullName()}"
				};

				var schedule = schedules.Where(x => x.StudentId == student.Id);
				for (var i = MinRange; i <= MaxRange; i++)
				{
					var date = currentDate.AddDays(i);
					if (schedule.FirstOrDefault(x => x.DateOfLesson.Date == date.Date) != null)
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
