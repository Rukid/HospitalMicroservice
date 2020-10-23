using System;
using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
   public class GetVisitByIdQuery : IRequest<Visit>
    {
        public Guid Id { get; set; }
    }
}
