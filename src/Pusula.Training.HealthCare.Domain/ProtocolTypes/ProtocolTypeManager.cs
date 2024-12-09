using Pusula.Training.HealthCare.ProtocolTypes;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.ProtocolTypes;

    public class ProtocolTypeManager : DomainService
    {
        private readonly IProtocolTypeRepository _protocolTypeRepository;

        public ProtocolTypeManager(IProtocolTypeRepository protocolTypeRepository)
        {
            _protocolTypeRepository = protocolTypeRepository;
        }

        public async Task<ProtocolType> CreateAsync(Guid id, string name)
        {
            
            if (await _protocolTypeRepository.FindByNameAsync(name) != null)
            {
                throw new BusinessException("ProtocolTypeNameAlreadyExists")
                    .WithData("Name", name);
            }

            var protocolType = new ProtocolType(id, name);
            return await _protocolTypeRepository.InsertAsync(protocolType);
        }

        public async Task ChangeNameAsync(Guid id, string newName)
        {
            var protocolType = await _protocolTypeRepository.GetAsync(id);

            protocolType.SetName(newName);

            await _protocolTypeRepository.UpdateAsync(protocolType);
        }
    }

    