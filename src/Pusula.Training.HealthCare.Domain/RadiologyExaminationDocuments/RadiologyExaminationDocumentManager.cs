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
            string path,
            DateTime uploadDate,
            Guid itemId
           )
        {
            Check.NotNullOrWhiteSpace(path, nameof(path));
            Check.Length(path, nameof(path), RadiologyExaminationDocumentConsts.DocumentPathMaxLength);

            try
            {
                var radiologyExaminationDocument = new RadiologyExaminationDocument(
                    GuidGenerator.Create(),
                    path,
                    uploadDate,
                    itemId
                );

                return await radiologyExaminationDocumentRepository.InsertAsync(radiologyExaminationDocument);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error: {ex.Message}");
                throw new UserFriendlyException("An error occurred while creating the radiology examination document.");
            }
        }

        public virtual async Task<RadiologyExaminationDocument> UpdateAsync(
            Guid id,
            string path,
            DateTime uploadDate,
            Guid itemId,
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(path, nameof(path));
            Check.Length(path, nameof(path), RadiologyExaminationDocumentConsts.DocumentPathMaxLength);

            try
            {
                var radiologyExaminationDocument = await radiologyExaminationDocumentRepository.FindAsync(id);

                radiologyExaminationDocument.Path = path;
                radiologyExaminationDocument.UploadDate = uploadDate;
                radiologyExaminationDocument.ItemId = itemId;

                return await radiologyExaminationDocumentRepository.UpdateAsync(radiologyExaminationDocument);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error: {ex.Message}");
                throw new UserFriendlyException("An error occurred while updating the radiology examination document.");
            }
        }
    }

}
