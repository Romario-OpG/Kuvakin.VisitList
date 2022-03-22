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

            AddColumns();

            AddRows();

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            short id = short.Parse(dataGridView1[0, e.RowIndex].Value.ToString());

            bool value;
            try
            {
                if(bool.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString()) == false){ value = false; }
                else { value = true; }
            }
            catch (Exception)
            {
                dataGridView1.Rows.Clear();
                AddRows();

                MessageBox.Show("Не изменяйте значение посещаемости вручную");

                return;
            }

            if (value == false)
            {
                var schedule = dbContext.Schedules.Where(x => x.StudentId == id && x.DateOfLesson.Date == currentDate.AddDays(e.ColumnIndex - 17).Date);
                try
                {
                    dbContext.Schedules.RemoveRange(schedule);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось сделать запись в базу данных");

                    return;
                }
            }
            else
            {
                var shedule = new Schedule()
                {
                    Student = dbContext.Students.FirstOrDefault(x => x.Id == id),
                    StudentId = dbContext.Students.FirstOrDefault(x => x.Id == id).Id,
                    DateOfLesson = currentDate.AddDays(e.ColumnIndex - 17).Date
                };

                try
                {
                    await dbContext.Schedules.AddAsync(shedule);
                    await dbContext.SaveChangesAsync();

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Не удалось сделать запись в базу данных");

                    return;
                }
            }
        }



        private void AddColumns()
        {
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns.Add("student", "ФИО");
            dataGridView1.Columns.Add("proportion", "Посещаемость");

            for (var i = MinRange; i <= MaxRange; i++)
            {
                var date = currentDate.AddDays(i);

                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                {
                    column.CellTemplate = new DataGridViewCheckBoxCell();
                    column.HeaderText = date.ToString("dd.MM.yyyy");
                }

                dataGridView1.Columns.Add(column);
            }
        }

        private async void AddRows()
        {
            double proportion = 0;

            var schedules = await dbContext.Schedules
           .Where(x => x.DateOfLesson.Date >= currentDate.AddDays(MinRange).Date && x.DateOfLesson.Date <= currentDate.AddDays(MaxRange).Date)
           .OrderBy(x => x.DateOfLesson)
           .ToListAsync();

            var students = dbContext.Students.ToList();
            foreach (var student in students)
            {
                var values = new List<object>()
                {
                    student.Id,
                    student.ToFullName()
                };

                var schedule = schedules.Where(x => x.StudentId == student.Id);
                for (var i = MinRange; i <= MaxRange; i++)
                {
                    var date = currentDate.AddDays(i);
                    if (schedule.FirstOrDefault(x => x.DateOfLesson.Date == date.Date) != null)
                    {
                        proportion++;
                    }
                    else
                    {
                    }
                }
                values.Add($"{proportion / 16 * 100}%");

                for (var i = MinRange; i <= MaxRange; i++)
                {
                    var date = currentDate.AddDays(i);
                    if (schedule.FirstOrDefault(x => x.DateOfLesson.Date == date.Date) != null)
                    {
                        values.Add(true);
                    }
                    else
                    {
                        values.Add(false);
                    }
                }

                dataGridView1.Rows.Add(values.ToArray());
                proportion = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            AddRows();
        }
    }
}
