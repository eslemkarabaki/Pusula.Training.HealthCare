using System; 
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Hospitals
{
    public class HospitalDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    { 
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 
        public string[]? DepartmentNames { get; set; }  
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
