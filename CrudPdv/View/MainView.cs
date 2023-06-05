using System;
using System.Windows.Forms;

namespace CrudPdv.View
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            btnAddCliente.Click += delegate
            {
                ShowClienteView?.Invoke(this, EventArgs.Empty);
            };
        }

        public event EventHandler ShowClienteView;
    }

    

}
