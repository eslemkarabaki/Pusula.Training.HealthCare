namespace Pusula.Training.HealthCare.Hospitals
{
    public class HospitalExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }

        public HospitalExcelDownloadDto()
        {
        }
    }
}
