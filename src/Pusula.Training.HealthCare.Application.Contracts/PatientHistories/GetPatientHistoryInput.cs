using System;

namespace Pusula.Training.HealthCare.PatientHistories;

public record GetPatientHistoryInput(Guid? PatientHistoryId = null, Guid? PatientId = null);