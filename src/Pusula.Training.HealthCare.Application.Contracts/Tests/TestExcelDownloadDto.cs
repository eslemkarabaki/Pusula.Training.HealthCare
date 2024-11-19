using System;

namespace Pusula.Training.HealthCare.Tests
{
    public class TestExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;
        public string? FilterText { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public Guid? TestGroupId { get; set; }
    }
}
