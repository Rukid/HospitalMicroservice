using ClientApi.Data.Database;
using ClientApi.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ClientApi.Data.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ClientContext clientContext) : base(clientContext)
        {
        }

        public async Task<Client> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await ClientContext.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
