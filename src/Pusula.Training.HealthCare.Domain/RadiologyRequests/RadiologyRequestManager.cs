using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestManager(IRadiologyRequestRepository radiologyRequestRepository) : DomainService
{
    #region CreateAsync
    public virtual async Task<RadiologyRequest> CreateAsync
        (
        DateTime requestDate,
        Guid protocolId,
        Guid departmentId,
        Guid doctorId
        ) 
    {
        var radiologyRequest = new RadiologyRequest(
                GuidGenerator.Create(),
                requestDate,
                protocolId,
                departmentId,
                doctorId
            );

        return await radiologyRequestRepository.InsertAsync(radiologyRequest);
    }
    #endregion

    #region UpdateAsync
    public virtual async Task<RadiologyRequest> UpdateAsync
        (
        Guid id,
        DateTime requestDate,
        Guid protocolId,
        Guid departmentId,
        Guid doctorId,
        [CanBeNull] string? concurrencyStamp = null
        )
    {
        var radiologyRequest = await radiologyRequestRepository.GetAsync(id);

        radiologyRequest.SetRequestDate(requestDate);
        radiologyRequest.SetProtocolId(protocolId);
        radiologyRequest.SetDepartmentId(departmentId);
        radiologyRequest.SetDoctorId(doctorId);

        radiologyRequest.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await radiologyRequestRepository.UpdateAsync(radiologyRequest);
    }
    #endregion
}
