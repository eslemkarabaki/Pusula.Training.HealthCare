using Pusula.Training.HealthCare.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Permissions;

public class HealthCarePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HealthCarePermissions.GroupName);

        myGroup.AddPermission(HealthCarePermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(HealthCarePermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HealthCarePermissions.MyPermission1, L("Permission:MyPermission1")); 

        SetPatientPermissions(myGroup);
        SetCountryPermissions(myGroup);
        SetCityPermissions(myGroup);
        SetDistrictPermissions(myGroup);

        var protocolPermission = myGroup.AddPermission(HealthCarePermissions.Protocols.Default, L("Permission:Protocols"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Create, L("Permission:Create"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Edit, L("Permission:Edit"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Delete, L("Permission:Delete"));

        var appointmentPermission = myGroup.AddPermission(HealthCarePermissions.Appointments.Default, L("Permission:Appointments"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Create, L("Permission:Create"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Edit, L("Permission:Edit"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(HealthCarePermissions.Departments.Default, L("Permission:Departments"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Delete, L("Permission:Delete"));

        var hospitalPermission = myGroup.AddPermission(HealthCarePermissions.Hospitals.Default, L("Permission:Hospitals"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Create, L("Permission:Create"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Edit, L("Permission:Edit"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Delete, L("Permission:Delete"));

        var doctorPermission = myGroup.AddPermission(HealthCarePermissions.Doctors.Default, L("Permission:Doctors"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Create, L("Permission:Create"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Edit, L("Permission:Edit"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Delete, L("Permission:Delete"));

        var titlePermission = myGroup.AddPermission(HealthCarePermissions.Titles.Default, L("Permission:Titles"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Create, L("Permission:Create"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Edit, L("Permission:Edit"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Delete, L("Permission:Delete"));

        var examinationPermission = myGroup.AddPermission(HealthCarePermissions.Examinations.Default, L("Permission:Examinations"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HealthCareResource>(name);
    }

    private void SetPatientPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Patients.Default, L("Permission:Patients"));
        permission.AddChild(HealthCarePermissions.Patients.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Patients.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Patients.Delete, L("Permission:Delete"));
    }

    private void SetCountryPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Countries.Default, L("Permission:Countries"));
        permission.AddChild(HealthCarePermissions.Countries.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Countries.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Countries.Delete, L("Permission:Delete"));
    }

    private void SetCityPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Cities.Default, L("Permission:Cities"));
        permission.AddChild(HealthCarePermissions.Cities.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Cities.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Cities.Delete, L("Permission:Delete"));
    }

    private void SetDistrictPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Districts.Default, L("Permission:Districts"));
        permission.AddChild(HealthCarePermissions.Districts.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Districts.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Districts.Delete, L("Permission:Delete"));
    }
    private void SetExaminationsPermissions(PermissionGroupDefinition group)
    {
        var examinations = group.AddPermission(HealthCarePermissions.Examinations.Default, L("Permission:Examinations"));
        examinations.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinations.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinations.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));
    }
}