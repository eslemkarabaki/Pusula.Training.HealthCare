using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcessManager(ITestProcessRepository testProcessRepository) : DomainService
    {
        public virtual async Task<TestProcess> CreateAsync(Guid patientId, Guid doctorId, Guid testId, string result, DateTime resultDate, Guid departmentId)
        {
            Check.NotNullOrWhiteSpace(result, nameof(result));
            Check.NotNull(patientId, nameof(patientId));
            Check.NotNull(doctorId, nameof(doctorId));
            Check.NotNull(testId, nameof(testId));

            var testProcess = new TestProcess(GuidGenerator.Create(), patientId, doctorId, testId, result, resultDate, departmentId);
            return await testProcessRepository.InsertAsync(testProcess);
        }

        public virtual async Task<TestProcess> UpdateAsync(Guid id, string result, DateTime resultDate, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(result, nameof(result));

            var testProcess = await testProcessRepository.GetAsync(id);
            testProcess.Result = result;
            testProcess.ResultDate = resultDate;

            testProcess.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await testProcessRepository.UpdateAsync(testProcess);
        }
    }
}
