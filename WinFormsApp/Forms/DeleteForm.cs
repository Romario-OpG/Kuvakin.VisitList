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
	public partial class DeleteForm : Form
	{
		private readonly AppDbContext dbContext = new();

		private readonly short studentId;

		public DeleteForm(short studentId)
		{
			this.studentId = studentId;

			InitializeComponent();

			var student = dbContext.Students.FirstOrDefault(x => x.Id == this.studentId);
			richTextBox1.Text += $"Удалить ({student.LastName} {student.FirstName} {student.MiddleName} {student.DateOfBirth.Date})?\n";
		}

        private async void button1_Click(object sender, EventArgs e)
        {
			var student = dbContext.Students.FirstOrDefault(x => x.Id == this.studentId);

            try
            {
				dbContext.Students.Remove(student);
				await dbContext.SaveChangesAsync();

				richTextBox1.Text += $"({student.LastName} {student.FirstName} {student.MiddleName} {student.DateOfBirth.Date}) Удалён {DateTime.Now}\n";
			}
            catch (Exception)
            {
				MessageBox.Show("Не удалось удалить учащегося");

				return;
            }
		}
    }
}
