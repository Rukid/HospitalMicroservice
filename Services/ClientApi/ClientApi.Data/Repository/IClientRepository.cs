using ClientApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApi.Data.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClientByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
