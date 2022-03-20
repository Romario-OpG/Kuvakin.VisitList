using Database;
using Database.Models;
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

            /*AddColumns();

            var schedules = await dbContext.Schedules
                .Where(x =>
                    x.DateOfLesson.Date >= currentDate.AddDays(minRange).Date &&
                    x.DateOfLesson.Date <= currentDate.AddDays(maxRange).Date)
                .OrderBy(x => x.DateOfLesson)
                .ToListAsync();

            var students = await dbContext.Students.ToListAsync();

            AddRows();*/

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
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
