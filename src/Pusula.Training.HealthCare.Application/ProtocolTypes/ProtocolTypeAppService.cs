using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;


namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolTypeAppService : ApplicationService, IProtocolTypeAppService
{
    private readonly IProtocolTypeRepository _protocolTypeRepository;
    private readonly ProtocolTypeManager _protocolTypeManager;

    public ProtocolTypeAppService(IProtocolTypeRepository protocolTypeRepository, ProtocolTypeManager protocolTypeManager)
    {
        _protocolTypeRepository = protocolTypeRepository;
        _protocolTypeManager = protocolTypeManager;
    }

    public async Task<PagedResultDto<ProtocolTypeDto>> GetListAsync(GetProtocolTypeInput input)
    {
        var queryable = await _protocolTypeRepository.GetQueryableAsync();

        // Sýralama iþlemi
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            queryable = queryable.OrderBy(input.Sorting); // Sorting parametresine göre sýralama yapýyoruz
        }

        // Sayfalama iþlemi
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var items = await AsyncExecuter.ToListAsync(queryable.Skip(input.SkipCount).Take(input.MaxResultCount));

        return new PagedResultDto<ProtocolTypeDto>(
            totalCount,
            ObjectMapper.Map<List<ProtocolType>, List<ProtocolTypeDto>>(items)
        );
    }

    public async Task<ProtocolTypeDto> GetAsync(Guid id)
    {
        var protocolType = await _protocolTypeRepository.GetAsync(id);
        return ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(protocolType);
    }

    public async Task<ProtocolTypeDto> CreateAsync(ProtocolTypeCreateDto input)
    {
        var protocolType = await _protocolTypeManager.CreateAsync(GuidGenerator.Create(), input.Name);
        return ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(protocolType);
    }

    public async Task<ProtocolTypeDto> UpdateAsync(Guid id, ProtocolTypeUpdateDto input)
    {
        var protocolType = await _protocolTypeRepository.GetAsync(id);
        protocolType.SetName(input.Name);
        await _protocolTypeRepository.UpdateAsync(protocolType);
        return ObjectMapper.Map<ProtocolType, ProtocolTypeDto>(protocolType);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _protocolTypeRepository.DeleteAsync(id);
    }
}
