using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using System;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class Diagnosis : FullAuditedAggregateRoot<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Diagnosis(Guid id, string code, string name) : base(id)
        {
            SetCode(code); SetName(name); 
        }

        public void  SetCode(string code)
        {
            Code = Check.NotNullOrWhiteSpace(code, nameof(code), DiagnosisConsts.CodeMaxLength);
           
        }
        public void  SetName(string name)
        {
           Name = Check.NotNullOrWhiteSpace(name, nameof(name), DiagnosisConsts.NameMaxLength);
        }
    }
}
