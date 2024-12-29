using System;
namespace Pusula.Training.HealthCare.Examinations;
public class ExaminationAnamnezUpdateDto
{
    public string Complaint { get; set; }        // Yeni Şikayet
    public string History { get; set; }          // Yeni Geçmiş
    public DateTime StartDate { get; set; }      // Yeni Başlangıç Tarihi
}
