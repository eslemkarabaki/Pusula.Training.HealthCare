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

#region PatientRegistration

    public static class Patients
    {
        public const string Default = GroupName + ".Patients";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Protocols
    {
        public const string Default = GroupName + ".Protocols";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientProtocols
    {
        public const string Default = GroupName + ".PatientProtocols";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientTypes
    {
        public const string Default = GroupName + ".PatientTypes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PatientNotes
    {
        public const string Default = GroupName + ".PatientNotes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ProtocolTypes
    {
        public const string Default = GroupName + ".ProtocolTypes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Countries
    {
        public const string Default = GroupName + ".Countries";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Cities
    {
        public const string Default = GroupName + ".Cities";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Districts
    {
        public const string Default = GroupName + ".Districts";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Insurances
    {
        public const string Default = GroupName + ".Insurances";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

#endregion

    public static class Departments
    {
        public const string Default = GroupName + ".Departments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Appointments
    {
        public const string Default = GroupName + ".Appointments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }    

    public static class AppointmentTypes
    {
        public const string Default = GroupName + ".AppointmentTypes";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Hospitals
    {
        public const string Default = GroupName + ".Hospitals";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Doctors
    {
        public const string Default = GroupName + ".Doctors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    
    public static class DoctorWorkList
    {
        public const string Default = GroupName + ".DoctorWorkList";
    }

    public static class Titles
    {
        public const string Default = GroupName + ".Titles";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Examinations
    {
        public const string Default = GroupName + ".Examinations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Tests
    {
        public const string Default = GroupName + ".Tests";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestTypes
    {
        public const string Default = GroupName + ".TestTypes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestGroups
    {
        public const string Default = GroupName + ".TestGroups";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class TestProcesses
    {
        public const string Default = GroupName + ".TestProcesses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class WorkLists
    {
        public const string Default = GroupName + ".WorkLists";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Diagnosis
    {
        public const string Default = GroupName + ".Diagnosis";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    #region Radiology

    public static class RadiologyExaminationGroups
    {
        public const string Default = GroupName + ".RadiologyExaminationGroups";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyExaminations
    {
        public const string Default = GroupName + ".RadiologyExaminations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyExaminationProcedures
    {
        public const string Default = GroupName + ".RadiologyExaminationProcedures";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyExaminationDocuments
    {
        public const string Default = GroupName + ".RadiologyExaminationDocuments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyRequests
    {
        public const string Default = GroupName + ".RadiologyRequests";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RadiologyRequestItems
    {
        public const string Default = GroupName + ".RadiologyRequestItems";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    #endregion
}

