using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models
{
    public class Endereco
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Logradouro { get; set; }

        [MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(20)]
        public string Complemento { get; set; }

        [MaxLength(8)]
        public string CEP { get; set; }

        [MaxLength(30)]
        public string Cidade { get; set; }

        [MaxLength(2)]
        public string UF { get; set; }

        [ForeignKey("ClienteForeignKey")]
        [JsonIgnore]
        public Cliente Cliente { get; set; }

        public Guid ClienteForeignKey { get; set; }
    }
}
