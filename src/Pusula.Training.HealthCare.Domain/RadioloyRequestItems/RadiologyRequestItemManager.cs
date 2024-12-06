using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemManager(IRadiologyRequestItemRepository radiologyRequestItemRepository) : DomainService
{
    #region CreateAsync
    public virtual async Task<RadiologyRequestItem> CreateAsync
        (
            Guid requestId,
            Guid examinationId,
            DateTime resultDate,
            RadiologyRequestItemState state,
            string? result = null
        )
    {
        var radiologyRequestItem = new RadiologyRequestItem(
            GuidGenerator.Create(),
            requestId,
            examinationId,
            result!,
            resultDate,
            state
            );

        return await radiologyRequestItemRepository.InsertAsync(radiologyRequestItem);
    }
    #endregion

    #region UpdateAsync
    public virtual async Task<RadiologyRequestItem> UpdateAsync
        (
            Guid id,
            Guid requestId,
            Guid examinationId,
            DateTime resultDate,
            RadiologyRequestItemState state,
            string? result = null,
            [CanBeNull] string? concurrencyStamp = null
        )
    {
        var radiologyRequestItem = await radiologyRequestItemRepository.GetAsync(id);

        radiologyRequestItem.SetExaminationId(examinationId);
        radiologyRequestItem.SetRequestId(requestId);
        radiologyRequestItem.SetResult(result);
        radiologyRequestItem.SetResultDate(resultDate);
        radiologyRequestItem.SetState(state);

        radiologyRequestItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await radiologyRequestItemRepository.UpdateAsync(radiologyRequestItem);
    }
    #endregion
}
