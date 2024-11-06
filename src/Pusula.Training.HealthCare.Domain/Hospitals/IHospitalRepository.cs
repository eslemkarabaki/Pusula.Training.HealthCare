using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Hospitals
{
    public interface IHospitalRepository : IRepository<Hospital, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            string? address = null,
            CancellationToken cancellationToken = default);

        Task<List<Hospital>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? address = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );
        Task<List<HospitalWithDepartment>> GetListAsync(
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<HospitalWithDepartment> GetAsync(Guid id, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? address = null,
            CancellationToken cancellationToken = default);
    }
}
