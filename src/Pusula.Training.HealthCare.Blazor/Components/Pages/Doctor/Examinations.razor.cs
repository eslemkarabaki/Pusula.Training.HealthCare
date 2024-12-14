using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Permissions;
using Syncfusion.Blazor.PivotView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Doctor;

public partial class Examinations
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = [];
    public GetExaminationsInput ExaminationFilter { get; set; } = new();
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

    protected PageToolbar Toolbar { get; } = new();
    protected bool ShowAdvancedFilters { get; set; }
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; } = string.Empty;
    private int TotalCount { get; set; }
    private ExaminationCreateDto NewExamination { get; set; } = new();
    private Validations NewExaminationValidations { get; set; } = new();
    private ExaminationUpdateDto EditingExamination { get; set; }
    private Validations EditingExaminationValidations { get; set; } = new();
    private Guid EditingExaminationId { get; set; }
    private Modal CreateExaminationModal { get; set; } = new();
    private Modal EditExaminationModal { get; set; } = new();

    private DataGridEntityActionsColumn<ExaminationDto> EntityActionsColumn { get; set; } = new();
    protected string SelectedCreateTab = "examination-create-tab";
    protected string SelectedEditTab = "examination-edit-tab";
    private List<ExaminationDto> SelectedExaminations { get; set; } = [];
    private bool AllExaminationsSelected { get; set; }
    public IReadOnlyList<ExaminationDto> ExaminationList { get; set; } = Array.Empty<ExaminationDto>();
    public string? FilterText { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await GetExaminationsAsync();
        await SetPermissionsAsync();
    }

    public async Task GetExaminationsAsync()
    {
        ExaminationFilter.MaxResultCount = PageSize;
        ExaminationFilter.SkipCount = (CurrentPage - 1) * PageSize;
        ExaminationFilter.Sorting = CurrentSorting;

        var result = await ExaminationAppService.GetListAsync(ExaminationFilter);
        ExaminationList = result.Items;
        TotalCount = (int)result.TotalCount;

        await ClearSelection();
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

    protected virtual async Task SearchAsync()
    {
        CurrentPage = 1;
        await GetExaminationsAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OnNameChangedAsync(string? filterText)
    {
        ExaminationFilter.FilterText = filterText;
        await SearchAsync();
    }

    protected virtual async Task OnVisitDateChangedAsync(DateTime? visitDate)
    {
        ExaminationFilter.VisitDate = visitDate.HasValue ? visitDate.Value.Date : visitDate;
        await SearchAsync();
    }

    private async Task DownloadAsExcelAsync()
    {
        // Get the download token
        var token = (await ExaminationAppService.GetDownloadTokenAsync()).Token;

        // Get the remote service configuration
        var remoteService =
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("HealthCare") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");

        // Validate the BaseUrl
        var baseUrl = remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty;
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new InvalidOperationException("Base URL is not configured properly.");
        }

        // Get the current culture
        var culture = CultureInfo.CurrentCulture.Name ?? CultureInfo.InvariantCulture.Name;
        if (!string.IsNullOrEmpty(culture))
        {
            culture = $"&culture={culture}";
        }

        // Construct the URL
        var url =
            $"{baseUrl}api/app/examinations/as-excel-file?DownloadToken={token}&FilterText={HttpUtility.UrlEncode(ExaminationFilter.FilterText)}{culture}";

        // Log the URL for debugging
        Console.WriteLine($"Constructed URL: {url}");

        // Perform the HTTP GET request to download the file
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);

            // Check the response status
            if (response.IsSuccessStatusCode)
            {
                await File.WriteAllBytesAsync(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ExaminationsData.xlsx"
                    ),
                    await response.Content.ReadAsByteArrayAsync()
                );
            } else
            {
                Console.WriteLine($"Failed to download file. Status Code: {response.StatusCode}");
            }
        }
    }

    private async Task OpenCreateExaminationModalAsync()
    {
        NewExamination = new ExaminationCreateDto { };

        SelectedCreateTab = "examination-create-tab";

        await NewExaminationValidations.ClearAll();
        await CreateExaminationModal.Show();
    }

    protected virtual ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Examinations"]));
        return ValueTask.CompletedTask;
    }

    protected virtual ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["ExportToExcel"], DownloadAsExcelAsync, IconName.Download);

        Toolbar.AddButton(
            L["NewExamination"], OpenCreateExaminationModalAsync, IconName.Add,
            requiredPolicyName: HealthCarePermissions.Examinations.Create
        );

        return ValueTask.CompletedTask;
    }

