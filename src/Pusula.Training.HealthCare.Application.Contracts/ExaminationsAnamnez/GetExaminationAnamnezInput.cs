using System;
namespace Pusula.Training.HealthCare.Examinations;
public class GetExaminationAnamnezInput
{
    public string IdentityNumber { get; set; } // Kimlik numarasına göre filtreleme
    public Guid? ExaminationId { get; set; }  // ExaminationId'ye göre filtreleme
    public DateTime? StartDate { get; set; }  // Belirli bir tarihe göre filtreleme
}
