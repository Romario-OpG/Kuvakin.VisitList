using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.Interfaces
{
    public interface IScheduleDataGrid
    {
        public Task SaveRowsAsync(DataGridViewRowCollection rows);
    }
}
