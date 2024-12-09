using System;

namespace Pusula.Training.HealthCare.WorkLists;

public class WorkListExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public Guid? DepartmentId { get; set; }
}
