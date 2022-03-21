using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Interfaces;

namespace WinFormsApp.Controls
{
    public class ScheduleDataGrid : IScheduleDataGrid
    {
        private readonly short minRange;
        private readonly short maxRange;
        private readonly DateTime currentDate;
        private readonly AppDbContext dbContext;

        private readonly IEnumerable<Schedule> schedules;
        private readonly IEnumerable<Student> students;

        public ScheduleDataGrid(short minRange, short maxRange, DateTime currentDate, AppDbContext dbContext)
        {
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.currentDate = currentDate;
            this.dbContext = dbContext;

            this.schedules = dbContext.Schedules
                .Where(x =>
                    x.DateOfLesson.Date >= currentDate.AddDays(minRange).Date &&
                    x.DateOfLesson.Date <= currentDate.AddDays(maxRange).Date)
                .OrderBy(x => x.DateOfLesson)
                .ToList();

            this.students = dbContext.Students.ToList();
        }

        public Task SaveRowsAsync(DataGridViewRowCollection rows)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i];

                var studentId = GetStudentId();
                for (var j = 2; j < row.Cells.Count; j++) // change code
                {
                    if ()
                }

                short GetStudentId()
                {
                    var studentId = (short)row.Cells["id"].Value;
                    return studentId;
                }
            }

            foreach (var row in rows[])
            {
                row
            }
        }
    }
}
