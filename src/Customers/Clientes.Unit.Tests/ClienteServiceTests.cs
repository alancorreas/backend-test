using Customers;
using Customers.Models.Request;
using Customers.Repositories;
using Customers.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clientes.Unit.Tests
{
    public class ClienteServiceTests
    {
        #region POST

        [Fact]
        public async Task Criar_Cliente_Sem_Endereco_Com_Sucesso_Retorna_O_Cliente_Criado()
        {
            // Preparação

            string nome = "Nome";
            string email = "email@microsoft.com";
            int codigoArea = 41;
            string telefone = "999999999";
            var cliente = new ClienteRequest
            {
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone
            };

            var mockRepository = new Mock<IClienteRepository>();
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem, Customers.Models.Cliente clienteRetornado) = await service.CriarCliente(cliente);

            mockRepository.Verify(r => r.InserirCliente(It.IsAny<Customers.Models.Cliente>()), Times.Exactly(1));
            Assert.Equal(201, statusCode);
            Assert.Null(mensagem);
            Assert.Equal(nome, clienteRetornado.Nome);
            Assert.Equal(email, clienteRetornado.Email);
            Assert.Equal(codigoArea, clienteRetornado.CodigoArea);
            Assert.Equal(telefone, clienteRetornado.Telefone);
            Assert.Null(cliente.Endereco);
        }

        [Fact]
        public async Task Criar_Cliente_Com_Sucesso_Retorna_O_Cliente_Criado()
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
            var cliente = new ClienteRequest
            {
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = logradouro,
                    Numero = numero,
                    Complemento = complemento,
                    CEP = cep,
                    Cidade = cidade,
                    UF = uf
                }
            };

            var mockRepository = new Mock<IClienteRepository>();
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem, Customers.Models.Cliente clienteRetornado) = await service.CriarCliente(cliente);

            mockRepository.Verify(r => r.InserirCliente(It.IsAny<Customers.Models.Cliente>()), Times.Exactly(1));
            Assert.Equal(201, statusCode);
            Assert.Null(mensagem);
            Assert.Equal(nome, clienteRetornado.Nome);
            Assert.Equal(email, clienteRetornado.Email);
            Assert.Equal(codigoArea, clienteRetornado.CodigoArea);
            Assert.Equal(telefone, clienteRetornado.Telefone);
            Assert.Equal(logradouro, clienteRetornado.Endereco.Logradouro);
            Assert.Equal(numero, clienteRetornado.Endereco.Numero);
            Assert.Equal(complemento, clienteRetornado.Endereco.Complemento);
            Assert.Equal(cep, clienteRetornado.Endereco.CEP);
            Assert.Equal(cidade, clienteRetornado.Endereco.Cidade);
            Assert.Equal(uf, clienteRetornado.Endereco.UF);
        }

        #endregion

        #region PUT

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Utilizando_Ids_Diferentes_Na_Rota_E_No_Corpo_Retorna_BadRequest()
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
            var cliente = new ClienteUpdateRequest
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = logradouro,
                    Numero = numero,
                    Complemento = complemento,
                    CEP = cep,
                    Cidade = cidade,
                    UF = uf
                }
            };

            var mockRepository = new Mock<IClienteRepository>();
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem) = await service.AtualizarCliente(Guid.NewGuid(), cliente);

            mockRepository.Verify(r => r.AtualizarCliente(It.IsAny<Customers.Models.Cliente>()), Times.Never);
            Assert.Equal(400, statusCode);
            Assert.Equal("O id de busca não pode ser diferente do id do objeto.", mensagem);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Inexistente_Retorna_NotFound()
        {
            // Preparação
            Guid id = Guid.NewGuid();
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
            var cliente = new ClienteUpdateRequest
            {
                Id = id,
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = logradouro,
                    Numero = numero,
                    Complemento = complemento,
                    CEP = cep,
                    Cidade = cidade,
                    UF = uf
                }
            };

            var mockRepository = new Mock<IClienteRepository>();
            var mockUpdateReadonlyList = new Mock<IReadOnlyList<IUpdateEntry>>();
            mockUpdateReadonlyList.Setup(rl => rl.Count).Returns(1);
            mockRepository.Setup((r) => r.AtualizarCliente(It.IsAny<Customers.Models.Cliente>())).Throws(new DbUpdateConcurrencyException("", mockUpdateReadonlyList.Object));
            mockRepository.Setup((r) => r.ClienteExiste(It.Is<Guid>((g) => g == id))).Returns(() => Task.FromResult(false));
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem) = await service.AtualizarCliente(id, cliente);

            mockRepository.Verify(r => r.AtualizarCliente(It.IsAny<Customers.Models.Cliente>()), Times.Exactly(1));
            mockRepository.Verify(r => r.ClienteExiste(It.IsAny<Guid>()), Times.Exactly(1));
            Assert.Equal(404, statusCode);
            Assert.Equal("Cliente não encontrado.", mensagem);
        }

        [Fact]
        public async Task Atualizar_Cliente_Com_Sucesso_Retorna_O_Status_NoContent_Sem_Mensagem_De_Erro()
        {
            // Preparação
            Guid id = Guid.NewGuid();
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
            var cliente = new ClienteUpdateRequest
            {
                Id = id,
                Nome = nome,
                Email = email,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = logradouro,
                    Numero = numero,
                    Complemento = complemento,
                    CEP = cep,
                    Cidade = cidade,
                    UF = uf
                }
            };

            var mockRepository = new Mock<IClienteRepository>();
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem) = await service.AtualizarCliente(id, cliente);

            mockRepository.Verify(r => r.AtualizarCliente(It.IsAny<Customers.Models.Cliente>()), Times.Exactly(1));
            Assert.Equal(204, statusCode);
        }

        #endregion

        #region DELETE

        [Fact]
        public async Task Tentar_Apagar_Um_Cliente_Com_Um_Id_Inexistente_Retorna_Nao_Encontrado()
        {
            // Preparação
            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.RecuperarCliente(It.IsAny<Guid>())).Returns(() => Task.FromResult((Customers.Models.Cliente)null));
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem) = await service.ApagarCliente(Guid.NewGuid());

            mockRepository.Verify(r => r.RecuperarCliente(It.IsAny<Guid>()), Times.Exactly(1));
            Assert.Equal(404, statusCode);
            Assert.Equal("Cliente não encontrado.", mensagem);
        }

        [Fact]
        public async Task Apagar_Um_Cliente_Com_Sucesso_Retorna_Ok_Sem_Mensagem()
        {
            // Preparação
            var mockRepository = new Mock<IClienteRepository>();
            mockRepository.Setup(r => r.RecuperarCliente(It.IsAny<Guid>())).Returns<Guid>((id) => Task.FromResult(new Customers.Models.Cliente { Id = id }));
            // Execução
            var service = new ClienteService(mockRepository.Object);

            // Conferência
            (int statusCode, string mensagem) = await service.ApagarCliente(Guid.NewGuid());

            mockRepository.Verify(r => r.RecuperarCliente(It.IsAny<Guid>()), Times.Exactly(1));
            mockRepository.Verify(r => r.ApagarCliente(It.IsAny<Customers.Models.Cliente>()), Times.Exactly(1));
            Assert.Equal(200, statusCode);
            Assert.Null(mensagem);
        }

        #endregion DELETE

        #region GET

        [Fact]
        public async Task Tentar_Recuperar_Cliente_Inexistente_Retorna_Nulo_Nao_Encontrado()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.RecuperarCliente(It.IsAny<Guid>())).Returns(Task.FromResult((Customers.Models.Cliente)null));

            var service = new ClienteService(mockRepo.Object);

            (int statusCode, string mensagem, Customers.Models.Cliente cliente) = await service.RecuperarCliente(Guid.NewGuid());

            Assert.Equal(404, statusCode);
            Assert.Equal("Cliente não encontrado", mensagem);
            Assert.Null(cliente);
        }

        [Fact]
        public async Task Recuperar_Cliente_Com_Sucesso_Retorna_O_Cliente_Ok()
        {
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
            Guid id = Guid.NewGuid();

            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.RecuperarCliente(It.IsAny<Guid>())).Returns(Task.FromResult(new Customers.Models.Cliente
            {
                Id = id,
                Nome = nome,
                CodigoArea = codigoArea,
                Telefone = telefone,
                Email = email,
                Endereco = new Customers.Models.Endereco
                {
                    CEP = cep,
                    Cidade = cidade,
                    Complemento = complemento,
                    Id = id,
                    Logradouro = logradouro,
                    Numero = numero,
                    UF = uf
                }
            }));

            var service = new ClienteService(mockRepo.Object);

            (int statusCode, string mensagem, Customers.Models.Cliente cliente) = await service.RecuperarCliente(Guid.NewGuid());

            Assert.Equal(200, statusCode);
            Assert.Null(mensagem);
            mockRepo.Verify((repo) => repo.RecuperarCliente(It.IsAny<Guid>()), Times.Exactly(1));
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(email, cliente.Email);
            Assert.Equal(codigoArea, cliente.CodigoArea);
            Assert.Equal(telefone, cliente.Telefone);
            Assert.Equal(logradouro, cliente.Endereco.Logradouro);
            Assert.Equal(numero, cliente.Endereco.Numero);
            Assert.Equal(complemento, cliente.Endereco.Complemento);
            Assert.Equal(cep, cliente.Endereco.CEP);
            Assert.Equal(cidade, cliente.Endereco.Cidade);
            Assert.Equal(uf, cliente.Endereco.UF);
        }

        #endregion
    }
}
