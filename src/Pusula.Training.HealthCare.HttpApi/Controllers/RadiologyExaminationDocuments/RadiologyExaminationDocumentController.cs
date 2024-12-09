using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos; 

namespace Pusula.Training.HealthCare.Controllers.RadiologyExaminationDocuments
{
    [RemoteService]
    [Area("app")]
    [ControllerName("RadiologyExaminationDocument")]
    [Route("api/healthcare/radiology-examination-documents")]
    public class RadiologyExaminationDocumentController : HealthCareController , IRadiologyExaminationDocumentAppService
    {
        protected IRadiologyExaminationDocumentAppService _radiologyExaminationDocumentAppService;

        public RadiologyExaminationDocumentController(IRadiologyExaminationDocumentAppService radiologyExaminationDocumentAppService)
        {
            _radiologyExaminationDocumentAppService = radiologyExaminationDocumentAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RadiologyExaminationDocumentDto>> GetListAsync(GetRadiologyExaminationDocumentsInput input)
        {
            return _radiologyExaminationDocumentAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationDocumentDto> GetAsync(Guid id)
        {
            return _radiologyExaminationDocumentAppService.GetAsync(id);
        } 
   
        [HttpPost]
        public virtual Task<RadiologyExaminationDocumentDto> CreateAsync([FromForm] RadiologyExaminationDocumentCreateDto input)
        {
            return _radiologyExaminationDocumentAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationDocumentDto> UpdateAsync(Guid id, RadiologyExaminationDocumentUpdateDto input)
        {
            return _radiologyExaminationDocumentAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _radiologyExaminationDocumentAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("deleteByIds")]
        public virtual Task DeleteByIdsAsync(List<Guid> RadiologyExaminationDocumentIds)
        {
            return _radiologyExaminationDocumentAppService.DeleteByIdsAsync(RadiologyExaminationDocumentIds);
        }

        [HttpDelete]
        [Route("deleteAll")]
        public virtual Task DeleteAllAsync(GetRadiologyExaminationDocumentsInput input)
        {
            return _radiologyExaminationDocumentAppService.DeleteAllAsync(input);
        }
         
    }
}
