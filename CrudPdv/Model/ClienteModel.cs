using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudPdv.Model
{
    public class ClienteModel
    {

        //campos
        private int _id;
        private string _nome;
        private string _prefix;
        private string _phone;
        private string _cellPhone;
        private string _cpf;
        private string _email;
        private string _dataNasc;

        //propriedades
        [DisplayName("ID")]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Campo 'NOME' é obrigatório!")]
        public string Nome
        {
            get => _nome;
            set => _nome = value;
        }

        [DisplayName("DDD")]
        public string Prefix
        {
            get => _prefix;
            set => _prefix = value;
        }

        [DisplayName("Telefone")]
        [StringLength(9)]
        [RegularExpression(@"^\d{4}-?\d{4}$", ErrorMessage = "Informe um TELEFONE válido...")]
        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        [DisplayName("Celular")]
        [StringLength(10)]
        [RegularExpression(@"^\d{5}-?\d{4}$", ErrorMessage = "Informe um CELULAR válido...")]
        public string CellPhone
        {
            get => _cellPhone;
            set => _cellPhone = value;
        }

        [DisplayName("CPF")]
        [StringLength(15)]
        [RegularExpression(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}$", ErrorMessage = "Informe um CPF válido...")]
        public string Cpf
        {
            get => _cpf;
            set => _cpf = value;
        }

        [DisplayName("Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Informe um EMAIL válido...")]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        [DisplayName("Data de Nascimento")]
        public string DataNasc
        {
            get => _dataNasc;
            set => _dataNasc = value;
        }

    }
}
