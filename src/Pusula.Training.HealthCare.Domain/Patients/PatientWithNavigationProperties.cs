using System.Collections.Generic;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.PatientTypes;

namespace Pusula.Training.HealthCare.Patients;

public class PatientWithNavigationProperties
{
    public Patient Patient { get; set; }
    public Country Country { get; set; }
    public PatientType PatientType { get; set; }
    public Insurance Insurance { get; set; }
    public IEnumerable<PatientNote> PatientNotes { get; set; }
}