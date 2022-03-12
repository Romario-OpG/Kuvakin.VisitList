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
	public partial class ScheduleForm : Form
	{
		private readonly AppDbContext dbContext = new();

		public ScheduleForm()
		{
			InitializeComponent();

			AddColumns();

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
		}
	}
}
