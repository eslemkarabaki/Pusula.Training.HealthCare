using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using System;

namespace Pusula.Training.HealthCare.Tests
{
    public class TestDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid TestGroupId { get; set; }
        public string TestGroupName { get; set; } = null!;
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
