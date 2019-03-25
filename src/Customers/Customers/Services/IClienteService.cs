using Clientes.Models;
using Customers.Models;
using Customers.Models.Request;
using System;
using System.Threading.Tasks;

namespace Customers.Services
{
    public interface IClienteService
    {
        Task<(int statusCode, string mensagem, Cliente cliente)> CriarCliente(ClienteRequest cliente);
        Task<(int statusCode, string mensagem)> AtualizarCliente(Guid id, ClienteUpdateRequest cliente);
        Task<(int statusCode, string mensagem)> ApagarCliente(Guid id);
        Task<(int statusCode, string mensagem, Cliente cliente)> RecuperarCliente(Guid id);
    }
}