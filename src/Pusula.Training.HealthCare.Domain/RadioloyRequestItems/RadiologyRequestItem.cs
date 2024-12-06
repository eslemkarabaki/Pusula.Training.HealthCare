using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItem : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid RequestId { get; private set; }
    public virtual Guid ExaminationId { get; private set; }
    [NotNull]
    public virtual string Result { get; private set; }
    [NotNull]
    public virtual DateTime? ResultDate { get; private set; }
    [NotNull]
    public virtual RadiologyRequestItemState State { get; private set; }

    protected RadiologyRequestItem()
    {
        Result = string.Empty;
        ResultDate = DateTime.Now;
        State = RadiologyRequestItemState.Pending;
    }

    public RadiologyRequestItem(Guid id, Guid requestId, Guid examinationId, string result, DateTime resultDate, RadiologyRequestItemState state)
    {
        SetRequestId(requestId);
        SetExaminationId(examinationId);
        SetState(state);
        SetResultDate(resultDate);
        SetResult(result);
    }

    public void SetRequestId(Guid requestId) => RequestId = Check.NotDefaultOrNull<Guid>(requestId, nameof(requestId));
    public void SetExaminationId(Guid examinationId) => ExaminationId = Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
    public void SetResult(string result) => Result = Check.NotNullOrWhiteSpace(result, nameof(result), RadiologyRequestItemConsts.ResultMaxLength);
    public void SetResultDate(DateTime resultDate) => ResultDate = Check.NotNull(resultDate, nameof(resultDate));
    public void SetState(RadiologyRequestItemState state) => State = state;
}
