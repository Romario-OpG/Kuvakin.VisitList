using Database.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp.Interfaces
{
    public interface IScheduleGridView
    {
        public IEnumerable<DataGridViewColumn> GetColumns();
        public IEnumerable<DataGridViewRow> GetRows(IEnumerable<Schedule> schedules, IEnumerable<Student> students);
    }
}
