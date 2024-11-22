using System;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeCreateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}