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
using Blazorise.DeepCloner;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Patients;
using Pusula.Training.HealthCare.Blazor.Extensions;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.PatientTypes;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Notifications;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;
using SortDirection = Blazorise.SortDirection;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Patients
{
    private SfGrid<PatientWithNavigationPropertiesDto> SfGrid { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private bool ShowAdvancedFilters { get; set; }

    private IReadOnlyList<PatientWithNavigationPropertiesDto> PatientList { get; set; } = [];
    private GetPatientsInput Filter { get; set; }
    private GetPatientsInput LastFilter { get; set; }
    private GetPatientsInputValidator FilterValidator { get; set; } = null!;
    private EditContext FilterContext { get; set; }

    private IEnumerable<PatientTypeDto> PatientTypeList { get; set; } = [];
    private IEnumerable<CountryDto> CountryList { get; set; } = [];
    private bool AllPatientsSelected { get; set; }
    private SfToast FilterToast { get; set; } = null!;

    public Patients()
    {
        Filter = new GetPatientsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
        FilterContext = new EditContext(Filter);
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetPatientsAsync();
        CountryList = await CountryAppService.GetListAsync();
        PatientTypeList = await PatientTypeAppService.GetListAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetBreadcrumbItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Patients"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetPatientsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await PatientAppService.GetNavigationPropertiesListAsync(Filter);
        PatientList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        if (Filter.AreAllPropertiesEmpty())
        {
            await FilterToast.ShowAsync();
            return;
        }
        if (Filter.DeepCompare(LastFilter))
        {
            return;
        }
        LastFilter = Filter.DeepClone();

        CurrentPage = 1;
        await GetPatientsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task DownloadAsExcelAsync()
    {
        var token = (await PatientAppService.GetDownloadTokenAsync()).Token;
        var remoteService =
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
        var culture = CultureInfo.CurrentUICulture.Name ?? CultureInfo.CurrentCulture.Name;

        NavigationManager.NavigateTo(
            $"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/patients/as-excel-file?DownloadToken={token}{Filter.ToQueryParameterString(culture)}",
            true
        );
    }

#region DataGrid

    private Task ClearSelection()
    {
        AllPatientsSelected = false;
        SfGrid.SelectedRecords.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedPatientRowChangedAsync()
    {
        AllPatientsSelected = PatientList.Count < PageSize ?
            SfGrid.SelectedRecords.Count == PatientList.Count :
            SfGrid.SelectedRecords.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion

#region Create

    private PatientCreateDialog CreatePatientDialog { get; set; } = null!;

    private async Task OpenCreatePatientDialogAsync() => await CreatePatientDialog.ShowAsync();

    private async Task CreatePatientAsync(PatientCreateDto patient)
    {
        try
        {
            await PatientAppService.CreateAsync(patient);
            await GetPatientsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Update

    private Guid EditingPatientId { get; set; }
    private PatientUpdateDialog UpdatePatientDialog { get; set; } = null!;

    private async Task OpenEditPatientModalAsync(PatientDto input)
    {
        EditingPatientId = input.Id;
        await UpdatePatientDialog.ShowAsync(EditingPatientId);
    }

    private async Task UpdatePatientAsync(PatientUpdateDto dto)
    {
        try
        {
            await PatientAppService.UpdateAsync(EditingPatientId, dto);
            await GetPatientsAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

#endregion

#region Delete

    private async Task DeletePatientAsync(PatientDto input)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await PatientAppService.DeleteAsync(input.Id);
            await GetPatientsAsync();
        }
    }

    private async Task DeleteSelectedPatientsAsync()
    {
        var message = AllPatientsSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        if (AllPatientsSelected)
        {
            await PatientAppService.DeleteAllAsync(Filter);
        } else
        {
            await PatientAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Patient.Id).ToList());
        }

        await GetPatientsAsync();
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