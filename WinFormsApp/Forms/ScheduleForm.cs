using Database;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp.Controls;

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

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            // new code:
            var schedules = dbContext.Schedules
                .Where(x =>
                    x.DateOfLesson.Date >= currentDate.AddDays(MinRange).Date &&
                    x.DateOfLesson.Date <= currentDate.AddDays(MaxRange).Date)
                .OrderBy(x => x.DateOfLesson)
                .ToList();

            var students = dbContext.Students.ToList();

            var scheduleGridView = new ScheduleGridView(MinRange, MaxRange, currentDate);
            
            var columns = scheduleGridView.GetColumns();
            dataGridView1.Columns.AddRange(columns.ToArray());

            var rows = scheduleGridView.GetRows(schedules, students);
            dataGridView1.Rows.AddRange(rows.ToArray());
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs eventArgs)
        {
            MessageBox.Show(GetStudentId().ToString());


            /*var schedule = new Schedule()
            {
                Student = dbContext.Students.FirstOrDefault(x => x.LastName == s[0] && x.FirstName == s[1] && x.MiddleName == s[2]),
                StudentId = dbContext.Students.FirstOrDefault(x => x.LastName == s[0] && x.FirstName == s[1] && x.MiddleName == s[2]).Id,
                DateOfLesson = currentDate.AddDays(e.ColumnIndex - 15).Date
            };

            try
            {
                await dbContext.Schedules.AddAsync(schedule);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сделать запись в базу данных");

                return;
            }*/

            short GetStudentId()
            {
                var studentId = (short)dataGridView1["id", eventArgs.RowIndex].Value;
                return studentId;
            }
        }
    }
}
