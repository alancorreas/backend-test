using Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Repositories
{
    public interface IClienteRepository
    {
        Task InserirCliente(Cliente cliente);
        Task AtualizarCliente(Cliente clienteAtualizado);
        Task<bool> ClienteExiste(Guid guid);
        Task<Cliente> RecuperarCliente(Guid guid);
        Task ApagarCliente(Cliente guid);
    }
}
