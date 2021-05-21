

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCadastroClientes.Models
{
    [Table("clients")]
    public class Client
    {
        [Key]
        public int id { get; set; }
        public string client_name { get; set; }
        public string cpf { get; set; }
        public string birth_date { get; set; }
        public List<Phone> Phones {get; set;}
    }
}
