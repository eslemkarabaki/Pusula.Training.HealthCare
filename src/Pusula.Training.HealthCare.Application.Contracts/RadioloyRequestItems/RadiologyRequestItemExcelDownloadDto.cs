using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public Guid? RequestId { get; set; }
    public Guid? ExaminationId { get; set; }
    public string? Result { get; set; }
    public DateTime? ResultDate { get; set; }
    public RadiologyRequestItemState? State { get; set; }
    public RadiologyRequestItemExcelDownloadDto() { }
}