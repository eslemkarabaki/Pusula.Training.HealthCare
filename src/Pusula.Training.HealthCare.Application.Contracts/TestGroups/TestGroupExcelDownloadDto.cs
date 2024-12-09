namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
}
