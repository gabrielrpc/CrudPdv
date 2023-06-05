using CrudPdv.Presenter;
using CrudPdv.View;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace CrudPdv
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
            IMainView view = new MainView();
            new MainPresenter(view, sqlConnectionString);
            Application.Run((Form)view);
        }
    }
}
