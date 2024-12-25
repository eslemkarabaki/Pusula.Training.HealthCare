using System;
using Pusula.Training.HealthCare.Medicines;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PatientHistoryMedicines;

public class PatientHistoryMedicineDto : EntityDto<Guid>
{
    public Guid PatientHistoryId { get; set; }
    public Guid MedicineId { get; set; }
    public MedicineDto Medicine { get; set; }
    public string? Explanation { get; set; }
}