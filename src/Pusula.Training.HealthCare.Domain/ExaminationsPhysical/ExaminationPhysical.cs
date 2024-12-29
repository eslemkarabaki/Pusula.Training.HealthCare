using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class ExaminationPhysical : AuditedEntity<Guid>  
    {
        public Guid ExaminationId { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; } 
        public float? BodyMassIndex { get; set; } // VKI
        public float? VitalAge { get; set; }      // VYA
        public float? Fever { get; set; }         // Ateş
        public float? Pulse { get; set; } // Nabız
        public float? SystolicBloodPressure { get; set; } // KB-S
        public float? DiastolicBloodPressure { get; set; } // KB-D
        public float? SPO2 { get; set; }
        public string PhysicalNote { get; set; }

        // Navigation Property
        public Guid UserId { get; set; }

        protected ExaminationPhysical()
        {
            Weight = null;
            Height = null;
            BodyMassIndex = null;
            VitalAge = null;
            Fever = null;
            Pulse = null;
            SystolicBloodPressure = null;
            DiastolicBloodPressure = null;
            SPO2 = null;
            PhysicalNote = string.Empty; // Boş string başlangıç değeri
        }

        public ExaminationPhysical(
            Guid id,
            Guid examinationId,
            float? weight,
            float? height,
            float? bodyMassIndex,
            float? vitalAge,
            float? fever,
            float? pulse,
            float? systolicBloodPressure,
            float? diastolicBloodPressure,
            float? sPO2,
            string physicalNote)
        {
            // ID ve gerekli kontroller
            Check.NotDefaultOrNull<Guid>(id, nameof(id));
            Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
            Check.Length(physicalNote, nameof(physicalNote), ExaminationPhysicalConsts.PhysicalNoteMaxLength, 0);

            // Atamalar
            Id = id;
            ExaminationId = examinationId;
            Weight = weight;
            Height = height;
            BodyMassIndex = bodyMassIndex; // VKI
            VitalAge = vitalAge; // VYA
            Fever = fever; // Ateş
            Pulse = pulse;
            SystolicBloodPressure = systolicBloodPressure; // KB-S
            DiastolicBloodPressure = diastolicBloodPressure; // KB-D
            SPO2 = sPO2;
            PhysicalNote = physicalNote ?? string.Empty; // Null kontrolü
        }
    }
}
