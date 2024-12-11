using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.DeepCloner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Doctors;
using Pusula.Training.HealthCare.Blazor.Components.Dialogs.Protocols;
using Pusula.Training.HealthCare.Blazor.Extensions;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.SplitButtons;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages;

public partial class Doctors
{
    private SfGrid<DoctorWithNavigationPropertiesDto> SfGrid { get; set; } = null!;

    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];

    private int PageSize => 50;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }

    private IReadOnlyList<DoctorWithNavigationPropertiesDto> DoctorList { get; set; } = [];
    private IEnumerable<DepartmentDto> DepartmentList { get; set; } = [];
    private GetDoctorsInput Filter { get; set; }
    private GetDoctorsInput LastFilter { get; set; } = new();

    private bool AllDoctorsSelected { get; set; }

    public Doctors()
    {
        Filter = new GetDoctorsInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetDoctorsAsync();
        DepartmentList = await DepartmentsAppService.GetListDepartmentsAsync();
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
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Doctors"]));
        return ValueTask.CompletedTask;
    }

    private async Task GetDoctorsAsync()
    {
        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await DoctorAppService.GetListWithNavigationPropertiesAsync(Filter);
        DoctorList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
    }

    protected virtual async Task SearchAsync()
    {
        if (Filter.DeepCompare(LastFilter))
        {
            return;
        }

        LastFilter = Filter.DeepClone();

        CurrentPage = 1;
        await GetDoctorsAsync();
        await InvokeAsync(StateHasChanged);
    }

#region DataGrid

    private Task ClearSelection()
    {
        AllDoctorsSelected = false;
        SfGrid.SelectedRecords.Clear();

        return Task.CompletedTask;
    }

    private Task SelectedDoctorRowChangedAsync()
    {
        AllDoctorsSelected = DoctorList.Count < PageSize ?
            SfGrid.SelectedRecords.Count == DoctorList.Count :
            SfGrid.SelectedRecords.Count == PageSize;

        return Task.CompletedTask;
    }

#endregion

#region Create

    private DoctorCreateDialog CreateDoctorDialog { get; set; } = null!;

    private async Task OpenCreateDoctorDialogAsync() => await CreateDoctorDialog.ShowAsync();

#endregion

#region Update

    private Guid EditingDoctorId { get; set; }
    private DoctorUpdateDialog UpdateDoctorDialog { get; set; } = null!;

    private async Task OpenEditDoctorModalAsync(Guid id)
    {
        EditingDoctorId = id;
        await UpdateDoctorDialog.ShowAsync(EditingDoctorId);
    }

#endregion

#region Delete

    private async Task DeleteDoctorAsync(Guid id)
    {
        var isConfirm = await DialogService.ConfirmAsync("Are you sure you want to delete these item?", "Delete Item");
        if (isConfirm)
        {
            await DoctorAppService.DeleteAsync(id);
            await GetDoctorsAsync();
        }
    }

    private async Task DeleteSelectedDoctorsAsync()
    {
        var message = AllDoctorsSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SfGrid.SelectedRecords.Count].Value;
        var isConfirm = await DialogService.ConfirmAsync(message, "Delete Item");

        if (!isConfirm)
        {
            return;
        }

        if (AllDoctorsSelected)
        {
            await DoctorAppService.DeleteAllAsync(Filter);
        } else
        {
            await DoctorAppService.DeleteByIdsAsync(SfGrid.SelectedRecords.Select(x => x.Doctor.Id).ToList());
        }

        await GetDoctorsAsync();
    }

#endregion

#region Permission

    private bool CanCreateDoctor { get; set; }
    private bool CanEditDoctor { get; set; }
    private bool CanDeleteDoctor { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateDoctor = await AbpAuthorizationServiceExtensions.IsGrantedAsync(
            AuthorizationService, HealthCarePermissions.Doctors.Create
        );
        CanEditDoctor = await AbpAuthorizationServiceExtensions.IsGrantedAsync(
            AuthorizationService, HealthCarePermissions.Doctors.Edit
        );
        CanDeleteDoctor = await AbpAuthorizationServiceExtensions.IsGrantedAsync(
            AuthorizationService, HealthCarePermissions.Doctors.Delete
        );
    }

#endregion
}