using DoctorApi.Data.Repository;
using DoctorApi.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorApi.Service.Query
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Doctor>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorByIdAsync(request.Id, cancellationToken);
        }
    }
}
