using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Examinations
{
    public class PhysicialExaminationFinding:Entity<Guid>
    {
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? BodyMassIndex { get; set; } // VKI
        public float? VitalAge { get; set; }      // VYA
        public float? Fever { get; set; }         // Ateş
        public float? Pulse { get; set; } // Nabız
        public float? SystolicBloodPressure { get; set; } // KB-S
        public float? DiastolicBloodPressure { get; set; } // KB-D
        public float? SPO2 { get; set; }
        public Guid AnamnesisId { get; set; }

    }
}
