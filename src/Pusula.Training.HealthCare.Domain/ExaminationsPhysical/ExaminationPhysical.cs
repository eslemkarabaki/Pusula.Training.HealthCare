using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationPhysical : FullAuditedAggregateRoot<Guid>
    {
        public virtual string IdentityNumber { get; set; }
        public Guid ExaminationId { get; set; }
        [NotNull]
        public virtual int Weight { get; set; } // Kilo
        public virtual int Height { get; set; } //Boy
        public virtual int Faver { get; set; } // Ateş
        public virtual int Pulse { get; set; } // Nabız
        public string WeightAsString => Weight.ToString();
        public string HeightAsString => Height.ToString();
        public string FaverAsString => Faver.ToString();
        public string PulseAsString => Pulse.ToString();


        public virtual string GetIntsAsString()
        {
            return $"{WeightAsString}-{HeightAsString}-{FaverAsString}-{PulseAsString}";
        }

        protected ExaminationPhysical()
        {
            IdentityNumber = string.Empty;
        }
        public ExaminationPhysical(Guid id, Guid examinationId, int weight, int height, int faver, int pulse, string identityNumber, string complaint, string history, DateTime startDate)
        {
            Check.NotDefaultOrNull<Guid>(id, nameof(id));
            Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
            Check.NotNull(identityNumber, nameof(identityNumber));
            Check.Length(identityNumber, nameof(identityNumber), ExaminationPyhsicalConsts.IdentityNumberMaxLength, 0);

            Check.Range(height, nameof(height), 0, ExaminationPyhsicalConsts.WeightMaxLength);
            Check.Range(weight, nameof(weight), 0, ExaminationPyhsicalConsts.WeightMaxLength);
            Check.Range(faver, nameof(faver), 0, ExaminationPyhsicalConsts.WeightMaxLength);
            Check.Range(pulse, nameof(pulse), 0, ExaminationPyhsicalConsts.WeightMaxLength);




            Id = id;
            Weight = weight;
            Height = height;
            Faver = faver;
            Pulse = pulse;
            ExaminationId = examinationId;
            IdentityNumber = identityNumber;


        }

    }
}
