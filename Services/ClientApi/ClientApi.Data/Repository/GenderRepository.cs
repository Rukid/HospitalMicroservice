using ClientApi.Data.Database;
using ClientApi.Domain.Entities;

namespace ClientApi.Data.Repository
{
    public class GenderRepository : Repository<GenderType>, IGenderRepository
    {
        public GenderRepository(ClientContext clientContext) : base(clientContext)
        {
        }
    }
}
