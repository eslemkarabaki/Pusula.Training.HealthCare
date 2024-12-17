using Pusula.Training.HealthCare.Examinations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class ExaminationPhysicalCreateDto
    {
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
    }
}
