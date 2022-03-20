using Database.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp.Interfaces
{
    public interface IScheduleGridView
    {
        public IEnumerable<DataGridViewColumn> GetColumns(DateTime currentDate);
        public IEnumerable<DataGridViewRow> GetRows(IEnumerable<Schedule> schedules, IEnumerable<Student> students);
    }
}
