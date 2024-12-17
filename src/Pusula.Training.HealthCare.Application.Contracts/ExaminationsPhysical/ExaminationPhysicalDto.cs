using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class ExaminationPhysicalDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid Id { get; set; }
        public Guid ExaminationId { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? BodyMassIndex { get; set; } // VKI
        public float? VitalAge { get; set; }      // VYA
        public float? Fever { get; set; }         // Ateş
        public float? Pulse { get; set; }         // Nabız
        public float? SystolicBloodPressure { get; set; } // KB-S
        public float? DiastolicBloodPressure { get; set; } // KB-D
        public float? SPO2 { get; set; }
        public string PhysicalNote { get; set; }
        public Guid UserId { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
    }
}