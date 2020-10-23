using VisitApi.Service.Models;

namespace VisitApi.Service.Services
{
    public interface IClientUpdateService
    {
        void UpdateClientInVisits(UpdateClientFullNameModel updateCustomerFullNameModel);
    }
}