namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroupExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public RadiologyExaminationGroupExcelDownloadDto()
        {
        }
    }
}
