using AutoMapper;
using DoctorApi.Domain.Entities;
using DoctorApi.Models;

namespace DoctorApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDoctorModel, Doctor>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateDoctorModel, Doctor>();
        }
    }
}
