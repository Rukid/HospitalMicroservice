using DoctorApi.Domain.Entities;
using MediatR;

namespace DoctorApi.Service.Command
{
    public class CreateDoctorCommand : IRequest<Doctor>
    {
        public Doctor Doctor { get; set; }
    }
}
