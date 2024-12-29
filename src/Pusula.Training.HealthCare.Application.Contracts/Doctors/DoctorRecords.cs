using System;

namespace Pusula.Training.HealthCare.Doctors;

public record GetDoctorInput(Guid? DoctorId = null, Guid? UserId = null);

public record SendDoctorNotificationInput(Guid DoctorId, string Message);