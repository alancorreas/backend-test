using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Clientes.Models
{
    public interface IClientesDbContext
    {
        DbSet<Customers.Models.Cliente> Cliente { get; set; }
        DbSet<Customers.Models.Endereco> Endereco { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry Entry(object entity);

        Task UpdateEntity<T>(T entity) where T : class;
    }
}