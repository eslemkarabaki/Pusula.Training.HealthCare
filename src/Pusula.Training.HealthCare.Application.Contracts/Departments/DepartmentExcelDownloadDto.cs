namespace Pusula.Training.HealthCare.Departments;

public class DepartmentExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;

    public string? FilterText { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Duration { get; set; }

    public DepartmentExcelDownloadDto()
    {
    }
}