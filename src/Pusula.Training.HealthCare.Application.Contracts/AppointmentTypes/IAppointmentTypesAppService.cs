﻿using Pusula.Training.HealthCare.AppointmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public interface IAppointmentTypesAppService : IApplicationService
    {
        Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input);

        Task<AppointmentTypeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input);

        Task<AppointmentTypeDto> UpdateAsync(Guid id, AppointmentTypeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentTypeExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> appointmentTypeIds);

        Task DeleteAllAsync(GetAppointmentTypesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
