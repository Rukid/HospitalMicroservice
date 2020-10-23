using System;

namespace VisitApi.Domain
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public int VisitId { get; set; }
        public VisitType VisitType { get; set; }
        public Guid DoctorGuid { get; set; }
        public string DoctorFullName { get; set; }
        public Guid ClientGuid { get; set; }
        public string ClientFullName { get; set; }
    }
}
