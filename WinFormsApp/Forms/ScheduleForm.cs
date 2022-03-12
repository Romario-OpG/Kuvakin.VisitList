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
				var schedules = dbContext.Schedules
					.Include(x => x.Student)
					.ToList();

				foreach (var schedule in schedules)
				{
					var FIO = $"{schedule.Student.LastName} {schedule.Student.FirstName} {schedule.Student.MiddleName}";

					var values = new object[]
					{
						FIO
					};

					dataGridView1.Rows.Add(values);
				}
			}
		}
	}
}
