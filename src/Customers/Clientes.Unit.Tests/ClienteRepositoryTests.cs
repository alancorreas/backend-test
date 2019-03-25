using Clientes.Models;
using Customers.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Update;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Customers.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Clientes.Unit.Tests
{
    public class ClienteRepositoryTests
    {
        #region Insert

        [Fact]
        public async Task Inserir_Cliente_Com_Sucesso()
        {
            // Preparação
            string nome = "Nome";
            string email = "email@microsoft.com";
            int codigoArea = 41;
            string telefone = "999999999";
            string logradouro = "Logradouro com 50 caracteres resulta em sucessoooo";
            string numero = "15-A 6789";
            string complemento = "Apenas-20 caracteres";
            string cep = "8120000";
            string cidade = "Cidade Com apenas Trinta Carac";
            string uf = "PR";
            var cliente = new Customers.Models.Cliente
            {
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Endereco = new Customers.Models.Endereco
                {
                    Logradouro = logradouro,
                    Numero = numero,
                    Complemento = complemento,
                    CEP = cep,
                    Cidade = cidade,
                    UF = uf
                }
            };

            var mockDbSet = new Mock<DbSet<Customers.Models.Cliente>>();
            var mockDbContext = new Mock<IClientesDbContext>();
            mockDbContext.Setup(dbc => dbc.Cliente).Returns(mockDbSet.Object);

            // Execução
            var repository = new ClienteRepository(mockDbContext.Object);
            await repository.InserirCliente(cliente);

            // Conferência
            mockDbContext.Verify(r => r.Cliente, Times.Exactly(1));
            mockDbContext.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
        }

        #endregion Insert

        #region Update

        //[Fact]
        //public async Task Tentar_Atualizar_Cliente_Com_Cliente_Inexistente_Dispara_Excecao()
        //{
        //    // Preparação
        //    Guid id = Guid.NewGuid();
        //    string nome = "Nome";
        //    string email = "email@microsoft.com";
        //    int codigoArea = 41;
        //    string telefone = "999999999";
        //    string logradouro = "Logradouro com 50 caracteres resulta em sucessoooo";
        //    string numero = "15-A 6789";
        //    string complemento = "Apenas-20 caracteres";
        //    string cep = "8120000";
        //    string cidade = "Cidade Com apenas Trinta Carac";
        //    string uf = "PR";
        //    var cliente = new Customers.Models.Cliente
        //    {
        //        Id = id,
        //        Nome = nome,
        //        Email = email,
        //        CodigoArea = codigoArea,
        //        Telefone = telefone,
        //        Endereco = new Customers.Models.Endereco
        //        {
        //            Logradouro = logradouro,
        //            Numero = numero,
        //            Complemento = complemento,
        //            CEP = cep,
        //            Cidade = cidade,
        //            UF = uf
        //        }
        //    };

        //    var mockEntry = new Mock<EntityEntry>();
        //    var mockUpdateReadonlyList = new Mock<IReadOnlyList<IUpdateEntry>>();
        //    var mockDbSet = new Mock<DbSet<Customers.Models.Cliente>>();
        //    var mockDbSetEndereco = new Mock<DbSet<Customers.Models.Endereco>>();
        //    mockUpdateReadonlyList.Setup(rl => rl.Count).Returns(1);
        //    var mockDbContext = new Mock<IClientesDbContext>();
        //    mockDbContext.Setup(dbc => dbc.Cliente).Returns(mockDbSet.Object);
        //    mockDbContext.Setup(dbc => dbc.Endereco).Returns(mockDbSetEndereco.Object);
        //    mockDbContext.Setup(dbc => dbc.Entry(It.Is<Customers.Models.Cliente>(c => c == cliente))).Throws(new DbUpdateConcurrencyException("", mockUpdateReadonlyList.Object));

        //    // Execução
        //    var repository = new ClienteRepository(mockDbContext.Object);
        //    await repository.AtualizarCliente(cliente);

        //    // Conferência
        //    mockDbContext.Verify(r => r.UpdateEntity(It.Is<Customers.Models.Cliente>(c => c == cliente)), Times.Exactly(1));
        //    mockEntry.Verify(e => e.State, Times.Never);
        //}

        //[Fact]
        //public async Task Atualizar_Cliente_Com_Sucesso()
        //{
        //    // Preparação
        //    Guid id = Guid.NewGuid();
        //    string nome = "Nome";
        //    string email = "email@microsoft.com";
        //    int codigoArea = 41;
        //    string telefone = "999999999";
        //    string logradouro = "Logradouro com 50 caracteres resulta em sucessoooo";
        //    string numero = "15-A 6789";
        //    string complemento = "Apenas-20 caracteres";
        //    string cep = "8120000";
        //    string cidade = "Cidade Com apenas Trinta Carac";
        //    string uf = "PR";
        //    var cliente = new Customers.Models.Cliente
        //    {
        //        Id = id,
        //        Nome = nome,
        //        Email = email,
        //        CodigoArea = codigoArea,
        //        Telefone = telefone,
        //        Endereco = new Customers.Models.Endereco
        //        {
        //            Logradouro = logradouro,
        //            Numero = numero,
        //            Complemento = complemento,
        //            CEP = cep,
        //            Cidade = cidade,
        //            UF = uf
        //        }
        //    };

        //    var mockDbContext = new Mock<IClientesDbContext>();
        //    var mockDbSetEndereco = new Mock<DbSet<Customers.Models.Endereco>>();
        //    mockDbContext.Setup(dbc => dbc.Endereco).Returns(mockDbSetEndereco.Object);

        //    // Execução
        //    var repository = new ClienteRepository(mockDbContext.Object);
        //    await repository.AtualizarCliente(cliente);

        //    // Conferência
        //    mockDbContext.Verify(r => r.UpdateEntity(It.Is<Customers.Models.Cliente>(c => c == cliente)), Times.Exactly(1));
        //}

        #endregion Update

        #region Delete

        //[Fact]
        //public async Task Apagar_Cliente_Com_Sucesso()
        //{
        //    // Preparação
        //    var mockDbContext = new Mock<IClientesDbContext>();
        //    var mockCliente = new Mock<DbSet<Cliente>>();
        //    mockDbContext.Setup(dbc => dbc.Cliente).Returns(mockCliente.Object);
        //    var mockEndereco = new Mock<DbSet<Endereco>>();
        //    mockDbContext.Setup(dbc => dbc.Endereco).Returns(mockEndereco.Object);

        //    // Execução
        //    var repository = new ClienteRepository(mockDbContext.Object);
        //    await repository.ApagarCliente(new Cliente { Id = Guid.NewGuid() });

        //    // Validação
        //    mockCliente.Verify(dbs => dbs.Remove(It.IsAny<Cliente>()), Times.Exactly(1));
        //}

        #endregion

        #region Select

        //[Fact]
        //public async Task Tentar_Recuperar_Cliente_Inexistente_Retorna_Nulo()
        //{
        //    var mockDbSetCliente = new Mock<DbSet<Cliente>>();
        //    var mockDbContext = new Mock<IClientesDbContext>();
        //    mockDbContext.Setup(dbc => dbc.Cliente).Returns(mockDbSetCliente.Object);
        //    var repo = new ClienteRepository(mockDbContext.Object);

        //    Cliente cliente = await repo.RecuperarCliente(Guid.NewGuid());

        //    mockDbSetCliente.Verify(dbs => dbs.FindAsync(It.IsAny<Guid>()), Times.Exactly(1));
        //    Assert.Null(cliente);
        //}

        //[Fact]
        //public async Task Recuperar_Cliente_Com_Sucesso()
        //{
        //    string nome = "Nome";
        //    string email = "email@microsoft.com";
        //    int codigoArea = 41;
        //    string telefone = "999999999";
        //    string logradouro = "Logradouro com 50 caracteres resulta em sucessoooo";
        //    string numero = "15-A 6789";
        //    string complemento = "Apenas-20 caracteres";
        //    string cep = "8120000";
        //    string cidade = "Cidade Com apenas Trinta Carac";
        //    string uf = "PR";
        //    Guid id = Guid.NewGuid();

        //    var mockDbSetCliente = new Mock<DbSet<Cliente>>();
        //    mockDbSetCliente.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Customers.Models.Cliente
        //    {
        //        Id = id,
        //        Nome = nome,
        //        CodigoArea = codigoArea,
        //        Telefone = telefone,
        //        Email = email,
        //        Endereco = new Customers.Models.Endereco
        //        {
        //            CEP = cep,
        //            Cidade = cidade,
        //            Complemento = complemento,
        //            Id = id,
        //            Logradouro = logradouro,
        //            Numero = numero,
        //            UF = uf
        //        }
        //    }));

        //    var mockDbSetEndereco = new Mock<DbSet<Endereco>>();

        //    var mockDbContext = new Mock<IClientesDbContext>();
        //    mockDbContext.Setup(dbc => dbc.Cliente).Returns(mockDbSetCliente.Object);
        //    var repo = new ClienteRepository(mockDbContext.Object);

        //    Cliente cliente = await repo.RecuperarCliente(id);

        //    mockDbSetCliente.Verify(dbs => dbs.FindAsync(It.IsAny<Guid>()), Times.Exactly(1));
        //    Assert.Equal(nome, cliente.Nome);
        //    Assert.Equal(email, cliente.Email);
        //    Assert.Equal(codigoArea, cliente.CodigoArea);
        //    Assert.Equal(telefone, cliente.Telefone);
        //    Assert.Equal(logradouro, cliente.Endereco.Logradouro);
        //    Assert.Equal(numero, cliente.Endereco.Numero);
        //    Assert.Equal(complemento, cliente.Endereco.Complemento);
        //    Assert.Equal(cep, cliente.Endereco.CEP);
        //    Assert.Equal(cidade, cliente.Endereco.Cidade);
        //    Assert.Equal(uf, cliente.Endereco.UF);
        //}

        #endregion
    }
}
