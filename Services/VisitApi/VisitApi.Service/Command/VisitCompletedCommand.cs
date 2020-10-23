using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Command
{
   public class VisitCompletedCommand : IRequest<Visit>
    {
        public Visit Visit { get; set; }
    }
}
