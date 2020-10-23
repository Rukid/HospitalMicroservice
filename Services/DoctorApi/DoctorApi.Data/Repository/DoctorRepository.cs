using DoctorApi.Data.Database;
using DoctorApi.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DoctorApi.Data.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DoctorContext doctorContext) : base(doctorContext)
        {
        }

        public async Task<Doctor> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DoctorContext.Doctors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
