using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.Models;
using Customers.Models;
using Customers.Models.Request;
using Customers.Services;

namespace Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteServie;

        public ClienteController(Services.IClienteService clienteService)
        {
            _clienteServie = clienteService;
        }

        // GET: api/Cliente
        [HttpGet]
        public IEnumerable<Cliente> GetCliente()
        {
            //return _context.Cliente;
            return null;
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (int statusCode, string mensagem, Cliente cliente) = await _clienteServie.RecuperarCliente(id);

            if (statusCode != StatusCodes.Status200OK)
                return StatusCode(statusCode);

            //var cliente = await _context.Cliente.FindAsync(id);

            //if (cliente == null)
            //{
            //    return NotFound();
            //}

            return Ok(cliente);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] Guid id, [FromBody] ClienteUpdateRequest cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clienteServie.AtualizarCliente(id, cliente);
            }
            catch (Exception)
            {
                // TODO logar detalhes do erro
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] ClienteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                (int statusCode, string mensagem, Cliente cliente) = await _clienteServie.CriarCliente(request);

                return CreatedAtAction(nameof(PostCliente), new { id = cliente.Id }, cliente);
            }
            catch (Exception e)
            {
                // TODO logar detalhes do erro
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (int statusCode, string mensagem) = await _clienteServie.ApagarCliente(id);

            // TODO logar mensagem
            return StatusCode(statusCode);
        }
    }
}