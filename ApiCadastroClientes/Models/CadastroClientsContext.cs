
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroClientes.Models
{
    public class CadastroClientsContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public CadastroClientsContext(DbContextOptions<CadastroClientsContext> options) :
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(p => p.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Phone>()
                .Property(p => p.id)
                .ValueGeneratedOnAdd();
        }
    }
}
