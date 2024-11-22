using System;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}