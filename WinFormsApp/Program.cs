using System;
using System.Windows.Forms;
using WinFormsApp.Forms;

namespace WinFormsApp
{
	public class Program
	{
		[STAThread]
		public static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
