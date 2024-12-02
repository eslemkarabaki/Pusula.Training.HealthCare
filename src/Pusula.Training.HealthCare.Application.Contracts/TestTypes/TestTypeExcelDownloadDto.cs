using System;

namespace Pusula.Training.HealthCare.TestTypes;

public class TestTypeExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public string? Name { get; set; }
}
