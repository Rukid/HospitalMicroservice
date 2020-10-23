using DoctorApi.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorApi.Data.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
