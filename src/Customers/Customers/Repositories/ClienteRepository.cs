using Clientes.Models;
using Customers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IClientesDbContext _context;

        public ClienteRepository(IClientesDbContext context)
        {
            _context = context;
        }

        public async Task ApagarCliente(Cliente cliente)
        {
            Endereco endereco = _context.Endereco.Where(e => e.Cliente.Id == cliente.Id).FirstOrDefault();

            if (endereco != null)
            {
                _context.Endereco.Remove(endereco);
            }
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            await _context.UpdateEntity<Cliente>(cliente);

            if (cliente.Endereco != null)
            {
                Endereco novasInformacoesEndereco = new Endereco
                {
                    CEP = cliente.Endereco.CEP,
                    Cidade = cliente.Endereco.Cidade,
                    Complemento = cliente.Endereco.Complemento,
                    Logradouro = cliente.Endereco.Logradouro,
                    Numero = cliente.Endereco.Numero,
                    UF = cliente.Endereco.UF
                };

                Endereco endereco = _context.Endereco.Where(e => e.Cliente.Id == cliente.Id).SingleOrDefault();

                if (endereco == null)
                {
                    await _context.Entry(cliente).ReloadAsync();
                    novasInformacoesEndereco.Id = Guid.NewGuid();
                    novasInformacoesEndereco.Cliente = cliente;
                    _context.Endereco.Add(novasInformacoesEndereco);
                }
                else
                {
                    endereco.CEP = novasInformacoesEndereco.CEP;
                    endereco.Cidade = novasInformacoesEndereco.Cidade;
                    endereco.Complemento = novasInformacoesEndereco.Complemento;
                    endereco.Logradouro = novasInformacoesEndereco.Logradouro;
                    endereco.Numero = novasInformacoesEndereco.Numero;
                    endereco.UF = novasInformacoesEndereco.UF;

                    await _context.UpdateEntity<Endereco>(endereco);
                }                
            }
            else
            {
                Endereco endereco = _context.Endereco.Where(e => e.Cliente.Id == cliente.Id).SingleOrDefault();
                if (endereco != null)
                {
                    cliente.Endereco = null;
                    _context.Endereco.Remove(endereco);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ClienteExiste(Guid guid)
        {
            return await _context.Cliente.AnyAsync((c) => c.Id == guid);
        }

        public async Task InserirCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> RecuperarCliente(Guid guid)
        {
            //return await _context.Cliente.Where(c => c.Id == guid).FirstOrDefaultAsync();
            Cliente cliente = await _context.Cliente.FindAsync(guid);

            if (cliente != null)
            {
                Endereco endereco = await _context.Endereco.Where(e => e.Cliente.Id == cliente.Id).FirstOrDefaultAsync();
            }

            return cliente;
        }
    }
}
