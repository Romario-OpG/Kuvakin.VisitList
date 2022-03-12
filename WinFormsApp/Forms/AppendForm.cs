using Database;
using Database.Models;
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
	public partial class AppendForm : Form
	{
		private readonly AppDbContext dbContext = new();

		public AppendForm()
		{
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{

			string LastName;
			try
			{
				if (string.IsNullOrWhiteSpace(textBox1.Text)) { throw new ArgumentNullException(); }
				else { LastName = textBox1.Text; }
			}
			catch (ArgumentNullException)
			{
				MessageBox.Show("Введите Фамилию.");

				textBox1.Focus();
				return;
			}

			string FirstName;
			try
			{
				if (string.IsNullOrWhiteSpace(textBox2.Text)) { throw new ArgumentNullException(); }
				else { FirstName = textBox2.Text; }
			}
			catch (ArgumentNullException)
			{
				MessageBox.Show("Введите имя.");

				textBox2.Focus();
				return;
			}

			string MiddleName;
			try
			{
				if (string.IsNullOrWhiteSpace(textBox3.Text)) { throw new ArgumentNullException(); }
				else { MiddleName = textBox3.Text; }
			}
			catch (ArgumentNullException)
			{
				MessageBox.Show("Введите имя.");

				textBox3.Focus();
				return;
			}

			DateTime birthday;
			try
			{
				if (DateTime.Parse(textBox4.Text) >= DateTime.Now) { throw new FormatException(); }

				birthday = DateTime.Parse(textBox4.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Введите корректное значение даты рождения.");

				textBox4.Clear();
				textBox4.Focus();
				return;
			}

			var student = new Student()
			{
				LastName = LastName,
				FirstName = FirstName,
				MiddleName = MiddleName,
				DateOfBirth = birthday
			};

			await dbContext.Students.AddAsync(student);
			try
			{
				await dbContext.SaveChangesAsync();
			}
			catch (Exception)
			{
				MessageBox.Show("Ошибка при добавлении ученика.");
				return;
			}
			richTextBox1.Text += $"{DateTime.Now} Добавлен новый ученик: {LastName} {FirstName} {MiddleName}\n";
		}
	}
}
