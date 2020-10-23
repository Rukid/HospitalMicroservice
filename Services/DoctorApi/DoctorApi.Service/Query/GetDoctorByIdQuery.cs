using DoctorApi.Domain.Entities;
using MediatR;
using System;

namespace DoctorApi.Service.Query
{
    public class GetDoctorByIdQuery : IRequest<Doctor>
    {
        public Guid Id { get; set; }
    }
}
