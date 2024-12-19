using System;

namespace Pusula.Training.HealthCare.Patients;

public record GetPatientInput(
    Guid? Id = null,
    int? PatientNo = null
);