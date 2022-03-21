using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp.Interfaces;

namespace WinFormsApp.Controls
{
    public class ScheduleManager : IScheduleManager
    {
        private readonly short minRange;
        private readonly short maxRange;
        private readonly DateTime currentDate;

        public ScheduleManager(short minRange, short maxRange, DateTime currentDate)
        {
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.currentDate = currentDate;
        }

        public IEnumerable<DataGridViewColumn> GetColumns()
        {
            var columns = new List<DataGridViewColumn>
            {
                GetIdColumn(),
                GetFullNameColumn()
            };

            for (var i = minRange; i <= maxRange; i++)
            {
                var column = new DataGridViewCheckBoxColumn();
                {
                    var date = currentDate.AddDays(i);
                    column.Name = date.ToString("yyyy-MM-dd");
                    column.HeaderText = date.ToString("dd.MM.yyyy");
                    column.CellTemplate = new DataGridViewCheckBoxCell();
                }

                columns.Add(column);
            }

            return columns;

            DataGridViewColumn GetIdColumn()
            {
                var column = new DataGridViewColumn();
                column.Name = "id";
                column.HeaderText = "Id";
                column.CellTemplate = new DataGridViewTextBoxCell();

                return column;
            }

            DataGridViewColumn GetFullNameColumn()
            {
                var column = new DataGridViewColumn();
                column.Name = "full_name";
                column.HeaderText = "Фамилия Имя Отчество";
                column.CellTemplate = new DataGridViewTextBoxCell();

                return column;
            }
        }

        public IEnumerable<DataGridViewRow> GetRows(IEnumerable<Schedule> schedules, IEnumerable<Student> students)
        {
            var rows = new List<DataGridViewRow>();

            foreach (var student in students)
            {
                var row = new DataGridViewRow();
                row.Cells.AddRange(
                    GetStudentIdCell(),
                    GetStudentFullNameCell()
                );

                var schedule = schedules.Where(x => x.StudentId == student.Id);
                for (var i = minRange; i <= maxRange; i++)
                {
                    var date = currentDate.AddDays(i);
                    row.Cells.Add(
                        GetDateOfLessonCell(date)
                    );
                }

                rows.Add(row);

                DataGridViewCell GetStudentIdCell()
                {
                    return new DataGridViewTextBoxCell
                    {
                        Value = student.Id
                    };
                }

                DataGridViewCell GetStudentFullNameCell()
                {
                    return new DataGridViewTextBoxCell
                    {
                        Value = GetStudentFullName()
                    };

                    string GetStudentFullName()
                    {
                        return $"{student.LastName} {student.FirstName} {student.MiddleName}";
                    }
                }

                DataGridViewCell GetDateOfLessonCell(DateTime dateOfLesson)
                {
                    return new DataGridViewCheckBoxCell
                    {
                        Value = schedule.FirstOrDefault(x => x.DateOfLesson.Date == dateOfLesson.Date) != null
                    };
                }
            }

            return rows;
        }
    }
}
