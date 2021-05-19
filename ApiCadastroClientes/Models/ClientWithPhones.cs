using System;

namespace ApiCadastroClientes.Models
{
    public class ClientWithPhones
    {
        public string name { get; set; }
        public string cpf { get; set; }
        public string birthDate { get; set; }
        public string[]  phones { get; set; }

        public ClientWithPhones(string  name, string  cpf, string  birthDate, string[] phones)
        {
            this.name = name;
            this.cpf = cpf;
            this.birthDate = birthDate;
            this.phones = phones;
        }
    }
}
