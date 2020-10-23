using DoctorApi.Data.Repository;
using DoctorApi.Domain.Entities;
using DoctorApi.Messaging.Send.Sender;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorApi.Service.Command
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Doctor>
    {
        private readonly IDoctorRepository _doctorRepository;   
        private readonly IDoctorUpdateSender _doctorUpdateSender;

        public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IDoctorUpdateSender doctorUpdateSender)
        {
            _doctorRepository = doctorRepository;
            _doctorUpdateSender = doctorUpdateSender;
        }

        public async Task<Doctor> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.UpdateAsync(request.Doctor);

            _doctorUpdateSender.SendDoctor(doctor);

            return doctor;
        }
    }
}
