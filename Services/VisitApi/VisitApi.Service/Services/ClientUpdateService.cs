using System;
using System.Diagnostics;
using MediatR;
using VisitApi.Service.Command;
using VisitApi.Service.Models;
using VisitApi.Service.Query;

namespace VisitApi.Service.Services
{
    public class ClientUpdateService : IClientUpdateService
    {
        private readonly IMediator _mediator;

        public ClientUpdateService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void UpdateClientInVisits(UpdateClientFullNameModel updateCustomerFullNameModel)
        {
            try
            {
                var clientVisits = await _mediator.Send(new GetVisitByClientGuidQuery
                {
                    ClientId = updateCustomerFullNameModel.Id
                });

                if (clientVisits.Count != 0)
                {
                    clientVisits.ForEach(x => x.ClientFullName = $"{updateCustomerFullNameModel.FirstName} {updateCustomerFullNameModel.LastName}");
                }

                await _mediator.Send(new UpdateVisitCommand
                {
                    Visits = clientVisits
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}