using System;
namespace Pusula.Training.HealthCare.Examinations;

public class ExaminationAnamnezCreateDto
{
    public Guid ExaminationId { get; set; }      // İlgili ExaminationId
    public string IdentityNumber { get; set; }   // Kimlik Numarası
    public string Complaint { get; set; }        // Şikayet
    public string History { get; set; }          // Geçmiş
    public DateTime StartDate { get; set; }      // Başlangıç Tarihi
}
