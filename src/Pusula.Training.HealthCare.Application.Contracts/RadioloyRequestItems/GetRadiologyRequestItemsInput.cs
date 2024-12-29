using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class GetRadiologyRequestItemsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public Guid? RequestId { get; set; }
    public Guid? ExaminationId { get; set; }
    public string? Result { get; set; }
    public DateTime? ResultDate { get; set; }
    public RadiologyRequestItemState? State { get; set; }
    public Guid? ProtocolId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? PatientId { get; set; }  

public GetRadiologyRequestItemsInput() { }
}