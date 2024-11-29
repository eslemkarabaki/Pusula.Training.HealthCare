using Pusula.Training.HealthCare.WorkLists;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Laboratory
{
    public interface IWorkListRepository : IRepository<WorkList, Guid>
    {
        Task<List<WorkList>> GetListAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null,
            CancellationToken cancellationToken = default);
        Task DeleteAllAsync(string? filterText, string? code, string? name, Guid? departmentId);
        Task<List<WorkList>> GetListAsync(string? filterText, string? code, string? name, Guid? departmentId, string? sorting, int maxResultCount, int skipCount);
        Task<long> GetCountAsync(string? filterText, string? code, string? name, Guid? departmentId);
        Task<List<WorkList>> GetListAsync(string? filterText, string? code, string? name, Guid? departmentId);
    }
}
