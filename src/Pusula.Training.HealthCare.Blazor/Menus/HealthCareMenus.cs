namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenus
{
    private const string Prefix = "HealthCare";
    public const string Home = Prefix + ".Home";

    //Add your menu items here...

#region PatientRegistration

    public const string PatientRegistration = Prefix + ".PatientRegistration";

    // definition
    public const string PatientRegistrationDefinitions = PatientRegistration + ".Definitions";
    public const string ProtocolTypes = PatientRegistrationDefinitions + ".ProtocolTypes";
    public const string Insurances = PatientRegistrationDefinitions + ".Insurances";

    // management
    public const string PatientRegistrationManagement = PatientRegistration + ".Management";
    public const string Patients = PatientRegistrationManagement + ".Patients";

    // reports
    public const string PatientRegistrationReports = PatientRegistration + ".Reports";
    public const string PatientProtocols = PatientRegistrationReports + ".PatientProtocols";

#endregion

    public const string Departments = Prefix + ".Departments";
    public const string Locations = Prefix + ".Locations";
    public const string Countries = Locations + ".Countries";
    public const string Cities = Locations + ".Cities";
    public const string Districts = Locations + ".Districts";
    public const string Hospitals = Prefix + ".Hospitals";
    public const string Examinations = Prefix + ".Examinations";
    public const string Appointments = Prefix + ".Appointments";
    public const string Transactions = Appointments + ".Transactions";
    public const string AppointmentTypes = Appointments + ".AppointmentTypes";

}