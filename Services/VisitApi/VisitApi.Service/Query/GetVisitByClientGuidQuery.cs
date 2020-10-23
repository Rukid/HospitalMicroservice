using System;
using System.Collections.Generic;
using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
    public class GetVisitByClientGuidQuery : IRequest<List<Visit>>
    {
        public Guid ClientId { get; set; }
    }
}