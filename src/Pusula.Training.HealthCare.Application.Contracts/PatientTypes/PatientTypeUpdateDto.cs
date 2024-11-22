using System;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}