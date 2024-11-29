using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public interface IRadiologyExaminationProcedureRepository : IRepository<RadiologyExaminationProcedure, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            string? result = null,
            DateTime? resultDate = null,
            Guid? doctorId = null,
            Guid? protocolId = null,
            Guid? RadiologyExaminationId = null, 
            CancellationToken cancellationToken = default);
        Task<List<RadiologyExaminationProcedure>> GetListAsync(
                    string? filterText = null,
                    string? result = null,
                    DateTime? resultDate = null,
                    Guid? doctorId = null,
                    Guid? protocolId = null,
                    Guid? RadiologyExaminationId = null, 
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? result = null,
            DateTime? resultDate = null,
            Guid? doctorId = null,
            Guid? protocolId = null,
            Guid? RadiologyExaminationId = null, 
            CancellationToken cancellationToken = default);
    }
}
