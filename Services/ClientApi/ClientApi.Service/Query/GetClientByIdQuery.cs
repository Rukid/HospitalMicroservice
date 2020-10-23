using ClientApi.Domain.Entities;
using MediatR;
using System;

namespace ClientApi.Service.Query
{
    public class GetClientByIdQuery : IRequest<Client>
    {
        public Guid Id { get; set; }
    }
}
