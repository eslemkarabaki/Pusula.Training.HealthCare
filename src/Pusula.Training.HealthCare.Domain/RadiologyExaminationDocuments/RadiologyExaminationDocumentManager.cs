using JetBrains.Annotations;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using System.IO;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentManager(IRadiologyExaminationDocumentRepository radiologyExaminationDocumentRepository) : DomainService
    {
        public virtual async Task<RadiologyExaminationDocument> CreateAsync(
            string documentName,
            string documentPath,
            DateTime uploadDate,
            Guid RadiologyExaminationProcedureId
           )
        {
            Check.NotNullOrWhiteSpace(documentName, nameof(documentName));
            Check.Length(documentName, nameof(documentName), RadiologyExaminationDocumentConsts.DocumentNameMaxLength);
            Check.NotNullOrWhiteSpace(documentPath, nameof(documentPath));
            Check.Length(documentPath, nameof(documentPath), RadiologyExaminationDocumentConsts.DocumentPathMaxLength);

            var radiologyExaminationDocument = new RadiologyExaminationDocument(
            GuidGenerator.Create(),
            documentName,
            documentPath,
            uploadDate,
            RadiologyExaminationProcedureId
             );

            var uniqueFileName = radiologyExaminationDocument.GenerateUniqueFileName(documentName);
            var updatedPath = Path.Combine("uploads", uniqueFileName);  

            return await radiologyExaminationDocumentRepository.InsertAsync(radiologyExaminationDocument);
        }

        public virtual async Task<RadiologyExaminationDocument> UpdateAsync(
            Guid id,
            string documentName,
            string documentPath,
            DateTime uploadDate,
            Guid RadiologyExaminationProcedureId,
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(documentName, nameof(documentName));
            Check.Length(documentName, nameof(documentName), RadiologyExaminationDocumentConsts.DocumentNameMaxLength);
            Check.NotNullOrWhiteSpace(documentPath, nameof(documentPath));
            Check.Length(documentPath, nameof(documentPath), RadiologyExaminationDocumentConsts.DocumentPathMaxLength);

            var RadiologyExaminationDocument = await radiologyExaminationDocumentRepository.FindAsync(id);

            RadiologyExaminationDocument!.DocumentName = documentName;
            RadiologyExaminationDocument.DocumentPath = documentPath;
            RadiologyExaminationDocument.UploadDate = uploadDate;
            RadiologyExaminationDocument.RadiologyExaminationProcedureId = RadiologyExaminationProcedureId;

            return await radiologyExaminationDocumentRepository.UpdateAsync(RadiologyExaminationDocument);
        }
    }
}
