using ApiCadastroClientes.Models;
using ApiCadastroClientes.Data;
using ApiCadastroClientes.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCadastroClientes.Controllers
{
    [Route("/[controller]")]
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    public class AdminController : ControllerBase
    {   
        private readonly CadastroClientsContext _context;

        public AdminController(CadastroClientsContext context)
        {
            _context = context;
        }
        
        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> GetJ()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost]
        public  async Task<IActionResult> PostAuth([FromBody] Admin admin)
        {
            var authUser = await _context.Admin.SingleOrDefaultAsync(a => a.username.ToLower() == admin.username.ToLower());
            
            if(authUser == null)
            {
                return NotFound();
            }

            if(authUser.password == admin.password)
            {   
                var token = TokenService.GenerateToken(admin);

                return Ok( new { authToken = token});
            }

            return Unauthorized();
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
