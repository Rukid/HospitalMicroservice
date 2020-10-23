using DoctorApi.Data.Repository;
using DoctorApi.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorApi.Service.Command
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Doctor>
    {
        private readonly IDoctorRepository _doctorRepository;

        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.AddAsync(request.Doctor);
        }
    }
}
