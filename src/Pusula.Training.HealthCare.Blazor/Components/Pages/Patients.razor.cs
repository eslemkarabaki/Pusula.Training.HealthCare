using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Patients
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private IReadOnlyList<PatientDto> PatientList { get; set; } = [];
    private GetPatientsInput Filter { get; set; }

    private IEnumerable<CountryDto> CountryList { get; set; } = [];
    private IEnumerable<CityDto> CityList { get; set; } = [];
    private IEnumerable<DistrictDto> DistrictList { get; set; } = [];

    private DataGridEntityActionsColumn<PatientDto> EntityActionsColumn { get; set; } = new();

    private List<PatientDto> SelectedPatients { get; set; } = [];
    private bool AllPatientsSelected { get; set; }

    public Patients()
    {
        Filter = new GetPatientsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        CountryList = await CountryAppService.GetListAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await SetToolbarItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Patients"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(L["NewPatient"], OpenCreatePatientModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Patients.Create);

        return ValueTask.CompletedTask;
    }


    private async Task GetPatientsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await PatientsAppService.GetListWithAddressAndCountryAsync(Filter);
        PatientList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task DownloadAsExcelAsync()
    {
        var token = (await PatientsAppService.GetDownloadTokenAsync()).Token;
        var remoteService =
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;

        NavigationManager.NavigateTo(
            $"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/patients/as-excel-file?DownloadToken={token}{Filter.ToQueryParameterString(culture)}",
            true);
    }

