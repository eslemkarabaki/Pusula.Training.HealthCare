namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public enum RadiologyRequestItemState
{
    Pending = 0,       // Bekliyor
    InProgress = 1,    // İşlemde
    Completed = 2,     // Tamamlandı
    Cancelled = 3      // İptal Edildi
}