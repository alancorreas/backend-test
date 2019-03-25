using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models.Request
{
    public class ClienteRequest
    {
        [RegularExpression(@"^[A-Za-zãáâéêóôõÃÁÂÉÊÓÔÕ]+$", ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_NAME), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(20, ErrorMessageResourceName = nameof(Resources.SharedResources.NAME_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.SharedResources.NAME_IS_MANDATORY), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.SharedResources.EMAIL_IS_MANDATORY), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_EMAIL), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(40, ErrorMessageResourceName = nameof(Resources.SharedResources.EMAIL_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Email { get; set; }

        // Neste momento fiz uma validação mais simples. Uma melhoria seria utilizar uma regex com todos os códigos de área suportados
        [Range(11, 99, ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_AREA_CODE), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public int? CodigoArea { get; set; }

        [Phone(ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_PHONE_NUMBER), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(15, ErrorMessageResourceName = nameof(Resources.SharedResources.PHONE_NUMBER_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Telefone { get; set; }

        public Endereco Endereco { get; set; }
    }
}
