using System;
using System.IO;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocument : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string DocumentName { get; set; }
        [NotNull]
        public string DocumentPath { get; set; }
        [NotNull]
        public DateTime UploadDate { get; set; }
        public Guid RadiologyExaminationProcedureId { get; set; }

        protected RadiologyExaminationDocument()
        {
            DocumentName = string.Empty;
            DocumentPath = string.Empty;
            UploadDate = DateTime.Now;
        }

        public RadiologyExaminationDocument(Guid id, string documentName, string documentPath, DateTime uploadDate, Guid RadiologyExaminationProcedureId)
        {
            Id = id;
            Check.NotNull(documentName, nameof(documentName));
            Check.Length(documentName, nameof(documentName), RadiologyExaminationDocumentConsts.DocumentNameMaxLength, 0);
            DocumentName = documentName;
            Check.NotNull(documentPath, nameof(documentPath));
            Check.Length(documentPath, nameof(documentPath), RadiologyExaminationDocumentConsts.DocumentPathMaxLength, 0);
            DocumentPath = documentPath;
            UploadDate = uploadDate;
            RadiologyExaminationProcedureId = RadiologyExaminationProcedureId;
        }

        public string GenerateUniqueFileName(string fileName)
        {
            var uniqueName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
            return uniqueName;
        }

    }
}
