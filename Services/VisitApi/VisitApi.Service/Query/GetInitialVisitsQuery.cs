using System.Collections.Generic;
using MediatR;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
    public class GetInitialVisitsQuery : IRequest<List<Visit>>
    {
    }
}