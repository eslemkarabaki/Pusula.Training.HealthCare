using System;

namespace Pusula.Training.HealthCare.Doctors;

public record GetDoctorInput(Guid? DoctorId = null, Guid? UserId = null);