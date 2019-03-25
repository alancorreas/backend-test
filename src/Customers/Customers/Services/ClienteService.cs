using Clientes.Models;
using Customers.Models;
using Customers.Models.Request;
using Customers.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<(int statusCode, string mensagem, Cliente cliente)> CriarCliente(ClienteRequest cliente)
        {            
            var novoCliente = new Cliente
            {
                CodigoArea = cliente.CodigoArea,
                Email = cliente.Email,
                Id = Guid.NewGuid(),
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
            };

            if (cliente.Endereco != null)
                novoCliente.Endereco = new Models.Endereco
                {
                    CEP = cliente.Endereco.CEP,
                    Cidade = cliente.Endereco.Cidade,
                    Complemento = cliente.Endereco.Complemento,
                    Id = Guid.NewGuid(),
                    Logradouro = cliente.Endereco.Logradouro,
                    Numero = cliente.Endereco.Numero,
                    UF = cliente.Endereco.UF
                };

            await _clienteRepository.InserirCliente(novoCliente);

            return (StatusCodes.Status201Created, (string)null, novoCliente);
        }

        public async Task<(int statusCode, string mensagem)> AtualizarCliente(Guid id, ClienteUpdateRequest cliente)
        {
            if (id != cliente.Id)
            {
                // TODO Utilizar localizer
                return (StatusCodes.Status400BadRequest, "O id de busca não pode ser diferente do id do objeto.");
            }

            var clienteAtualizado = new Cliente
            {
                CodigoArea = cliente.CodigoArea,
                Email = cliente.Email,
                Id = id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
            };

            if (cliente.Endereco != null)
                clienteAtualizado.Endereco = new Models.Endereco
                {
                    CEP = cliente.Endereco.CEP,
                    Cidade = cliente.Endereco.Cidade,
                    Complemento = cliente.Endereco.Complemento,
                    //Id = cliente.Endereco.Id,
                    Logradouro = cliente.Endereco.Logradouro,
                    Numero = cliente.Endereco.Numero,
                    UF = cliente.Endereco.UF
                };

            try
            {
                await _clienteRepository.AtualizarCliente(clienteAtualizado);
            }
            catch (DbUpdateConcurrencyException)
            {
                // TODO logar informações do erro
                if (!(await _clienteRepository.ClienteExiste(cliente.Id)))
                    return (StatusCodes.Status404NotFound, "Cliente não encontrado.");

                throw;
            }

            return (StatusCodes.Status204NoContent, (string)null);
        }

        public async Task<(int statusCode, string mensagem)> ApagarCliente(Guid id)
        {
            Cliente cliente = await _clienteRepository.RecuperarCliente(id);

            if (cliente == null)
                return (StatusCodes.Status404NotFound, "Cliente não encontrado.");

            await _clienteRepository.ApagarCliente(cliente);

            return (StatusCodes.Status200OK, null);
        }

        public async Task<(int statusCode, string mensagem, Cliente cliente)> RecuperarCliente(Guid id)
        {
            Cliente cliente = await _clienteRepository.RecuperarCliente(id);

            if (cliente == null)
                return (StatusCodes.Status404NotFound, "Cliente não encontrado", null);

            return (StatusCodes.Status200OK, null, cliente);
        }
    }
}
