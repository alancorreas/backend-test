using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models.Request
{
    public class Endereco
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessageResourceName = nameof(Resources.SharedResources.ADDRESS_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Logradouro { get; set; }

        [RegularExpression(@"^[0-9A-Za-z \-]+$", ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_ADDRESS_NUMBER), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(10, ErrorMessageResourceName = nameof(Resources.SharedResources.ADDRESS_NUMBER_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Numero { get; set; }

        [StringLength(20, ErrorMessageResourceName = nameof(Resources.SharedResources.ADDRESS_COMPLEMENT_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Complemento { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_ZIP_CODE), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(8, ErrorMessageResourceName = nameof(Resources.SharedResources.ZIP_CODE_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string CEP { get; set; }

        [RegularExpression(@"^[A-za-zãáâéêóôõÃÁÂÉÊÓÔÕ ]+$", ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_CITY_NAME), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        [StringLength(30, ErrorMessageResourceName = nameof(Resources.SharedResources.CITY_NAME_IS_TOO_LONG), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string Cidade { get; set; }

        [RegularExpression(@"^(AC|AL|AM|AP|BA|CE|DF|GO|ES|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RO|RR|RS|SC|SE|SP|TO)$", ErrorMessageResourceName = nameof(Resources.SharedResources.INVALID_STATE_CODE), ErrorMessageResourceType = typeof(Resources.SharedResources))]
        public string UF { get; set; }
    }
}
