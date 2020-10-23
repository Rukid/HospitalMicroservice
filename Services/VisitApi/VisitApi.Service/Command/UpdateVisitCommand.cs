using System.Collections.Generic;
using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Command
{
    public class UpdateVisitCommand : IRequest
    {
        public List<Visit> Visits { get; set; }
    }
}