using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace Pusula.Training.HealthCare.Examinations;
public class ExaminationAnamnezDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid Id { get; set; }
    public Guid ExaminationId { get; set; }
    public string IdentityNumber { get; set; }
    public string Complaint { get; set; }
    public string History { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime Date { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}
