using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Medicines;

public interface IMedicineAppService : IApplicationService
{
    Task<MedicineDto> GetAsync(Guid id);
    Task<List<MedicineDto>> GetListAsync(GetMedicinesInput input);
    Task<MedicineDto> CreateAsync(MedicineCreateDto input);

    Task<MedicineDto> UpdateAsync(Guid id, MedicineUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetMedicinesInput input);
}