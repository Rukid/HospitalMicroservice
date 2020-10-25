using AutoMapper;
using VisitApi.Domain;
using VisitApi.Models;

namespace VisitApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VisitModel, Visit>()
                .ForMember(x => x.VisitType, opt => opt.Ignore());
        }
    }
}