using AutoMapper;
using ClientApi.Domain.Entities;
using ClientApi.Models;

namespace ClientApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateClientModel, Client>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Gender, opt => opt.Ignore());

            CreateMap<UpdateClientModel, Client>()
                .ForMember(x => x.Gender, opt => opt.Ignore());
        }
    }
}
