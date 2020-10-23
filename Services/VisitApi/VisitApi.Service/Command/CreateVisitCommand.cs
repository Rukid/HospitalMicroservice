using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Command
{
    public class CreateVisitCommand : IRequest<Visit>
    {
        public Visit Visit { get; set; }
    }
}
