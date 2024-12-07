using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using System.Linq;
using FilteringEventArgs = Syncfusion.Blazor.DropDowns.FilteringEventArgs;
using Pusula.Training.HealthCare.Protocols;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;


namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class AppointmentReports
    {
        private EditContext FilterContext { get; set; }
        private int PageSize => 50;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private DateTime Today => DateTime.Today;
        private DateTime Tomorrow => Today.AddDays(1).AddMinutes(-1);
        private bool TodayIsSaturday => Today.DayOfWeek is DayOfWeek.Saturday; 
        private bool TodayIsSunday => Today.DayOfWeek is DayOfWeek.Sunday;
        private DateTime ThisWeekStart => TodayIsSaturday ? Today.AddDays(2) :  TodayIsSunday ? Today.AddDays(1) : Today.AddDays(-((int)Today.DayOfWeek - 1));
        private DateTime ThisMonthStart => new(Today.Year, Today.Month, 1);

        private IEnumerable<AppointmentTypeDto> AppointmentTypeList { get; set; } = [];
        private IReadOnlyList<AppointmentWithNavigationPropertiesDto> AppointmentList { get; set; } = [];
        private SfGrid<AppointmentWithNavigationPropertiesDto> SfGrid { get; set; } = null!; 
        private GetAppointmentsInput Filter { get; set; }

        public AppointmentReports()
        {
            Filter = new GetAppointmentsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting,
                StartTime = Today,
                EndTime = Tomorrow
            };
            FilterContext = new EditContext(Filter);
        }

        protected override async Task OnInitializedAsync() =>
       AppointmentTypeList = await AppointmentTypeAppService.GetListAsync();

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetAppointmentsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task GetAppointmentsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            AppointmentList = await AppointmentAppService.GetListWithNavigationPropertiesAsync(Filter);
        }

        #region Doctor 
        private IEnumerable<DoctorDto> DoctorList { get; set; } = [];
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

        #region Patient 
        private IEnumerable<PatientDto> PatientList { get; set; } = [];
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

        #region Department
        private SfAutoComplete<Guid?, DepartmentDto> DepartmentFilterAutoComplete { get; set; } = null!;
        private GetDepartmentsInput GetDepartmentsInput { get; set; } = new() { MaxResultCount = 10 };
        private IEnumerable<DepartmentDto> DepartmentList { get; set; } = [];

        private async Task FilterDepartmentAsync(FilteringEventArgs args)
        {
            args.PreventDefaultAction = true;
            GetDepartmentsInput.Name = args.Text;
            var departments = await DepartmentsAppService.GetListAsync(GetDepartmentsInput);
            DepartmentList = departments.Items;
            await DepartmentFilterAutoComplete.FilterAsync(DepartmentList);
        }
    }
    #endregion

}