#region permissions

    private bool CanCreateExamination { get; set; }
    private bool CanEditExamination { get; set; }
    private bool CanDeleteExamination { get; set; }

    private async Task SetPermissionsAsync()
    {
        CanCreateExamination = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Examinations.Create);
        CanEditExamination = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Examinations.Edit);
        CanDeleteExamination = await AuthorizationService
            .IsGrantedAsync(HealthCarePermissions.Examinations.Delete);
    }

#endregion

    private Task SelectedExaminationRowsChanged()
    {
        if (SelectedExaminations.Count != PageSize)
        {
            AllExaminationsSelected = false;
        }

        return Task.CompletedTask;
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ExaminationDto> e)
    {
        CurrentSorting = e
                         .Columns
                         .Where(c => c.SortDirection != SortDirection.Default)
                         .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                         .JoinAsString(",");
        CurrentPage = e.Page;
        await GetExaminationsAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task OpenEditExaminationModalAsync(ExaminationDto input)
    {
        SelectedEditTab = "examination-edit-tab";

        var examination = await ExaminationAppService.GetAsync(input.Id);

        EditingExaminationId = examination.Id;
        EditingExamination = ObjectMapper.Map<ExaminationDto, ExaminationUpdateDto>(examination);

        await EditingExaminationValidations.ClearAll();
        await EditExaminationModal.Show();
    }

    private async Task DeleteExaminationAsync(ExaminationDto input)
    {
        await ExaminationAppService.DeleteAsync(input.Id);
        await GetExaminationsAsync();
    }

    private async Task CloseCreateExaminationModalAsync()
    {
        NewExamination = new ExaminationCreateDto { };
        await CreateExaminationModal.Hide();
    }

    private async Task CreateExaminationAsync()
    {
        try
        {
            if (await NewExaminationValidations.ValidateAll() == false)
            {
                return;
            }

            await ExaminationAppService.CreateAsync(NewExamination);
            await GetExaminationsAsync();
            await CloseCreateExaminationModalAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private async Task CloseEditExaminationModalAsync() => await EditExaminationModal.Hide();

    private async Task UpdateExaminationAsync()
    {
        try
        {
            if (await EditingExaminationValidations.ValidateAll() == false)
            {
                return;
            }

            await ExaminationAppService.UpdateAsync(EditingExaminationId, EditingExamination);
            await GetExaminationsAsync();
            await EditExaminationModal.Hide();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private void OnSelectedCreateTabChanged(string name) => SelectedCreateTab = name;

    private void OnSelectedEditTabChanged(string name) => SelectedEditTab = name;

    private Task SelectAllItems()
    {
        AllExaminationsSelected = true;

        return Task.CompletedTask;
    }

    private Task ClearSelection()
    {
        AllExaminationsSelected = false;
        SelectedExaminations.Clear();

        return Task.CompletedTask;
    }

    private async Task DeleteSelectedExaminationsAsync()
    {
        var message = AllExaminationsSelected ?
            L["DeleteAllRecords"].Value :
            L["DeleteSelectedRecords", SelectedExaminations.Count].Value;

        if (!await UiMessageService.Confirm(message))
        {
            return;
        }

        if (AllExaminationsSelected)
        {
            await ExaminationAppService.DeleteAllAsync(ExaminationFilter);
        } else
        {
            await ExaminationAppService.DeleteByIdsAsync(SelectedExaminations.Select(x => x.Id).ToList());
        }

        SelectedExaminations.Clear();
        AllExaminationsSelected = false;

        await GetExaminationsAsync();
    }
}