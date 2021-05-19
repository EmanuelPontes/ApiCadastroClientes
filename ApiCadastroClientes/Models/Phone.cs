using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCadastroClientes.Models
{
    [Table("phones")]
    public class Phone
    {
        [Key]
        public int id { get; set; }
        public string number { get; set; }
        public int client_id { get; set; }
        [ForeignKey("client_id")]
        public Client Client { get; set; }
    }
}
