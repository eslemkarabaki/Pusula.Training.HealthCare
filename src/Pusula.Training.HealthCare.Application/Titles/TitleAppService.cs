using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Titles
{
    public class TitleAppService(ITitleRepository titleRepository,TitleManager titleManager) : ApplicationService, ITitleAppService
    {
        public async Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            var items = await titleRepository.GetListAsync(input.FilterText, input.Name,input.Sorting, input.MaxResultCount, input.SkipCount);
            var count = await titleRepository.GetCountAsync(input.FilterText, input.Name,input.Sorting, input.MaxResultCount, input.SkipCount);
            return new PagedResultDto<TitleDto>(count, ObjectMapper.Map<List<Title>, List<TitleDto>>(items));
        }

        public async Task<TitleDto> GetAsync(Guid id)
        {
            var title = await titleRepository.GetAsync(id);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task DeleteAsync(Guid id)
        {
           await titleRepository.DeleteAsync(id);
        }

        public async Task<TitleDto> CreateAsync(TitleCreateDto input)
        {
            var title= await titleManager.CreateAsync(input.Name);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {
           var title = await titleManager.UpdateAsync(id, input.Name);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TitleExcelDownloadDto input)
        {
            await Task.CompletedTask; // Excel dosyasını oluşturmak için implementasyon eklenebilir.
            return default;
        }

        public async Task DeleteByIdsAsync(List<Guid> titleIds)
        {
            await titleRepository.DeleteManyAsync(titleIds);
        }

        public async Task DeleteAllAsync(GetTitlesInput input)
        {
            await titleRepository.DeleteAllAsync(input.FilterText, input.Name);
        }

        public async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return new Shared.DownloadTokenResultDto { Token = "sample-token" };
        }
    }

}
