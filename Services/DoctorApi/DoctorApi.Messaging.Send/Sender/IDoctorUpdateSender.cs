using DoctorApi.Domain.Entities;

namespace DoctorApi.Messaging.Send.Sender
{
    public interface IDoctorUpdateSender
    {
        void SendDoctor(Doctor doctor);
    }
}
