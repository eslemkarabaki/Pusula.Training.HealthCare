using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;  
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
            var totalCount = await radiologyExaminationDocumentRepository.GetCountAsync(input.FilterText, input.DocumentName, input.DocumentPath, input.UploadDate, input.RadiologyExaminationProcedureId);
            var items = await radiologyExaminationDocumentRepository.GetListAsync(input.FilterText, input.DocumentName, input.DocumentPath, input.UploadDate, input.RadiologyExaminationProcedureId, input.Sorting, input.MaxResultCount, input.SkipCount);

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
            string documentPath = null;
            try
            {
                documentPath = await UploadDocumentAsync(input.File, input.RadiologyExaminationProcedureId);

                var radiologyExaminationDocument = await radiologyExaminationDocumentManager.CreateAsync(
                    input.DocumentName,
                    documentPath,
                    DateTime.Now,
                    input.RadiologyExaminationProcedureId
                );

                return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(radiologyExaminationDocument);
            }
            catch (Exception ex)
            { 
                if (!string.IsNullOrEmpty(documentPath) && File.Exists(Path.Combine("wwwroot", documentPath)))
                {
                    File.Delete(Path.Combine("wwwroot", documentPath));
                }

                throw new UserFriendlyException("An error occurred while creating the document. The uploaded file has been deleted.").WithData("Exception", ex);
            }
        }


        //private async Task<string> UploadDocumentAsync(IFormFile file)
        //{
        //    var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
        //    var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //    if (!allowedExtensions.Contains(fileExtension))
        //    {
        //        throw new UserFriendlyException("Sadece PDF, JPG ve PNG dosyaları yükleyebilirsiniz.");
        //    }

        //    const int maxFileSizeInBytes = 10 * 1024 * 1024;

        //    if (file.Length > maxFileSizeInBytes)
        //    {
        //        throw new UserFriendlyException("Dosya boyutu 10 MB'den büyük olamaz.");
        //    }

        //    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

        //    var uploadDirectory = Path.Combine("wwwroot", "uploads");
        //    if (!Directory.Exists(uploadDirectory))
        //    {
        //        Directory.CreateDirectory(uploadDirectory);
        //    }

        //    var uploadPath = Path.Combine(uploadDirectory, uniqueFileName);
        //    using (var stream = new FileStream(uploadPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return Path.Combine("uploads", uniqueFileName); 
        //}

        private async Task<string> UploadDocumentAsync(IFormFile file, Guid procedureId)
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
             
            var directoryPath = Path.Combine("wwwroot", "uploads", "radiology", procedureId.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);  
            }
             
            var uploadPath = Path.Combine(directoryPath, uniqueFileName);
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
             
            return Path.Combine("uploads", "radiology", procedureId.ToString(), uniqueFileName);
        }


        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Edit)]
        public virtual async Task<RadiologyExaminationDocumentDto> UpdateAsync(Guid id, RadiologyExaminationDocumentUpdateDto input)
        { 
            var radiologyExaminationDocument = await radiologyExaminationDocumentRepository.GetAsync(id);
             
            radiologyExaminationDocument.DocumentName = input.DocumentName;
             
            if (input.File != null)
            {
                var newDocumentPath = await UploadDocumentAsync(input.File, input.RadiologyExaminationProcedureId);
                radiologyExaminationDocument.DocumentPath = newDocumentPath;
            }
             
            radiologyExaminationDocument.UploadDate = input.UploadDate;
            radiologyExaminationDocument.RadiologyExaminationProcedureId = input.RadiologyExaminationProcedureId;
             
            await radiologyExaminationDocumentRepository.UpdateAsync(radiologyExaminationDocument);
             
            return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(radiologyExaminationDocument);
        }

        //public virtual async Task<RadiologyExaminationDocumentDto> UpdateAsync(Guid id, RadiologyExaminationDocumentUpdateDto input)
        //{
        //    var radiologyExaminationDocument = await radiologyExaminationDocumentRepository.GetAsync(id);
        //    radiologyExaminationDocument.DocumentName = input.DocumentName;
        //    radiologyExaminationDocument.DocumentPath = input.DocumentPath;
        //    radiologyExaminationDocument.UploadDate = input.UploadDate;
        //    radiologyExaminationDocument.RadiologyExaminationProcedureId = input.RadiologyExaminationProcedureId;

        //    await radiologyExaminationDocumentRepository.UpdateAsync(radiologyExaminationDocument);
        //    return ObjectMapper.Map<RadiologyExaminationDocument, RadiologyExaminationDocumentDto>(radiologyExaminationDocument);
        //}

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> RadiologyExaminationDocumentIds)
        {
            await radiologyExaminationDocumentRepository.DeleteManyAsync(RadiologyExaminationDocumentIds);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteAllAsync(GetRadiologyExaminationDocumentsInput input)
        {
            await radiologyExaminationDocumentRepository.DeleteAllAsync(input.FilterText, input.DocumentName, input.DocumentPath, input.UploadDate, input.RadiologyExaminationProcedureId);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationDocuments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await radiologyExaminationDocumentRepository.DeleteAsync(id);
        }
         

    }
}
