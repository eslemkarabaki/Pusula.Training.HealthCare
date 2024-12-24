using Pusula.Training.HealthCare.Localization;
using System.Text.RegularExpressions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Permissions;

public class HealthCarePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GroupName);

        myGroup.AddPermission(Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        // Define other permissions here
        SetPatientPermissions(myGroup);
        SetCountryPermissions(myGroup);
        SetCityPermissions(myGroup);
        SetDistrictPermissions(myGroup);
        SetPatientTypePermissions(myGroup);
        SetProtocolsPermissions(myGroup);
        SetPatientProtocolsPermissions(myGroup);
        SetProtocolTypesPermissions(myGroup);
        SetDiagnosisPermissions(myGroup);
        SetInsurancesPermissions(myGroup);
        SetExaminationsPermissions(myGroup);
        SetRadiologyPermissions(myGroup);
        SetPatientNotesPermissions(myGroup);
        SetAllergiesPermissions(myGroup);
        SetMedicinesPermissions(myGroup);
        SetOperationsPermissions(myGroup);
        SetVaccinesPermissions(myGroup);
        SetBloodTransfusionsPermissions(myGroup);
        SetJobsPermissions(myGroup);
        SetEducationsPermissions(myGroup);

        var appointmentPermission =
            myGroup.AddPermission(HealthCarePermissions.Appointments.Default, L("Permission:Appointments"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Menu, L("Permission:Menu"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Create, L("Permission:Create"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Edit, L("Permission:Edit"));
        appointmentPermission.AddChild(HealthCarePermissions.Appointments.Delete, L("Permission:Delete"));

        var appointmentTypePermission = myGroup.AddPermission(
            HealthCarePermissions.AppointmentTypes.Default, L("Permission:AppointmentTypes")
        );
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Menu, L("Permission:Menu"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Create, L("Permission:Create"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Edit, L("Permission:Edit"));
        appointmentTypePermission.AddChild(HealthCarePermissions.AppointmentTypes.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(
            HealthCarePermissions.Departments.Default, L("Permission:Departments")
        );
        departmentPermission.AddChild(HealthCarePermissions.Departments.Menu, L("Permission:Menu"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(HealthCarePermissions.Departments.Delete, L("Permission:Delete"));

        var hospitalPermission = myGroup.AddPermission(
            HealthCarePermissions.Hospitals.Default, L("Permission:Hospitals")
        );
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Menu, L("Permission:Menu"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Create, L("Permission:Create"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Edit, L("Permission:Edit"));
        hospitalPermission.AddChild(HealthCarePermissions.Hospitals.Delete, L("Permission:Delete"));

        var doctorPermission = myGroup.AddPermission(HealthCarePermissions.Doctors.Default, L("Permission:Doctors"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Menu, L("Permission:Menu"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Create, L("Permission:Create"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Edit, L("Permission:Edit"));
        doctorPermission.AddChild(HealthCarePermissions.Doctors.Delete, L("Permission:Delete"));

        var titlePermission = myGroup.AddPermission(HealthCarePermissions.Titles.Default, L("Permission:Titles"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Menu, L("Permission:Menu"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Create, L("Permission:Create"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Edit, L("Permission:Edit"));
        titlePermission.AddChild(HealthCarePermissions.Titles.Delete, L("Permission:Delete"));

        var testPermission = myGroup.AddPermission(HealthCarePermissions.Tests.Default, L("Permission:Tests"));
        testPermission.AddChild(HealthCarePermissions.Tests.Menu, L("Permission:Menu"));
        testPermission.AddChild(HealthCarePermissions.Tests.Create, L("Permission:Create"));
        testPermission.AddChild(HealthCarePermissions.Tests.Edit, L("Permission:Edit"));
        testPermission.AddChild(HealthCarePermissions.Tests.Delete, L("Permission:Delete"));

        var testTypePermission = myGroup.AddPermission(
            HealthCarePermissions.TestTypes.Default, L("Permission:TestTypes")
        );
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Menu, L("Permission:Menu"));
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Create, L("Permission:Create"));
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Edit, L("Permission:Edit"));
        testTypePermission.AddChild(HealthCarePermissions.TestTypes.Delete, L("Permission:Delete"));

        var testGroupPermission = myGroup.AddPermission(
            HealthCarePermissions.TestGroups.Default, L("Permission:TestGroups")
        );
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Menu, L("Permission:Menu"));
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Create, L("Permission:Create"));
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Edit, L("Permission:Edit"));
        testTypePermission.AddChild(HealthCarePermissions.TestGroups.Delete, L("Permission:Delete"));

        var testProcessPermission = myGroup.AddPermission(
            HealthCarePermissions.TestProcesses.Default, L("Permission:TestProcesses")
        );
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Menu, L("Permission:Menu"));
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Create, L("Permission:Create"));
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Edit, L("Permission:Edit"));
        testProcessPermission.AddChild(HealthCarePermissions.TestProcesses.Delete, L("Permission:Delete"));

        var workListPermission = myGroup.AddPermission(
            HealthCarePermissions.WorkLists.Default, L("Permission:WorkLists")
        );
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Menu, L("Permission:Menu"));
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Create, L("Permission:Create"));
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Edit, L("Permission:Edit"));
        workListPermission.AddChild(HealthCarePermissions.WorkLists.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name) => LocalizableString.Create<HealthCareResource>(name);

#region Radiology

    private void SetRadiologyPermissions(PermissionGroupDefinition group)
    {   
        SetStandardPermissions(group, HealthCarePermissions.RadiologyExaminationGroups.Default, "RadiologyExaminationGroups");
        SetStandardPermissions(group, HealthCarePermissions.RadiologyExaminations.Default, "RadiologyExaminations");
        SetStandardPermissions(group, HealthCarePermissions.RadiologyExaminationDocuments.Default, "RadiologyExaminationDocuments");
        SetStandardPermissions(group, HealthCarePermissions.RadiologyRequests.Default, "RadiologyRequests");
        SetStandardPermissions(group, RadiologyRequestItems.Default, "RadiologyRequestItems"); 
        SetStandardPermissions(group, RadiologyReports.Default, "RadiologyReports");
        SetStandardPermissions(group, RadiologyDefinitions.Default, "RadiologyDefinitions");
        SetStandardPermissions(group, RadiologyTransactions.Default, "RadiologyTransactions");
    }

#endregion

    private void SetPatientPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Patients.Default, L("Permission:Patients"));
        permission.AddChild(HealthCarePermissions.Patients.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Patients.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Patients.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Patients.Delete, L("Permission:Delete"));
    }

    private void SetPatientTypePermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.PatientTypes.Default, L("Permission:PatientTypes"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.PatientTypes.Delete, L("Permission:Delete"));
    }

    private void SetCountryPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Countries.Default, L("Permission:Countries"));
        permission.AddChild(HealthCarePermissions.Countries.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Countries.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Countries.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Countries.Delete, L("Permission:Delete"));
    }

    private void SetCityPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Cities.Default, L("Permission:Cities"));
        permission.AddChild(HealthCarePermissions.Cities.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Cities.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Cities.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Cities.Delete, L("Permission:Delete"));
    }

    private void SetDistrictPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Districts.Default, L("Permission:Districts"));
        permission.AddChild(HealthCarePermissions.Districts.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Districts.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Districts.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Districts.Delete, L("Permission:Delete"));
    }

    private void SetTestPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.Tests.Default, L("Permission:Tests"));
        permission.AddChild(HealthCarePermissions.Tests.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Tests.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Tests.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Tests.Delete, L("Permission:Delete"));
    }

    private void SetExaminationsPermissions(PermissionGroupDefinition group)
    {
        var examinations = group.AddPermission(
            HealthCarePermissions.Examinations.Default, L("Permission:Examinations")
        );
        examinations.AddChild(HealthCarePermissions.Examinations.Menu, L("Permission:Menu"));
        examinations.AddChild(HealthCarePermissions.Examinations.Create, L("Permission:Create"));
        examinations.AddChild(HealthCarePermissions.Examinations.Edit, L("Permission:Edit"));
        examinations.AddChild(HealthCarePermissions.Examinations.Delete, L("Permission:Delete"));
    }

    private void SetTestTypePermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.TestTypes.Default, L("Permission:TestTypes"));
        permission.AddChild(HealthCarePermissions.TestTypes.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.TestTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestTypes.Delete, L("Permission:Delete"));
    }

    private void SetTestGroupPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.TestGroups.Default, L("Permission:TestGroups"));
        permission.AddChild(HealthCarePermissions.TestGroups.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.TestGroups.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestGroups.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestGroups.Delete, L("Permission:Delete"));
    }

    private void SetTestProcessPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.TestProcesses.Default, L("Permission:TestProcesses")
        );
        permission.AddChild(HealthCarePermissions.TestProcesses.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.TestProcesses.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.TestProcesses.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.TestProcesses.Delete, L("Permission:Delete"));
    }

    private void SetWorkListPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(HealthCarePermissions.WorkLists.Default, L("Permission:WorkLists"));
        permission.AddChild(HealthCarePermissions.WorkLists.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.WorkLists.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.WorkLists.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.WorkLists.Delete, L("Permission:Delete"));
    }

    private void SetProtocolsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Protocols.Default, L("Permission:Protocols")
        );
        permission.AddChild(HealthCarePermissions.Protocols.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Protocols.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Protocols.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Protocols.Delete, L("Permission:Delete"));
    }

    private void SetPatientProtocolsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            PatientProtocols.Default, L("Permission:PatientProtocols")
        );
        permission.AddChild(PatientProtocols.Menu, L("Permission:Menu"));
        permission.AddChild(PatientProtocols.Create, L("Permission:Create"));
        permission.AddChild(PatientProtocols.Edit, L("Permission:Edit"));
        permission.AddChild(PatientProtocols.Delete, L("Permission:Delete"));
    }

    private void SetProtocolTypesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.ProtocolTypes.Default, L("Permission:ProtocolTypes")
        );
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.ProtocolTypes.Delete, L("Permission:Delete"));
    }

    private void SetInsurancesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Insurances.Default, L("Permission:Insurances")
        );
        permission.AddChild(HealthCarePermissions.Insurances.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Insurances.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Insurances.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Insurances.Delete, L("Permission:Delete"));
    }

    private void SetDiagnosisPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            Diagnosis.Default, L("Permission:Diagnosis")
        );
        permission.AddChild(Diagnosis.Menu, L("Permission:Menu"));
        permission.AddChild(Diagnosis.Create, L("Permission:Create"));
        permission.AddChild(Diagnosis.Edit, L("Permission:Edit"));
        permission.AddChild(Diagnosis.Delete, L("Permission:Delete"));
    }

    private void SetPatientNotesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.PatientNotes.Default, L("Permission:PatientNotes")
        );
        permission.AddChild(HealthCarePermissions.PatientNotes.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.PatientNotes.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.PatientNotes.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.PatientNotes.Delete, L("Permission:Delete"));
    }

    private void SetAllergiesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Allergies.Default, L("Permission:Allergies")
        );
        permission.AddChild(HealthCarePermissions.Allergies.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Allergies.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Allergies.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Allergies.Delete, L("Permission:Delete"));
    }

    private void SetMedicinesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Medicines.Default, L("Permission:Medicines")
        );
        permission.AddChild(HealthCarePermissions.Medicines.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Medicines.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Medicines.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Medicines.Delete, L("Permission:Delete"));
    }

    private void SetOperationsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Operations.Default, L("Permission:Operations")
        );
        permission.AddChild(HealthCarePermissions.Operations.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Operations.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Operations.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Operations.Delete, L("Permission:Delete"));
    }

    private void SetVaccinesPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Vaccines.Default, L("Permission:Vaccines")
        );
        permission.AddChild(HealthCarePermissions.Vaccines.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Vaccines.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Vaccines.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Vaccines.Delete, L("Permission:Delete"));
    }

    private void SetBloodTransfusionsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.BloodTransfusions.Default, L("Permission:BloodTransfusions")
        );
        permission.AddChild(HealthCarePermissions.BloodTransfusions.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.BloodTransfusions.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.BloodTransfusions.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.BloodTransfusions.Delete, L("Permission:Delete"));
    }

    private void SetJobsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Jobs.Default, L("Permission:Jobs")
        );
        permission.AddChild(HealthCarePermissions.Jobs.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Jobs.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Jobs.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Jobs.Delete, L("Permission:Delete"));
    }

    private void SetEducationsPermissions(PermissionGroupDefinition group)
    {
        var permission = group.AddPermission(
            HealthCarePermissions.Educations.Default, L("Permission:Educations")
        );
        permission.AddChild(HealthCarePermissions.Educations.Menu, L("Permission:Menu"));
        permission.AddChild(HealthCarePermissions.Educations.Create, L("Permission:Create"));
        permission.AddChild(HealthCarePermissions.Educations.Edit, L("Permission:Edit"));
        permission.AddChild(HealthCarePermissions.Educations.Delete, L("Permission:Delete"));
    }

#region StandardPermissions

    private void SetStandardPermissions(PermissionGroupDefinition group, string defaultPermission, string displayName)
    {
        var permission = group.AddPermission(defaultPermission, L($"Permission:{displayName}"));
        AddStandardChildPermissions(permission);
    }

    private void AddStandardChildPermissions(PermissionDefinition permission)
    {
        permission.AddChild(permission.Name + ".Menu", L("Permission:Menu"));
        permission.AddChild(permission.Name + ".Create", L("Permission:Create"));
        permission.AddChild(permission.Name + ".Edit", L("Permission:Edit"));
        permission.AddChild(permission.Name + ".Delete", L("Permission:Delete"));
    }


    #endregion
}