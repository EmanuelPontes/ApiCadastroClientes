using ApiCadastroClientes.Models;
using ApiCadastroClientes.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCadastroClientes
{
    [Route("/[controller]")]
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    public class ClientsController : ControllerBase
    {   
        private readonly CadastroClientsContext _context;

        public ClientsController(CadastroClientsContext context)
        {
            _context = context;
        }
        // GET: api/<ClientsController>
        [HttpGet("list")]
        public List<ClientWithPhones> GetClientList()
        {
            var Clients = _context.Clients.Include(c => c.Phones);

            var clientsArr = Clients.ToArray<Client>();

            List<ClientWithPhones> clientListWithPhones = new List<ClientWithPhones>();
            foreach (Client client in clientsArr)
            {
                List<string> phoneArr = new List<string>();
                foreach (Phone phone in client.Phones)
                {
                    phoneArr.Add(phone.number);
                }
                clientListWithPhones.Add(new ClientWithPhones(client.id, client.client_name, client.cpf, client.birth_date, phoneArr.ToArray()));
            }

            return clientListWithPhones;


        }

        // GET /<ClientsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _context.Clients.SingleOrDefaultAsync(c => c.id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST /<ClientsController>
        [HttpPost("new")]
        public async Task<IActionResult> PostClient([FromBody] ClientWithPhones clientWithPhones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Client client = ConvertToModelClient(clientWithPhones);

            _context.Clients.Add(client);
            
            await _context.SaveChangesAsync();

            return Ok(client.id);
        }

        // PUT /<ClientsController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] ClientWithPhones clientWithPhones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientWithPhones.id)
            {
                return BadRequest();
            }

            Client client = ConvertToModelClient(clientWithPhones);
            client.id = clientWithPhones.id;

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _context.Clients.SingleOrDefaultAsync(c => c.id == id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(client);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clients.Any(e => e.id == id);
        }

        private Client ConvertToModelClient(ClientWithPhones clientWithPhones) {
            List<Phone> phones = new List<Phone>();
            foreach(string phoneNumber in clientWithPhones.phones)
            {
                phones.Add(new Phone { number = HttpUtility.HtmlEncode(phoneNumber) });
            }

            Client client = new Client
            {
                client_name = HttpUtility.HtmlEncode(clientWithPhones.name),
                birth_date = HttpUtility.HtmlEncode(clientWithPhones.birthDate),
                cpf = HttpUtility.HtmlEncode(clientWithPhones.cpf),
                Phones = phones
            };

            return client;
        }

    
    }
}
