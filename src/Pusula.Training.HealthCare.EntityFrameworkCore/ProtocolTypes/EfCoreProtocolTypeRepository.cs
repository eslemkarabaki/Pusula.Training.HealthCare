<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.ProtocolTypes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class EfCoreProtocolTypeRepository : EfCoreRepository<HealthCareDbContext, ProtocolType, Guid>, IProtocolTypeRepository
{
    public EfCoreProtocolTypeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : base(dbContextProvider) { }

    public async Task<ProtocolType> FindByNameAsync(string name)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.ProtocolTypes.FirstOrDefaultAsync(pt => pt.Name == name);
    }

    public async Task<List<ProtocolType>> GetAllAsync()
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.ProtocolTypes.ToListAsync();
    }
}
=======
using System;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class EfCoreProtocolTypeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
: EfCoreRepository<HealthCareDbContext, ProtocolType, Guid>(dbContextProvider),
  IProtocolTypeRepository
{
}
>>>>>>> origin/development
