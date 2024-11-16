using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientsRiskFactors
{
    public class PatientRiskFactory : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string IdentityNumber { get; set; }
        public int RiskScore { get; set; }
        public int? Age { get; set; }
        public Guid PatientId { get; set; }

        protected PatientRiskFactory()
        {
            IdentityNumber = string.Empty;

        }
    }
} 
