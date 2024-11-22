namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenus
{
    private const string Prefix = "HealthCare";
    public const string Home = Prefix + ".Home";

    //Add your menu items here...
    public const string Patients = Prefix + ".Patients";
    public const string Protocols = Prefix + ".Protocols";
    public const string Departments = Prefix + ".Departments";
    public const string Locations = Prefix + ".Locations";
    public const string Countries = Locations + ".Countries";
    public const string Cities = Locations + ".Cities";
    public const string Districts = Locations + ".Districts";
    public const string Hospitals = Prefix + ".Hospitals";
    public const string Examinations = Prefix + ".Examinations";
    public const string Appointments = Prefix + ".Appointments";

}