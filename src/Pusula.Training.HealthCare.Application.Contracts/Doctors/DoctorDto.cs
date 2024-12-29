using System;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int AppointmentTime { get; set; }
    public Guid TitleId { get; set; }
    public TitleDto Title { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid UserId { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}