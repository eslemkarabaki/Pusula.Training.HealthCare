using JetBrains.Annotations;
using Pusula.Training.HealthCare.WorkLists;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Laboratory
{
    public class WorkListManager(IWorkListRepository workListRepository) : DomainService
    {
        public virtual async Task<WorkList> CreateAsync(Guid patientId, Guid doctorId, Guid testId, DateTime scheduledDate, bool isCompleted)
        {
            var workList = new WorkList(GuidGenerator.Create(), patientId, doctorId, testId, scheduledDate, isCompleted);
            return await workListRepository.InsertAsync(workList);
        }

        public async Task<WorkList> CreateAsync(string code, string name, Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkList> UpdateAsync(Guid id, string code, string name, Guid departmentId, string concurrencyStamp)
        {
            throw new NotImplementedException();
        }
    }
}
