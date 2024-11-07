using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string WorkingHours { get; set; } = null!;
    public Guid TitleId { get; set; } 
    public Guid DepartmentId { get; set; } 
    public Guid HospitalId { get; set; } 

    public string ConcurrencyStamp { get; set; } = null!;
}
