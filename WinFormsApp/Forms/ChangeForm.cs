﻿using Database;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp.Forms
{
    public partial class ChangeForm : Form
    {
        private readonly AppDbContext dbContext = new();

        private readonly short studentId;

        public ChangeForm(short studentId)
        {
            InitializeComponent();

            this.studentId = studentId;

            var student = dbContext.Students.FirstOrDefault(x => x.Id == studentId);

            textBox3.Text = student.LastName;
            textBox2.Text = student.FirstName;
            textBox5.Text = student.MiddleName;
            textBox1.Text = student.DateOfBirth.ToString("dd.MM.yyyy");

            comboBox1.DataSource = dbContext.Students.ToList();
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "Id";

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // исправить под fluent validation

            string LastName;
            try
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text)) { throw new ArgumentNullException(); }
                else { LastName = textBox3.Text; }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Введите Фамилию.");

                textBox3.Focus();
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
                if (string.IsNullOrWhiteSpace(textBox5.Text)) { throw new ArgumentNullException(); }
                else { MiddleName = textBox5.Text; }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Введите имя.");

                textBox5.Focus();
                return;
            }

            DateTime birthday;
            try
            {
                if (DateTime.Parse(textBox1.Text) >= DateTime.Now) { throw new FormatException(); }

                birthday = DateTime.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректное значение даты рождения.");

                textBox1.Clear();
                textBox1.Focus();
                return;
            }

            /*
            var student = dbContext.Students.FirstOrDefault(x => x.Id == id);
            student.LastName = LastName;
            student.FirstName = FirstName;
            student.MiddleName = MiddleName;
            student.DateOfBirth = birthday;
            */

            await dbContext.SaveChangesAsync();
        }
    }
}
