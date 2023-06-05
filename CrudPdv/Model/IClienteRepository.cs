using System.Collections.Generic;

namespace CrudPdv.Model
{
    public interface IClienteRepository
    {
        void Add(ClienteModel clienteModel);
        void Edit(ClienteModel clienteModel);
        void Delete(int id);

        IEnumerable<ClienteModel> GetAll();
        IEnumerable<ClienteModel> GetByValue(string value); // Pesquisa
    }
}