#region DataGrid

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<PatientDto> e)
    {
        CurrentSorting = e.Columns
                          .Where(c => c.SortDirection != SortDirection.Default)
                          .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                          .JoinAsString(",");
        CurrentPage = e.Page;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
    }


    private Task SelectAllItems()
    {
        AllPatientsSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllPatientsSelected = false;
        SelectedPatients.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedPatientRowsChanged()
    {
        AllPatientsSelected = PatientList.Count < PageSize
            ? SelectedPatients.Count == PatientList.Count
            : SelectedPatients.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion


#region Create

    private PatientCreateDto NewPatient { get; set; } = new();
    private Validations NewPatientValidations { get; set; } = new();
    private Modal CreatePatientModal { get; set; } = new();
    private Guid CreateAddressCountryId { get; set; } = Guid.Empty;
    private Guid CreateAddressCityId { get; set; } = Guid.Empty;

    protected string SelectedCreateTab = "patient-create-tab";
    private string _selectedCreateModalTab = "personal";

    private async Task OpenCreatePatientModalAsync()
    {
        SetDefaultValuesForCreatePatientModal();
        SelectedCreateTab = "patient-create-tab";

        await NewPatientValidations.ClearAll();
        await CreatePatientModal.Show();
    }

    private async Task CloseCreatePatientModalAsync()
    {
        SetDefaultValuesForCreatePatientModal();
        await CreatePatientModal.Hide();
    }

    private async Task CreatePatientAsync()
    {
        try
        {
            if (await NewPatientValidations.ValidateAll() == false)
            {
                return;
            }

            await PatientsAppService.CreateAsync(NewPatient);
            await GetPatientsAsync();
            await CloseCreatePatientModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void SetDefaultValuesForCreatePatientModal()
    {
        NewPatient = new PatientCreateDto
        {
            BirthDate = DateTime.Now
        };
        CreateAddressCountryId = Guid.Empty;
        CreateAddressCityId = Guid.Empty;
        CityList = [];
        DistrictList = [];
    }

    private Task OnSelectedCreateModalTabChanged(string tabName)
    {
        _selectedCreateModalTab = tabName;
        return Task.CompletedTask;
    }

    private async Task CreateAddressCountryIdChangedAsync(Guid countryId)
    {
        CreateAddressCountryId = countryId;
        CityList = await CityAppService.GetListAsync(countryId);
        DistrictList = [];
    }

    private async Task CreateAddressCityIdChangedAsync(Guid cityId)
    {
        CreateAddressCityId = cityId;
        DistrictList = await DistrictAppService.GetListAsync(cityId);
    }

#endregion

#region Update

    private PatientUpdateDto EditingPatient { get; set; } = new();
    private Validations EditingPatientValidations { get; set; } = new();
    private Guid EditingPatientId { get; set; }

    private Modal EditPatientModal { get; set; } = new();
    protected string SelectedEditTab = "patient-edit-tab";

    private Guid EditingAddressCountryId { get; set; } = Guid.Empty;
    private Guid EditingAddressCityId { get; set; } = Guid.Empty;

    private string _selectedUpdateModalTab = "personal";

    private async Task OpenEditPatientModalAsync(PatientDto input)
    {
        SelectedEditTab = "patient-edit-tab";

        var patient = await PatientsAppService.GetAsync(input.Id);

        EditingPatientId = patient.Id;
        EditingPatient = ObjectMapper.Map<PatientDto, PatientUpdateDto>(patient);
        EditingAddressCountryId = input.Address.CountryId;
        EditingAddressCityId = input.Address.CityId;

        CityList = await CityAppService.GetListAsync(input.Address.CountryId);
        DistrictList = await DistrictAppService.GetListAsync(input.Address.CityId);

        await EditingPatientValidations.ClearAll();
        await EditPatientModal.Show();
    }

    private async Task CloseEditPatientModalAsync()
    {
        CityList = [];
        DistrictList = [];
        await EditPatientModal.Hide();
    }

    private async Task UpdatePatientAsync()
    {
        try
        {
            if (await EditingPatientValidations.ValidateAll() == false)
            {
                return;
            }

            await PatientsAppService.UpdateAsync(EditingPatientId, EditingPatient);
            await GetPatientsAsync();
            await EditPatientModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private Task OnSelectedUpdateModalTabChanged(string tabName)
    {
        _selectedUpdateModalTab = tabName;
        return Task.CompletedTask;
    }

    private async Task EditingAddressCountryIdChangedAsync(Guid countryId)
    {
        EditingAddressCountryId = countryId;
        EditingAddressCityId = Guid.Empty;
        CityList = await CityAppService.GetListAsync(countryId);
        DistrictList = [];
    }

    private async Task EditingAddressCityIdChangedAsync(Guid cityId)
    {
        EditingAddressCityId = cityId;
        EditingPatient.DistrictId = Guid.Empty;
        DistrictList = await DistrictAppService.GetListAsync(cityId);
    }

#endregion

#region Delete

    private async Task DeletePatientAsync(PatientDto input)
    {
        await PatientsAppService.DeleteAsync(input.Id);
        await GetPatientsAsync();
    }

    private async Task DeleteSelectedPatientsAsync()
    {
        var message = AllPatientsSelected
            ? L["DeleteAllRecords"].Value
            : L["DeleteSelectedRecords", SelectedPatients.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllPatientsSelected)
        {
            await PatientsAppService.DeleteAllAsync(Filter);
        }
        else
        {
            await PatientsAppService.DeleteByIdsAsync(SelectedPatients.Select(x => x.Id).ToList());
        }

        SelectedPatients.Clear();
        AllPatientsSelected = false;

        await GetPatientsAsync();
    }

#endregion

#region Manual Binding Events

    protected virtual async Task OnFirstNameChangedAsync(string? firstName)
    {
        Filter.FirstName = firstName;
        await SearchAsync();
    }

    protected virtual async Task OnLastNameChangedAsync(string? lastName)
    {
        Filter.LastName = lastName;
        await SearchAsync();
    }

    protected virtual async Task OnBirthDateMinChangedAsync(DateTime? birthDateMin)
    {
        Filter.BirthDateMin = birthDateMin.HasValue ? birthDateMin.Value.Date : birthDateMin;
        await SearchAsync();
    }

    protected virtual async Task OnBirthDateMaxChangedAsync(DateTime? birthDateMax)
    {
        Filter.BirthDateMax = birthDateMax.HasValue ? birthDateMax.Value.Date.AddDays(1).AddSeconds(-1) : birthDateMax;
        await SearchAsync();
    }

    protected virtual async Task OnIdentityNumberChangedAsync(string? identityNumber)
    {
        Filter.IdentityNumber = identityNumber;
        await SearchAsync();
    }

    protected virtual async Task OnEmailAddressChangedAsync(string? emailAddress)
    {
        Filter.EmailAddress = emailAddress;
        await SearchAsync();
    }

    protected virtual async Task OnMobilePhoneNumberChangedAsync(string? mobilePhoneNumber)
    {
        Filter.MobilePhoneNumber = mobilePhoneNumber;
        await SearchAsync();
    }

    protected virtual async Task OnHomePhoneNumberChangedAsync(string? homePhoneNumber)
    {
        Filter.HomePhoneNumber = homePhoneNumber;
        await SearchAsync();
    }

    protected virtual async Task OnGenderChangedAsync(EnumGender gender)
    {
        Filter.Gender = gender;
        await SearchAsync();
    }

    protected virtual async Task OnBloodTypeChangedAsync(EnumBloodType bloodType)
    {
        Filter.BloodType = bloodType;
        await SearchAsync();
    }

    protected virtual async Task OnMaritalStatusChangedAsync(EnumMaritalStatus maritalStatus)
    {
        Filter.MaritalStatus = maritalStatus;
        await SearchAsync();
    }

#endregion

#region Permission

    private bool CanCreatePatient { get; set; }
    private bool CanEditPatient { get; set; }
    private bool CanDeletePatient { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreatePatient = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Patients.Create);
        CanEditPatient = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Patients.Edit);
        CanDeletePatient = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Patients.Delete);
    }

#endregion
}