using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public string Nome { get; set; }

        [MaxLength(40)]
        public string Email { get; set; }

        public int? CodigoArea { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        public Endereco Endereco { get; set; }
    }
}
