namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Radiologies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Radiologies;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using StackExchange.Redis;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using FilteringEventArgs = Syncfusion.Blazor.DropDowns.FilteringEventArgs;
public partial class Report
{
    private EditContext FilterContext { get; set; }
    private GetRadiologyRequestItemsInput Filter { get; set; }
    private int PageSize => 100;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; } 
    private IReadOnlyList<RadiologyRequestItemWithNavigationPropertiesDto> RadiologyRequestItemList { get; set; } = []; 
    private SfGrid<RadiologyRequestItemDto> SfGrid { get; set; } = null!;  

    public Report()
    { 

        Filter = new GetRadiologyRequestItemsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting 
        };
        FilterContext = new EditContext(Filter);
    }
     
    protected override async Task OnInitializedAsync()
    {
        await GetRadiologyRequestItemsAsync();
    }
     
    private async Task SearchAsync()
    { 
        CurrentPage = 1;
        await GetRadiologyRequestItemsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetRadiologyRequestItemsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await RadiologyRequestItemAppService.GetListNavigationPropertiesAsync(Filter);
        RadiologyRequestItemList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    #region Dashboard

    private readonly double[] _spacing = [10, 10];

    #endregion

    #region Doctor

    private IEnumerable<DoctorDto> DoctorList { get; set; } = new List<DoctorDto>(); 
    private SfAutoComplete<Guid?, DoctorDto> DoctorFilterAutoComplete { get; set; } = null!;
    private GetDoctorsInput GetDoctorsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterDoctorAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetDoctorsInput.FullName = args.Text;
        var doctors = await DoctorAppService.GetListAsync(GetDoctorsInput);
        DoctorList = doctors.Items;
        await DoctorFilterAutoComplete.FilterAsync(DoctorList);
    }

    #endregion

    #region Department

    private IEnumerable<DepartmentDto> DepartmentList { get; set; } = new List<DepartmentDto>();
    private SfAutoComplete<Guid?, DepartmentDto> DepartmentFilterAutoComplete { get; set; } = null!;
    private GetDepartmentsInput GetDepartmentsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterDepartmentAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetDepartmentsInput.Name = args.Text;
        var departments = await DepartmentsAppService.GetListAsync(GetDepartmentsInput);
        DepartmentList = departments.Items;
        await DepartmentFilterAutoComplete.FilterAsync(DepartmentList);
    }

    #endregion

    #region Patient 

    private IEnumerable<PatientDto> PatientList { get; set; } = new List<PatientDto>();
    private SfAutoComplete<Guid?, PatientDto> PatientFilterAutoComplete { get; set; } = null!;
    private GetPatientsInput GetPatientsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterPatientAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetPatientsInput.FullName = args.Text;
        var patients = await PatientAppService.GetListAsync(GetPatientsInput);
        PatientList = patients.Items;
        await PatientFilterAutoComplete.FilterAsync(PatientList);
    }

    #endregion

    #region Radiology Examination

    private IEnumerable<RadiologyExaminationDto> RadiologyExaminationList { get; set; } = new List<RadiologyExaminationDto>();
    private SfAutoComplete<Guid?, RadiologyExaminationDto> RadiologyExaminationFilterAutoComplete { get; set; } = null!;
    private GetRadiologyExaminationsInput GetRadiologyExaminationsInput { get; set; } = new() { MaxResultCount = 10 };

    private async Task FilterRadiologyExaminationAsync(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        GetRadiologyExaminationsInput.Name = args.Text;
        var radiologyExaminations = await RadiologyExaminationAppService.GetListAsync(GetRadiologyExaminationsInput);
        RadiologyExaminationList = radiologyExaminations.Items;
        await RadiologyExaminationFilterAutoComplete.FilterAsync(RadiologyExaminationList);
    }

    #endregion
     
    #region Permission

    private bool CanCreateRadiologyRequestItem { get; set; }
    private bool CanEditRadiologyRequestItem { get; set; }
    private bool CanDeleteRadiologyRequestItem { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateRadiologyRequestItem = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Create);
        CanEditRadiologyRequestItem = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Edit);
        CanDeleteRadiologyRequestItem = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.RadiologyRequestItems.Delete);
    }

    #endregion

    #region ScriptHtml
    private static string StripHtml(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
    }
    #endregion

    #region Document

    private RadiologyDocumentDialog DocumentDialog { get; set; } = null!;

    private async Task ShowDocumentsAsync(Guid itemId , string result)
    {
        var documents = await GetDocumentByItemId(itemId);
        await DocumentDialog.ShowAsync(documents, StripHtml(result));
    }

    private async Task<List<RadiologyExaminationDocumentDto>> GetDocumentByItemId(Guid itemId)
    {
        var result = await RadiologyExaminationDocumentAppService.GetListAsync(new GetRadiologyExaminationDocumentsInput { ItemId = itemId });
        return result.Items.ToList();
    }
    #endregion

}