
using Microsoft.EntityFrameworkCore;
using ApiCadastroClientes.Models;

namespace ApiCadastroClientes.Data
{
    public class CadastroClientsContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public CadastroClientsContext(DbContextOptions<CadastroClientsContext> options) :
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(p => p.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.cpf)
                .IsUnique();
            modelBuilder.Entity<Phone>()
                .HasIndex(p => p.number)
                .IsUnique();
            modelBuilder.Entity<Phone>()
                .Property(p => p.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Admin>().Property(p => p.id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Admin>().HasData(new Admin { id = 1, username = "admin", password = "12345" });
        }
    }
}
