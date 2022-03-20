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
            TextBoxModel textBoxModel = new()
            {
                LastName = textBox1,
                FirstName = textBox2,
                MiddleName = textBox3,
            };

            TextBoxValidator validator = new();
            if (validator.Validate(textBoxModel).IsValid == false)
            {
                string errors = "";
                foreach (var eror in validator.Validate(textBoxModel).Errors.Select(x => x.ErrorMessage))
                {
                    errors += eror;
                }
                MessageBox.Show(errors);

                return;
            }
            else
            {
                var student = new Student()
                {
                    LastName = textBoxModel.LastName.Text,
                    FirstName = textBoxModel.FirstName.Text,
                    MiddleName = textBoxModel.MiddleName.Text,
                    DateOfBirth = dateTimePicker1.Value.Date
                };

                try
                {
                    await dbContext.Students.AddAsync(student);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось добавить ученика");
                }

                richTextBox1.Text += $"{DateTime.Now} Добавлен новый ученик: {textBoxModel.LastName.Text} {textBoxModel.FirstName.Text} {textBoxModel.MiddleName.Text}\n";

                var form = new MainForm();
                form.UpdateRows();
            }
        }
    }
}
