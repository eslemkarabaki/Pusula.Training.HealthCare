using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string DocumentName { get; set; } = null!;
        public string DocumentPath { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public Guid RadiologyExaminationProcedureId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}
