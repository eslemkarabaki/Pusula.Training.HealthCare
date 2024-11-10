using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Doctors
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Doctor")]
    [Route("api/app/doctors")]
    public class DoctorController : HealthCareController, IDoctorsAppService
    {
        private readonly IDoctorsAppService _doctorsAppService;

        public DoctorController(IDoctorsAppService doctorsAppService)
        {
            _doctorsAppService = doctorsAppService ?? throw new ArgumentNullException(nameof(doctorsAppService));
        }

        // Get list of doctors with pagination
        [HttpGet]
        public virtual Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input)
        {
            return _doctorsAppService.GetListAsync(input);
        }

        // Get a single doctor by ID
        [HttpGet("{id}")]
        public virtual Task<DoctorDto> GetAsync(Guid id)
        {
            return _doctorsAppService.GetAsync(id);
        }

        // Create a new doctor
        [HttpPost]
        public virtual Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {
            return _doctorsAppService.CreateAsync(input);
        }

        // Update an existing doctor by ID
        [HttpPut("{id}")]
        public virtual Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
        {
            return _doctorsAppService.UpdateAsync(id, input);
        }

        // Delete a doctor by ID
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _doctorsAppService.DeleteAsync(id);
        }

        // Export doctor list as Excel file
        [HttpGet("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            return _doctorsAppService.GetListAsExcelFileAsync(input);
        }

        // Get a token for downloading
        [HttpGet("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _doctorsAppService.GetDownloadTokenAsync();
        }

        // Delete multiple doctors by IDs
        [HttpDelete("")]
        public virtual Task DeleteByIdsAsync([FromBody] List<Guid> doctorIds)
        {
            return _doctorsAppService.DeleteByIdsAsync(doctorIds);
        }

        // Delete all doctors with specific filter
        [HttpDelete("all")]
        public virtual Task DeleteAllAsync(GetDoctorsInput input)
        {
            return _doctorsAppService.DeleteAllAsync(input);
        }
    }
}
