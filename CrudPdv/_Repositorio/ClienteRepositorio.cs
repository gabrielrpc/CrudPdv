using CrudPdv.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CrudPdv._Repositorio
{
    public class ClienteRepositorio : RepositorioBase, IClienteRepository
    {
        //construtor
        public ClienteRepositorio(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //metodos
        public void Add(ClienteModel clienteModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Clientes values (@nome, @prefixo, @phone, @cell, @cpf, @email, @nasc )";
                command.Parameters.Add("@nome", SqlDbType.NVarChar).Value = clienteModel.Nome;
                command.Parameters.Add("@prefixo", SqlDbType.NVarChar).Value = clienteModel.Prefix.Replace("(", string.Empty).Replace(")", string.Empty);
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = clienteModel.Phone.Replace("-", string.Empty);
                command.Parameters.Add("@cell", SqlDbType.NVarChar).Value = clienteModel.CellPhone.Replace("-", string.Empty);
                command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = clienteModel.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = clienteModel.Email;
                command.Parameters.Add("@nasc", SqlDbType.Date).Value = Convert.ToDateTime(clienteModel.DataNasc).Date;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from clientes where cli_id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ClienteModel clienteModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update clientes set cli_nome=@nome, cli_prefixo=@prefixo, cli_celular=@cell, cli_telefone=@phone, cli_cpf=@cpf, cli_email=@email, cli_dataNasc=@nasc where cli_id=@id";
                command.Parameters.Add("@nome", SqlDbType.NVarChar).Value = clienteModel.Nome;
                command.Parameters.Add("@prefixo", SqlDbType.NVarChar).Value = clienteModel.Prefix.Replace("(", string.Empty).Replace(")", string.Empty);
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = clienteModel.Phone.Replace("-", string.Empty);
                command.Parameters.Add("@cell", SqlDbType.NVarChar).Value = clienteModel.CellPhone.Replace("-", string.Empty);
                command.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = clienteModel.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = clienteModel.Email;
                command.Parameters.Add("@nasc", SqlDbType.Date).Value = Convert.ToDateTime(clienteModel.DataNasc).Date;

                command.Parameters.Add("@id", SqlDbType.Int).Value = clienteModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            var clientList = new List<ClienteModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from clientes order by cli_id desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var clienteModel = new ClienteModel();
                        clienteModel.Id = (int)reader[0];
                        clienteModel.Nome = reader[1].ToString();
                        clienteModel.Prefix = reader[2].ToString();
                        clienteModel.Phone = reader[3].ToString();
                        clienteModel.CellPhone = reader[4].ToString();
                        clienteModel.Cpf = reader[5].ToString();
                        clienteModel.Email = reader[6].ToString();
                        clienteModel.DataNasc = reader[7].ToString();

                        clientList.Add(clienteModel);
                    }
                }
            }
            return clientList;
        }

        public IEnumerable<ClienteModel> GetByValue(string value)
        {
            var clientList = new List<ClienteModel>();
            string clienteCPF = value;
            string clienteName = value;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"select * from clientes where cli_cpf like @cpf+'%' or cli_nome like @nome+'%' order by cli_id desc";

                command.Parameters.Add("@cpf", SqlDbType.VarChar).Value = clienteCPF;
                command.Parameters.Add("@nome", SqlDbType.VarChar).Value = clienteName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var clienteModel = new ClienteModel();
                        clienteModel.Id = (int)reader[0];
                        clienteModel.Nome = reader[1].ToString();
                        clienteModel.Prefix = reader[2].ToString();
                        clienteModel.Phone = reader[3].ToString();
                        clienteModel.CellPhone = reader[4].ToString();
                        clienteModel.Cpf = reader[5].ToString();
                        clienteModel.Email = reader[6].ToString();
                        clienteModel.DataNasc = reader[7].ToString();

                        clientList.Add(clienteModel);
                    }
                }
            }
            return clientList;
        }
    }
}
