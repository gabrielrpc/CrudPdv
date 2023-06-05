using CrudPdv.Model;
using CrudPdv.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CrudPdv.Presenter
{
    public class ClientePresenter
    {
        //campos
        private IClienteView view;
        private IClienteRepository repository;
        private BindingSource clienteBindingSource;
        private IEnumerable<ClienteModel> clienteList;

        //construtor
        public ClientePresenter(IClienteView view, IClienteRepository repository)
        {
            this.clienteBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // escrever os metodos alternados nos eventos do view
            this.view.SearchEvent += SearchClientes;
            this.view.AddNewEvent += AddNewCliente;
            this.view.EditEvent += LoadSelectedClienteToEdit;
            this.view.DeleteEvent += DeleteSelectedCliente;
            this.view.SaveEvent += SaveCliente;
            this.view.CancelEvent += CancelAction;

            //setar a fonte de ligação dos clientes
            this.view.SetClienteListBindingSource(clienteBindingSource);

            //carregar a lista de clientes
            LoadAllClienteList();

            //mostrar o view
            this.view.Show();
        }

        //metodos

        private void CleanViewFields() // limpar campos do formulário
        {
            view.CliId = "0";
            view.CliName = "";
            view.CliPrefix = "";
            view.CliPhone = "";
            view.CliCellPhone = "";
            view.CliCpf = "";
            view.CliEmail = "";
            view.CliDataNasc = "";
        }

        private void LoadAllClienteList()
        {
            clienteList = repository.GetAll();
            clienteBindingSource.DataSource = clienteList;
        }
        private void SearchClientes(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                clienteList = repository.GetByValue(this.view.SearchValue);
            else clienteList = repository.GetAll();
            clienteBindingSource.DataSource = clienteList;
        }
        private void AddNewCliente(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private void LoadSelectedClienteToEdit(object sender, EventArgs e)
        {
            //Dados foram copiados para teste, de um backup de banco de um cliente/ foi usado o Replace para tratar de alguns caracteres e espaços em branco ao selecionar e clicar em editar um cliente
            //quando os dados são salvos após a edição o banco salva sem formatação.

            var cliente = (ClienteModel)clienteBindingSource.Current;
            view.CliId = cliente.Id.ToString();
            view.CliName = cliente.Nome.Replace(" ", string.Empty);
            view.CliPrefix = cliente.Prefix.Replace(" ", string.Empty);
            view.CliPhone = cliente.Phone.Replace(" ", string.Empty);
            view.CliCellPhone = cliente.CellPhone.Replace("-", string.Empty).Replace(" ", string.Empty);
            view.CliCpf = cliente.Cpf.Replace("-", ".").Replace("/", "-").Replace(" ", string.Empty);
            view.CliEmail = cliente.Email.Replace(" ", string.Empty);
            view.CliDataNasc = cliente.DataNasc.ToString();
            view.IsEdit = true;

        }
        private void SaveCliente(object sender, EventArgs e)
        {
            var model = new ClienteModel
            {
                Id = Convert.ToInt32(view.CliId),
                Nome = view.CliName,
                Prefix = view.CliPrefix,
                Phone = view.CliPhone,
                CellPhone = view.CliCellPhone,
                Cpf = view.CliCpf,
                Email = view.CliEmail,
                DataNasc = view.CliDataNasc,
            };

            try
            {
                new Validacao.ModelDataValidation().Validar(model);
                if (view.IsEdit)
                {
                    repository.Edit(model);
                    view.Message = "Cadasto editado com sucesso!";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Cliente foi cadastrado com sucesso!";
                }
                view.IsSuccessful = true;
                LoadAllClienteList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void DeleteSelectedCliente(object sender, EventArgs e)
        {
            var cliente = (ClienteModel)clienteBindingSource.Current;
            if (cliente == null)
            {
                view.Message = "Não há clientes cadastrados para deletar!";
            }
            else
            {
                try
                {

                    repository.Delete(cliente.Id);
                    view.IsSuccessful = true;
                    view.Message = "Cliente '" + cliente.Nome + "' foi deletado!";
                    LoadAllClienteList();

                }
                catch (Exception ex)
                {
                    view.IsSuccessful = false;
                    view.Message = "Um erro ocorreu, não foi possivel deletar o cadastro do cliente";
                }
            }

        }
    }
}
