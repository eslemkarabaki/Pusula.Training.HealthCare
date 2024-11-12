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

        var patientPermission = myGroup.AddPermission(HealthCarePermissions.Patients.Default, L("Permission:Patients"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Create, L("Permission:Create"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Edit, L("Permission:Edit"));
        patientPermission.AddChild(HealthCarePermissions.Patients.Delete, L("Permission:Delete"));

        var protocolPermission = myGroup.AddPermission(HealthCarePermissions.Protocols.Default, L("Permission:Protocols"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Create, L("Permission:Create"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Edit, L("Permission:Edit"));
        protocolPermission.AddChild(HealthCarePermissions.Protocols.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(HealthCarePermissions.Departments.Default, L("Permission:Departments"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Delete, L("Permission:Delete"));

        var hospitalPermission = myGroup.AddPermission(HealthCarePermissions.Hospitals.Default, L("Permission:Hospitals"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Create, L("Permission:Create"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Edit, L("Permission:Edit"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Delete, L("Permission:Delete"));

        var examinationPermission = myGroup.AddPermission(HealthCarePermissions.Examinations.Default, L("Permission:Examinations"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinationPermission.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HealthCareResource>(name);
    }
}