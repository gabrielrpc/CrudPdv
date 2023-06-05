using System;
using System.Windows.Forms;

namespace CrudPdv.View
{
    public interface IClienteView
    {
        //propriedades - campos
        string CliId { get; set; }
        string CliName { get; set; }
        string CliPrefix { get; set; }
        string CliPhone { get; set; }
        string CliCellPhone { get; set; }
        string CliCpf { get; set; }
        string CliEmail { get; set; }
        string CliDataNasc { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //metodos
        void SetClienteListBindingSource(BindingSource clieteList);
        void Show();

    }
}
