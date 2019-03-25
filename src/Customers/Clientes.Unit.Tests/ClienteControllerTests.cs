using Clientes.Models;
using Customers.Controllers;
using Customers.Models;
using Customers.Models.Request;
using Customers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Clientes.Unit.Tests
{
    public class ClienteControllerTests
    {
        #region POST
        
        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Nome_Nulo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest();

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � obrigat�rio.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Nome_Vazio_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � obrigat�rio.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Nome_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Inv Alido" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Nome_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "NomeComVinteEUmCaract" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome pode conter no m�ximo 20 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Email_Nulo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � obrigat�rio.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Email_Vazio_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � obrigat�rio.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Email_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Email_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email@7890123456789012345678901234567.com" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail n�o pode conter mais do que 40 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Codigo_de_Area_Abaixo_Do_Limite_Inferior_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email@microsoft.com", CodigoArea = 10 };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("CodigoArea", "O c�digo de �rea � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Codigo_de_Area_Acima_Do_Limite_Superior_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email@microsoft.com", CodigoArea = 100 };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("CodigoArea", "O c�digo de �rea � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Telefone_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email@microsoft.com", Telefone = "ABC" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Telefone_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest { Nome = "Nome", Email = "email@microsoft.com", Telefone = "1234567890123456" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O n�mero de telefone n�o pode conter mais do que 15 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Logradouro_Excedendo_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = "Logradouro com mais de 50 caracteres p/ causar erro"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Logradouro", "O logradouro pode conter no m�ximo 50 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Numero_Logradouro_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Numero = "15-A ~"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Numero", "O n�mero do logradouro � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Numero_Logradouro_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Numero = "15-A 67890"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Numero", "O n�mero do logradouro pode conter no m�ximo 10 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Complemento_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Complemento = "Mais de 20 caracteres"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Complemento", "O complemento do endere�o pode conter no m�ximo 20 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_CEP_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    CEP = "81000A00"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.CEP", "O CEP � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_CEP_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    CEP = "810000009"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.CEP", "O CEP pode conter no m�ximo 8 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Cidade_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Cidade = "Nome da Cidade !"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Cidade", "O nome da cidade � inv�lido.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_Cidade_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Cidade = "Cidade Com Mais de Trinta carac"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Cidade", "O nome da cidade pode conter no m�ximo 30 caracteres.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_UF_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    UF = "UC"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.UF", "Sigla do estado inv�lida.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Criar_Cliente_Com_UF_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    UF = "PRN"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.UF", "Sigla do estado inv�lida.");
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado.Result);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Criar_Cliente_Com_Sucesso_Retorna_Created()
        {
            // Prepara��o

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

            Guid id = Guid.NewGuid();

            var mockClienteService = new Mock<IClienteService>();
            mockClienteService.Setup((cs) => cs.CriarCliente(It.IsAny<ClienteRequest>())).Returns(Task.FromResult((201, (string)null, new Cliente
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
            })));
            // Execu��o
            var controller = new ClienteController(mockClienteService.Object);
            var resultado = await controller.PostCliente(cliente);

            // Confer�ncia
            ActionResult<Cliente> resultadoAction = Assert.IsType<ActionResult<Cliente>>(resultado);
            CreatedAtActionResult resultadoCreatedAt = Assert.IsType<CreatedAtActionResult>(resultado.Result);
            Cliente valorRetornado = Assert.IsType<Cliente>(resultadoCreatedAt.Value);
            mockClienteService.Verify((service) => service.CriarCliente(It.IsAny<ClienteRequest>()), Times.Exactly(1));
            Assert.Equal(nome, valorRetornado.Nome);
            Assert.Equal(email, valorRetornado.Email);
            Assert.Equal(codigoArea, valorRetornado.CodigoArea);
            Assert.Equal(telefone, valorRetornado.Telefone);
            Assert.Equal(logradouro, valorRetornado.Endereco.Logradouro);
            Assert.Equal(numero, valorRetornado.Endereco.Numero);
            Assert.Equal(complemento, valorRetornado.Endereco.Complemento);
            Assert.Equal(cep, valorRetornado.Endereco.CEP);
            Assert.Equal(cidade, valorRetornado.Endereco.Cidade);
            Assert.Equal(uf, valorRetornado.Endereco.UF);
        }

        #endregion

        #region PUT

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Nome_Nulo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest();

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � obrigat�rio.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Nome_Vazio_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � obrigat�rio.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Nome_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Inv Alido" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Nome_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "NomeComVinteEUmCaract" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Nome", "O nome pode conter no m�ximo 20 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Email_Nulo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � obrigat�rio.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Email_Vazio_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � obrigat�rio.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Email_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Email_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email@7890123456789012345678901234567.com" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail n�o pode conter mais do que 40 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Codigo_de_Area_Abaixo_Do_Limite_Inferior_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email@microsoft.com", CodigoArea = 10 };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("CodigoArea", "O c�digo de �rea � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Codigo_de_Area_Acima_Do_Limite_Superior_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email@microsoft.com", CodigoArea = 100 };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("CodigoArea", "O c�digo de �rea � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Telefone_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email@microsoft.com", Telefone = "ABC" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O e-mail � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Telefone_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest { Nome = "Nome", Email = "email@microsoft.com", Telefone = "1234567890123456" };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Email", "O n�mero de telefone n�o pode conter mais do que 15 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Logradouro_Excedendo_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Logradouro = "Logradouro com mais de 50 caracteres p/ causar erro"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Logradouro", "O logradouro pode conter no m�ximo 50 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Numero_Logradouro_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Numero = "15-A ~"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Numero", "O n�mero do logradouro � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Numero_Logradouro_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Numero = "15-A 67890"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Numero", "O n�mero do logradouro pode conter no m�ximo 10 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Complemento_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Complemento = "Mais de 20 caracteres"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Complemento", "O complemento do endere�o pode conter no m�ximo 20 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_CEP_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    CEP = "81000A00"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.CEP", "O CEP � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_CEP_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    CEP = "810000009"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.CEP", "O CEP pode conter no m�ximo 8 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Cidade_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Cidade = "Nome da Cidade !"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Cidade", "O nome da cidade � inv�lido.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_Cidade_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    Cidade = "Cidade Com Mais de Trinta carac"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.Cidade", "O nome da cidade pode conter no m�ximo 30 caracteres.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_UF_Contendo_Caracter_Invalido_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    UF = "UC"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.UF", "Sigla do estado inv�lida.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Tentar_Atualizar_Cliente_Com_UF_Excedendo_O_Tamanho_Maximo_Retorna_BadRequest()
        {
            // Prepara��o

            var cliente = new ClienteUpdateRequest
            {
                Nome = "Nome",
                Email = "email@microsoft.com",
                Endereco = new Customers.Models.Request.Endereco
                {
                    UF = "PRN"
                }
            };

            var mock = new Mock<IClienteService>();

            // Execu��o
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("Endereco.UF", "Sigla do estado inv�lida.");
            var resultado = await controller.PutCliente(Guid.NewGuid(), cliente);

            // Confer�ncia
            BadRequestObjectResult resultadoBadRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.IsType<SerializableError>(resultadoBadRequest.Value);
        }

        [Fact]
        public async Task Atualizar_Cliente_Com_Sucesso_Retorna_NoContent()
        {
            // Prepara��o
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

            var mockClienteService = new Mock<IClienteService>();
            mockClienteService.Setup((cs) => cs.CriarCliente(It.IsAny<ClienteUpdateRequest>())).Returns(Task.FromResult((201, (string)null, new Cliente
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
            })));
            // Execu��o
            var controller = new ClienteController(mockClienteService.Object);
            var resultado = await controller.PutCliente(id, cliente);

            // Confer�ncia
            NoContentResult resultadoNoContent = Assert.IsType<NoContentResult>(resultado);
            mockClienteService.Verify((service) => service.AtualizarCliente(It.IsAny<Guid>(), It.IsAny<ClienteUpdateRequest>()), Times.Exactly(1));
        }

        #endregion

        #region DELETE

        [Fact]
        public async Task Tentar_Apagar_Um_Cliente_Com_Um_Id_Inexistente_Retorna_NotFount()
        {
            // Prepara��o

            var mock = new Mock<IClienteService>();
            mock.Setup(cs => cs.ApagarCliente(It.IsAny<Guid>())).Returns(() => Task.FromResult((404, "Cliente n�o encontrado.")));

            // Execu��o
            var controller = new ClienteController(mock.Object);
            var resultado = await controller.DeleteCliente(Guid.NewGuid());

            // Confer�ncia
            StatusCodeResult resultadoBadRequest = Assert.IsType<StatusCodeResult>(resultado);
            Assert.Equal(404, resultadoBadRequest.StatusCode);
        }

        [Fact]
        public async Task Apagar_Um_Cliente_Com_Com_Sucesso_Retorna_Ok()
        {
            // Prepara��o

            var mock = new Mock<IClienteService>();
            mock.Setup(cs => cs.ApagarCliente(It.IsAny<Guid>())).Returns(() => Task.FromResult((200, (string)null)));

            // Execu��o
            var controller = new ClienteController(mock.Object);
            var resultado = await controller.DeleteCliente(Guid.NewGuid());

            // Confer�ncia
            StatusCodeResult resultadoBadRequest = Assert.IsType<StatusCodeResult>(resultado);
            Assert.Equal(200, resultadoBadRequest.StatusCode);
        }

        #endregion DELETE

        #region GET

        [Fact]
        public async Task Tentar_Recuperar_Cliente_Inexistente_Retorna_Notfound()
        {
            // Prepara��o
            var mockClientService = new Mock<IClienteService>();
            mockClientService.Setup(cs => cs.RecuperarCliente(It.IsAny<Guid>())).Returns(Task.FromResult((404, "Cliente n�o encontrado.", (Cliente)null)));

            // Execu��o
            var controller = new ClienteController(mockClientService.Object);
            var resultado = await controller.GetCliente(Guid.NewGuid());

            // Valida��o
            // Confer�ncia
            ActionResult<Cliente> resultadoAction = Assert.IsType<ActionResult<Cliente>>(resultado);
            StatusCodeResult resultadoNotFound = Assert.IsType<StatusCodeResult>(resultado.Result);
            Assert.Equal(404, resultadoNotFound.StatusCode);
        }

        [Fact]
        public async Task Recuperar_Cliente_Com_Sucesso_Retorna_Ok()
        {
            // Prepara��o
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

            var mockClientService = new Mock<IClienteService>();
            mockClientService.Setup(cs => cs.RecuperarCliente(It.IsAny<Guid>())).Returns(Task.FromResult((200, (string)null, new Cliente
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
            })));

            // Execu��o
            var controller = new ClienteController(mockClientService.Object);
            var resultado = await controller.GetCliente(Guid.NewGuid());

            // Valida��o
            // Confer�ncia
            ActionResult<Cliente> resultadoAction = Assert.IsType<ActionResult<Cliente>>(resultado);
            OkObjectResult resultadoEncontrado = Assert.IsType<OkObjectResult>(resultado.Result);
            Assert.Equal(200, resultadoEncontrado.StatusCode);
            Cliente valorRetornado = Assert.IsType<Cliente>(resultadoEncontrado.Value);
            mockClientService.Verify((service) => service.RecuperarCliente(It.IsAny<Guid>()), Times.Exactly(1));
            Assert.Equal(nome, valorRetornado.Nome);
            Assert.Equal(email, valorRetornado.Email);
            Assert.Equal(codigoArea, valorRetornado.CodigoArea);
            Assert.Equal(telefone, valorRetornado.Telefone);
            Assert.Equal(logradouro, valorRetornado.Endereco.Logradouro);
            Assert.Equal(numero, valorRetornado.Endereco.Numero);
            Assert.Equal(complemento, valorRetornado.Endereco.Complemento);
            Assert.Equal(cep, valorRetornado.Endereco.CEP);
            Assert.Equal(cidade, valorRetornado.Endereco.Cidade);
            Assert.Equal(uf, valorRetornado.Endereco.UF);
        }

        #endregion
    }
}
