using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public interface IAppointmentReportRepository:IRepository<AppointmentReport,Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            DateTime? reportDate = null, string? priorityNotes=null,
            string? doctorNotes=null, Guid? appointmentId=null,
            CancellationToken cancellationToken=default);

        Task<AppointmentReportWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken=default);

        Task<List<AppointmentReportWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText =null, DateTime? reportDate = null, 
            string? priorityNotes = null, string? doctorNotes = null, 
            Guid? appointmentId = null, string? sorting = null, 
            int maxResultCount = int.MaxValue, int skipCount = 0, 
            CancellationToken cancellationToken = default);
        
        Task<List<AppointmentReport>> GetListAsync(
            string? filterText = null, DateTime? reportDate = null,
            string? priorityNotes = null, string? doctorNotes = null,
            Guid? appointmentId = null, string? sorting = null,
            int maxResultCount = int.MaxValue, int skipCount = 0, 
            CancellationToken cancellationToken = default);
        
        Task<long> GetCountAsync(
            string? filterText = null, DateTime? reportDate = null,
            string? priorityNotes = null, string? doctorNotes = null,
            Guid? appointmentId = null, CancellationToken cancellationToken = default);
    }
}
