using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Models.Request
{
    public class ClienteUpdateRequest : ClienteRequest
    {
        public Guid Id { get; set; }
    }
}
