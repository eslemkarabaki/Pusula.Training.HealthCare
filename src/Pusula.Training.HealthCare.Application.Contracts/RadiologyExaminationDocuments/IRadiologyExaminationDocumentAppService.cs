using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services; 

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public interface IRadiologyExaminationDocumentAppService : IApplicationService
    {
        Task<PagedResultDto<RadiologyExaminationDocumentDto>> GetListAsync(GetRadiologyExaminationDocumentsInput input);

        Task<RadiologyExaminationDocumentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RadiologyExaminationDocumentDto> CreateAsync(RadiologyExaminationDocumentCreateDto input);

        Task<RadiologyExaminationDocumentDto> UpdateAsync(Guid id, RadiologyExaminationDocumentUpdateDto input);

        Task DeleteByIdsAsync(List<Guid> RadiologyExaminationDocumentIds);

        Task DeleteAllAsync(GetRadiologyExaminationDocumentsInput input);
    }
}
