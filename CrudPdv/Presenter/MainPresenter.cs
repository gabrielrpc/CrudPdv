using CrudPdv._Repositorio;
using CrudPdv.Model;
using CrudPdv.View;
using System;


namespace CrudPdv.Presenter
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowClienteView += ShowClienteView;
        }

        private void ShowClienteView(object sender, EventArgs e)
        {
            IClienteView view = ClienteView.GetInstancìa((MainView)mainView);
            IClienteRepository repository = new ClienteRepositorio(sqlConnectionString);

            new ClientePresenter(view, repository);
        }
    }
}
