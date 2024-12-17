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
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class Examinations
{
    [Parameter]
    public int ProtocolNo { get; set; }
}