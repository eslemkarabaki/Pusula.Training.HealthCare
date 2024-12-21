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
        public string Path { get; set; }
        [NotNull]
        public DateTime UploadDate { get; set; }
        public Guid ItemId { get; set; }

        protected RadiologyExaminationDocument()
        { 
            Path = string.Empty;
            UploadDate = DateTime.Now;
        }

        public RadiologyExaminationDocument(Guid id, string path, DateTime uploadDate, Guid itemId)
        {
            Id = id; 
            Check.NotNull(path, nameof(path));
            Check.Length(path, nameof(path), RadiologyExaminationDocumentConsts.DocumentPathMaxLength, 0);
            Path = path;
            UploadDate = uploadDate;
            ItemId = itemId;
        }  
    }
}
