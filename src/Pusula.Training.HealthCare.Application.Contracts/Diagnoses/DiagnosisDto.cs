using System;
using System.Globalization;

namespace Pusula.Training.HealthCare.Diagnoses;

public class DiagnosisDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string FullName => $"{Code}-{Name}";
}
