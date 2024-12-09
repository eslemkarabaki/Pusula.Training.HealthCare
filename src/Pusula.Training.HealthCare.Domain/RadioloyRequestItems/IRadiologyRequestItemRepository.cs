using System;
using System.Collections.Generic; 
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public interface IRadiologyRequestItemRepository : IRepository<RadiologyRequestItem, Guid>
{
    #region DeleteAllAsync
    Task DeleteAllAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            CancellationToken cancellationToken = default
        );
    #endregion

    #region GetWithNavigationPropertiesAsync
    Task<RadiologyRequestItemWithNavigationProperties> GetWithNavigationPropertiesAsync
        (
            Guid id,
            CancellationToken cancellationToken = default
        );
    #endregion

    #region GetListWithNavigationPropertiesAsync
    Task<List<RadiologyRequestItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );
    #endregion

    #region GetListAsync
    Task<List<RadiologyRequestItem>> GetListAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );
    #endregion

    #region GetCountAsync
    Task<long> GetCountAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            CancellationToken cancellationToken = default
        );
    #endregion
}
