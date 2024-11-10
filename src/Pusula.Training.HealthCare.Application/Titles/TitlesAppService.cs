using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Titles
{
    public class TitlesAppService : ApplicationService, ITitlesAppService
    {
        private readonly IRepository<Title, Guid> _titleRepository;

        public TitlesAppService(IRepository<Title, Guid> titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            var query = await _titleRepository.GetListAsync();
            var titleDtos = ObjectMapper.Map<List<Title>, List<TitleDto>>(query);
            return new PagedResultDto<TitleDto>(query.Count, titleDtos);
        }

        public async Task<TitleDto> GetAsync(Guid id)
        {
            var title = await _titleRepository.GetAsync(id);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Silmek için id'ye sahip başlığı alıyoruz
            var title = await _titleRepository.GetAsync(id);

            // Başlık varsa, silme işlemi yapılıyor
            if (title != null)
            {
                await _titleRepository.DeleteAsync(title);
            }
        }

        public async Task<TitleDto> CreateAsync(TitleCreateDto input)
        {
            var title = new Title(Guid.NewGuid(), input.Name);
            await _titleRepository.InsertAsync(title);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {
            var title = await _titleRepository.GetAsync(id);
            title.Name = input.Name; // 'Name' alanını güncelle
            await _titleRepository.UpdateAsync(title);
            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TitleExcelDownloadDto input)
        {
            return null;  // Excel dosyasını oluşturmak için implementasyon eklenebilir.
        }

        public async Task DeleteByIdsAsync(List<Guid> titleIds)
        {
            foreach (var id in titleIds)
            {
                var title = await _titleRepository.GetAsync(id);
                await _titleRepository.DeleteAsync(title);
            }
        }

        public async Task DeleteAllAsync(GetTitlesInput input)
        {
            var titles = await _titleRepository.GetListAsync();
            foreach (var title in titles)
            {
                await _titleRepository.DeleteAsync(title);
            }
        }

        public async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return new Shared.DownloadTokenResultDto { Token = "sample-token" };
        }

        void ITitlesAppService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TitleDto> UpdateAsync(TitleUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }

}
