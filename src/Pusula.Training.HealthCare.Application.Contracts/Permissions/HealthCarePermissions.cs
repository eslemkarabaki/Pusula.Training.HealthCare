namespace Pusula.Training.HealthCare.Permissions;

public static class HealthCarePermissions
{
    public const string GroupName = "HealthCare";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

#region Definitions

    public static class Allergies
    {
        public const string Default = GroupName + ".Allergies";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Medicines
    {
        public const string Default = GroupName + ".Medicines";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Operations
    {
        public const string Default = GroupName + ".Operations";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Vaccines
    {
        public const string Default = GroupName + ".Vaccines";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class BloodTransfusions
    {
        public const string Default = GroupName + ".BloodTransfusions";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Jobs
    {
        public const string Default = GroupName + ".Jobs";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Educations
    {
        public const string Default = GroupName + ".Educations";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Countries
    {
        public const string Default = GroupName + ".Countries";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Cities
    {
        public const string Default = GroupName + ".Cities";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Districts
    {
        public const string Default = GroupName + ".Districts";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Insurances
    {
        public const string Default = GroupName + ".Insurances";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientTypes
    {
        public const string Default = GroupName + ".PatientTypes";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ProtocolTypes
    {
        public const string Default = GroupName + ".ProtocolTypes";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Departments
    {
        public const string Default = GroupName + ".Departments";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class AppointmentTypes
    {
        public const string Default = GroupName + ".AppointmentTypes";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Hospitals
    {
        public const string Default = GroupName + ".Hospitals";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Doctors
    {
        public const string Default = GroupName + ".Doctors";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Titles
    {
        public const string Default = GroupName + ".Titles";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Diagnosis
    {
        public const string Default = GroupName + ".Diagnosis";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

#endregion

#region PatientRegistration

    public static class Patients
    {
        public const string Default = GroupName + ".Patients";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Protocols
    {
        public const string Default = GroupName + ".Protocols";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientProtocols
    {
        public const string Default = GroupName + ".PatientProtocols";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientNotes
    {
        public const string Default = GroupName + ".PatientNotes";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

#endregion

    public static class Appointments
    {
        public const string Default = GroupName + ".Appointments";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Examinations
    {
        public const string Default = GroupName + ".Examinations";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Tests
    {
        public const string Default = GroupName + ".Tests";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestTypes
    {
        public const string Default = GroupName + ".TestTypes";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestGroups
    {
        public const string Default = GroupName + ".TestGroups";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestProcesses
    {
        public const string Default = GroupName + ".TestProcesses";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class WorkLists
    {
        public const string Default = GroupName + ".WorkLists";
        public const string Menu = Default + ".Menu";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

#region Radiology

    public static class RadiologyExaminationGroups
    {
        public const string Default = GroupName + ".RadiologyExaminationGroups";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyExaminations
    {
        public const string Default = GroupName + ".RadiologyExaminations";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
     
    public static class RadiologyExaminationDocuments
    {
        public const string Default = GroupName + ".RadiologyExaminationDocuments";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyRequests
    {
        public const string Default = GroupName + ".RadiologyRequests";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyRequestItems
    {
        public const string Default = GroupName + ".RadiologyRequestItems";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
      
    public static class RadiologyReports
    {
        public const string Default = GroupName + ".RadiologyReports";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyDefinitions
    {
        public const string Default = GroupName + ".RadiologyDefinitions";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyTransactions
    {
        public const string Default = GroupName + ".RadiologyTransactions";
        public const string Menu = Default + ".Menu";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

#endregion
}