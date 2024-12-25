using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Default)]
    public class RadiologyExaminationDocumentAppService(
        IRadiologyExaminationDocumentRepository radiologyExaminationDocumentRepository,
        RadiologyExaminationDocumentManager radiologyExaminationDocumentManager)
         : HealthCareAppService, IRadiologyExaminationDocumentAppService
    {

        public virtual async Task<PagedResultDto<RadiologyExaminationDocumentDto>> GetListAsync(GetRadiologyExaminationDocumentsInput input)
        {
            var totalCount = await radiologyExaminationDocumentRepository.GetCountAsync(input.FilterText, input.Path, input.UploadDate, input.ItemId);
            var items = await radiologyExaminationDocumentRepository.GetListAsync(input.FilterText, input.Path, input.UploadDate, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RadiologyExaminationDocumentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RadiologyExaminationDocument>, List<RadiologyExaminationDocumentDto>>(items)
            };
        }
        public virtual async Task<RadiologyExaminationDocumentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(await radiologyExaminationDocumentRepository.GetAsync(id));
        }

        public virtual async Task<RadiologyExaminationDocumentDto> CreateAsync(RadiologyExaminationDocumentCreateDto input)
        { 
            try
            {
                input.Path = await SaveFileAsync(input.File);

                var radiologyExaminationDocument = await radiologyExaminationDocumentManager.CreateAsync(
                    input.Path,
                    DateTime.Now,
                    input.ItemId
       
                );
                  
                return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(radiologyExaminationDocument);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(input.Path) && File.Exists(Path.Combine("wwwroot", input.Path)))
                {
                    File.Delete(Path.Combine("wwwroot", input.Path));
                }

                throw new UserFriendlyException("An error occurred while creating the document. The uploaded file has been deleted.").WithData("Exception", ex);
            }
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new UserFriendlyException("Sadece PDF, JPG ve PNG dosyaları yükleyebilirsiniz.");
            }

            const int maxFileSizeInBytes = 10 * 1024 * 1024;
            if (file.Length > maxFileSizeInBytes)
            {
                throw new UserFriendlyException("Dosya boyutu 10 MB'den büyük olamaz.");
            }

            var uniqueFileName = $"RE-{Guid.NewGuid()}{fileExtension}";

            var directoryPath = Path.Combine("wwwroot", "uploads", "radiology");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var uploadPath = Path.Combine(directoryPath, uniqueFileName);
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", "radiology", uniqueFileName);
        }
 

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Edit)]
        public virtual async Task<RadiologyExaminationDocumentDto> UpdateAsync(Guid id, RadiologyExaminationDocumentUpdateDto input)
        {
            var radiologyExaminationDocument = await radiologyExaminationDocumentRepository.GetAsync(id);


            if (input.File != null)
            {
                var newPath = await SaveFileAsync(input.File);
                radiologyExaminationDocument.Path = newPath;
            }

            radiologyExaminationDocument.UploadDate = input.UploadDate;
            radiologyExaminationDocument.ItemId = input.ItemId;

            await radiologyExaminationDocumentRepository.UpdateAsync(radiologyExaminationDocument);

            return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(radiologyExaminationDocument);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> RadiologyExaminationDocumentIds)
        {
            await radiologyExaminationDocumentRepository.DeleteManyAsync(RadiologyExaminationDocumentIds);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteAllAsync(GetRadiologyExaminationDocumentsInput input)
        {
            await radiologyExaminationDocumentRepository.DeleteAllAsync(input.FilterText, input.Path, input.UploadDate, input.ItemId);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await radiologyExaminationDocumentRepository.DeleteAsync(id);
        }


    }
}
