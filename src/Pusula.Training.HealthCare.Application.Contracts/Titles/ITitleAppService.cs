﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Titles;

public interface ITitleAppService : IApplicationService
{
    Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input);
    Task<TitleDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id); // task!!
    Task<TitleDto> CreateAsync(TitleCreateDto input);
    Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(TitleExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> titleIds);
    Task DeleteAllAsync(GetTitlesInput input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}