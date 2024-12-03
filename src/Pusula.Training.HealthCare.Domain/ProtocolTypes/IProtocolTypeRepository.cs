using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
public interface IProtocolTypeRepository : IRepository<ProtocolType, Guid>
{
        Task<ProtocolType> FindByNameAsync(string name);
        Task<List<ProtocolType>> GetAllAsync();
    }
}


