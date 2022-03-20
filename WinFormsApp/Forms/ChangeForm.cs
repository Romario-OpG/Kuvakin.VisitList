using Database;
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
            dateTimePicker1.Value = student.DateOfBirth;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            TextBoxModel textBoxModel = new()
            {
                LastName = textBox3,
                FirstName = textBox2,
                MiddleName = textBox5,
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
                var student = dbContext.Students.FirstOrDefault(x => x.Id == studentId);
                student.LastName = textBoxModel.LastName.Text;
                student.FirstName = textBoxModel.FirstName.Text;
                student.MiddleName = textBoxModel.MiddleName.Text;
                student.DateOfBirth = dateTimePicker1.Value.Date;

                await dbContext.SaveChangesAsync();

                MessageBox.Show("Изменение прошло успешно");
            }
        }
    }
}
