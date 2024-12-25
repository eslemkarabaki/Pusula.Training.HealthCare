using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Medicines;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Medicines.Default)]
public class MedicineAppService(IMedicineRepository medicineRepository, MedicineManager medicineManager)
    : HealthCareAppService, IMedicineAppService
{
    public async Task<MedicineDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Medicine, MedicineDto>(await medicineRepository.GetAsync(id));

    public async Task<List<MedicineDto>> GetListAsync(GetMedicinesInput input) =>
        ObjectMapper.Map<List<Medicine>, List<MedicineDto>>(
            await medicineRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Medicines.Create)]
    public async Task<MedicineDto> CreateAsync(MedicineCreateDto input)
    {
        var medicine = await medicineManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Medicine, MedicineDto>(medicine);
    }

    [Authorize(HealthCarePermissions.Medicines.Edit)]
    public async Task<MedicineDto> UpdateAsync(Guid id, MedicineUpdateDto input)
    {
        var medicine = await medicineManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Medicine, MedicineDto>(medicine);
    }

    [Authorize(HealthCarePermissions.Medicines.Delete)]
    public async Task DeleteAsync(Guid id) => await medicineRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Medicines.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await medicineRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Medicines.Delete)]
    public async Task DeleteAllAsync(GetMedicinesInput input) => await medicineRepository.DeleteAllAsync(input.Name);
}