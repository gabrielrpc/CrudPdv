using System;
using System.Windows.Forms;


namespace CrudPdv.View
{
    public partial class ClienteView : Form, IClienteView
    {
        private string _message;
        private bool _IsSuccessful;
        private bool _IsEdit;

        //construtor
        public ClienteView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();

            tabControl.TabPages.Remove(tabDetalhes);
            btnSairCliente.Click += delegate
            {
                this.Close();
            };
            
        }

        private void AssociateAndRaiseViewEvents()
        {
            //pesquisar
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                };
            };
            //Adicionar 
            btnAdd.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl.TabPages.Remove(tabLista);
                tabControl.TabPages.Add(tabDetalhes);
                tabDetalhes.Text = "Adicionar novo Cliente";
            };
            //editar
            btnEdit.Click += delegate
            {
                var resultado = MessageBox.Show("Deseja editar o cliente selecionado?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                if (resultado == DialogResult.Yes)
                {
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabControl.TabPages.Remove(tabLista);
                    tabControl.TabPages.Add(tabDetalhes);
                    tabDetalhes.Text = "Editar Cliente";
                }

            };
            //salvar
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl.TabPages.Remove(tabDetalhes);
                    tabControl.TabPages.Add(tabLista);
                }
                MessageBox.Show(Message);
            };
            //cancelar
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl.TabPages.Remove(tabDetalhes);
                tabControl.TabPages.Add(tabLista);
            };
            //deletar
            btnDelete.Click += delegate
            {
                var resultado = MessageBox.Show("Deseja excluir o cliente selecionado?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //propriedades
        public string CliId { get => txtID.Text; set => txtID.Text = value; }
        public string CliName { get => txtNome.Text; set => txtNome.Text = value; }
        public string CliPrefix { get => txtPrefix.Text; set => txtPrefix.Text = value; }
        public string CliPhone { get => txtTel.Text; set => txtTel.Text = value; }
        public string CliCellPhone { get => txtCell.Text; set => txtCell.Text = value; }
        public string CliCpf { get => txtCpf.Text; set => txtCpf.Text = value; }
        public string CliEmail { get => txtEmail.Text; set => txtEmail.Text = value; }
        public string CliDataNasc { get => txtData.Text; set => txtData.Text = value; }

        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public bool IsSuccessful { get => _IsSuccessful; set => _IsSuccessful = value; }
        public string Message { get => _message; set => _message = value; }

        //eventos
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //metodos
        public void SetClienteListBindingSource(BindingSource clieteList)
        {
            dataGridView.DataSource = clieteList;
        }


        //abrir uma unica instancia de formulario
        private static ClienteView _instancia;
        public static ClienteView GetInstancìa(Form parentContainer)
        {
            if (_instancia == null || _instancia.IsDisposed)
            {
                _instancia = new ClienteView();
                _instancia.MdiParent = parentContainer;
                _instancia.FormBorderStyle = FormBorderStyle.None;
                _instancia.Dock = DockStyle.Fill;
                
            }
            else
            {
                if (_instancia.WindowState == FormWindowState.Minimized)
                    _instancia.WindowState = FormWindowState.Normal;
                _instancia.BringToFront();
            }
            return _instancia;
        }
    }
}
