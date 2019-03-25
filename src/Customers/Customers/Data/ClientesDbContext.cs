using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Customers.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clientes.Models
{
    public class ClientesDbContext : DbContext, IClientesDbContext
    {
        public ClientesDbContext (DbContextOptions<ClientesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Guid idCliente1 = Guid.Parse("D201F4AB-46E7-48B7-80D1-BFD5569F1316");
            var cliente1 = new Customers.Models.Cliente { Id = idCliente1, Nome = "Cliente1", Email = "cliente1@email.com" };
            Guid idCliente2 = Guid.Parse("5BA32385-0894-4589-85D5-8C83F924711C");
            Guid idCliente3 = Guid.Parse("5CB5A9BC-6AE7-43ED-AC6F-0CA70376CC3C");
            var cliente3 = new Customers.Models.Cliente { Id = idCliente3, Nome = "Cliente3", Email = "cliente1@email3.com", CodigoArea = 11, Telefone = "888888888" };
            modelBuilder.Entity<Cliente>().HasData(cliente1);
            modelBuilder.Entity<Cliente>().HasData(new Customers.Models.Cliente { Id = idCliente2, Nome = "Cliente2", Email = "cliente1@email2.com", CodigoArea = 41, Telefone = "999999999" });
            modelBuilder.Entity<Cliente>().HasData(cliente3);

            Guid idEndereco1 = Guid.Parse("B815C0A7-F0A6-4943-AA08-180A992E838E");
            Guid idEndereco2 = Guid.Parse("3F1BE2C4-B509-47A2-A097-93AEF7D337B6");
            modelBuilder.Entity<Endereco>().HasData(new Customers.Models.Endereco { Id = idEndereco1, CEP = "81000000", Cidade = "Curitiba", Complemento = "Ap 1 Bl 1s", Logradouro = "Rua de Teste", Numero = "999", UF = "PR", ClienteForeignKey = idCliente1 });
            modelBuilder.Entity<Endereco>().HasData(new Customers.Models.Endereco { Id = idEndereco2, CEP = "06100000", Cidade = "São Paulo", Logradouro = "Avenida de Teste", Numero = "1", UF = "SP", ClienteForeignKey = idCliente3 });
        }

        public DbSet<Customers.Models.Cliente> Cliente { get; set; }
        public DbSet<Customers.Models.Endereco> Endereco { get; set; }

        async Task IClientesDbContext.UpdateEntity<T>(T model)
        {
            Entry(model).State = EntityState.Modified;
        }
    }
}
