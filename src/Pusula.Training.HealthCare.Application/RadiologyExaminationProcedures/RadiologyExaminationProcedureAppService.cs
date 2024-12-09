using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.RadiologyExaminationProcedures.Default)]
    public class RadiologyExaminationProcedureAppService(
        IRadiologyExaminationProcedureRepository radiologyExaminationProcedureRepository,
        RadiologyExaminationProcedureManager radiologyExaminationProcedureManager,
        IDistributedCache<RadiologyExaminationProcedureDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IRadiologyExaminationProcedureAppService
    {
        public virtual async Task<RadiologyExaminationProcedureDto> CreateAsync(RadiologyExaminationProcedureCreateDto input)
        {
            var radiologyExaminationProcedure = await radiologyExaminationProcedureManager.CreateAsync(
                input.Result,
                input.ResultDate,
                input.DoctorId,
                input.ProtocolId,
                input.RadiologyExaminationId
            );

            return ObjectMapper.Map<RadiologyExaminationProcedure, RadiologyExaminationProcedureDto>(radiologyExaminationProcedure);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationProcedures.Delete)]
        public virtual async Task DeleteAllAsync(GetRadiologyExaminationProceduresInput input)
        {
            await radiologyExaminationProcedureRepository.DeleteAllAsync(input.FilterText, input.Result, input.ResultDate, input.DoctorId, input.ProtocolId, input.RadiologyExaminationId);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationProcedures.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await radiologyExaminationProcedureRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationProcedures.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> RadiologyExaminationProcedureIds)
        {
            await radiologyExaminationProcedureRepository.DeleteManyAsync(RadiologyExaminationProcedureIds);
        }

        public virtual async Task<RadiologyExaminationProcedureDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RadiologyExaminationProcedure, RadiologyExaminationProcedureDto>(await radiologyExaminationProcedureRepository.GetAsync(id));
        }


        public virtual async Task<PagedResultDto<RadiologyExaminationProcedureDto>> GetListAsync(GetRadiologyExaminationProceduresInput input)
        {
            var totalCount = await radiologyExaminationProcedureRepository.GetCountAsync(input.FilterText, input.Result, input.ResultDate, input.DoctorId, input.ProtocolId, input.RadiologyExaminationId);
            var items = await radiologyExaminationProcedureRepository.GetListAsync(input.FilterText, input.Result, input.ResultDate, input.DoctorId, input.ProtocolId, input.RadiologyExaminationId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RadiologyExaminationProcedureDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RadiologyExaminationProcedure>, List<RadiologyExaminationProcedureDto>>(items)
            };
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationProcedures.Edit)]
        public virtual async Task<RadiologyExaminationProcedureDto> UpdateAsync(Guid id, RadiologyExaminationProcedureUpdateDto input)
        {
            var radiologyExaminationProcedure = await radiologyExaminationProcedureRepository.GetAsync(id);
            radiologyExaminationProcedure.Result = input.Result;
            radiologyExaminationProcedure.ResultDate = input.ResultDate;
            radiologyExaminationProcedure.DoctorId = input.DoctorId;
            radiologyExaminationProcedure.ProtocolId = input.ProtocolId;
            radiologyExaminationProcedure.RadiologyExaminationId = input.RadiologyExaminationId;

            await radiologyExaminationProcedureRepository.UpdateAsync(radiologyExaminationProcedure);

            return ObjectMapper.Map<RadiologyExaminationProcedure, RadiologyExaminationProcedureDto>(radiologyExaminationProcedure);
        }
        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new RadiologyExaminationProcedureDownloadTokenCacheItem(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
                }
            );

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
         
        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationProcedureExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await radiologyExaminationProcedureRepository.GetListAsync(input.FilterText, input.Result, input.ResultDate);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<RadiologyExaminationProcedure>, List<RadiologyExaminationProcedureDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
