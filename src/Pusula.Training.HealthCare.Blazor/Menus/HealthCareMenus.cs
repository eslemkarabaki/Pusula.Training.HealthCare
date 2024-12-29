namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenus
{
    private const string Prefix = "HealthCare";
    public const string Home = Prefix + ".Home";

    //Add your menu items here...

#region PatientRegistration

    public const string PatientRegistration = Prefix + ".PatientRegistration";

    // management
    public const string PatientRegistrationManagement = PatientRegistration + ".Management";
    public const string Patients = PatientRegistrationManagement + ".Patients";

    // reports
    public const string PatientRegistrationReports = PatientRegistration + ".Reports";
    public const string PatientProtocols = PatientRegistrationReports + ".PatientProtocols";

#endregion

    public const string Medical = Prefix + ".Medicals";
    public const string Examinations = Prefix + ".Examinations";
    public const string Appointments = Prefix + ".Appointments";
    public const string Transactions = Appointments + ".Transactions";
    public const string AppointmentTypes = Appointments + ".AppointmentTypes";
    public const string Reports = Appointments + ".Reports";
    public const string Diagnoses = Prefix + ".Diagnosis";

#region Radiologies

    public const string Radiologies = Prefix + ".Radiologies";
    public const string RadiologyTransactions = Radiologies + ".Transactions";
    public const string RadiologyDefinitions = Radiologies + ".Definitions";
    public const string RadiologyReports = Radiologies + ".Reports";
    public const string RadiologyExaminationRequests = Radiologies + ".Requests";

#endregion

#region Definitions

    public const string Definitions = Prefix + ".Definitions";
    public const string Doctors = Definitions + ".Doctors";
    public const string Departments = Definitions + ".Departments";
    public const string Address = Definitions + ".Addresses";
    public const string Countries = Definitions + ".Countries";
    public const string Cities = Definitions + ".Cities";
    public const string Districts = Definitions + ".Districts";
    public const string Hospitals = Definitions + ".Hospitals";
    public const string ProtocolTypes = Definitions + ".ProtocolTypes";
    public const string Insurances = Definitions + ".Insurances";
    public const string Allergies = Definitions + ".Allergies";
    public const string Medicines = Definitions + ".Medicines";
    public const string Operations = Definitions + ".Operations";
    public const string Vaccines = Definitions + ".Vaccines";
    public const string BloodTransfusions = Definitions + ".BloodTransfusions";
    public const string Jobs = Definitions + ".Jobs";
    public const string Educations = Definitions + ".Educations";

#endregion
}