using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestTypes;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestTypes
{
    public class TestTypeManager(ITestTypeRepository testTypeRepository) : DomainService
    {
        public virtual async Task<TestType> CreateAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var testType = new TestType(GuidGenerator.Create(), name);
            return await testTypeRepository.InsertAsync(testType);
        }

        public virtual async Task<TestType> UpdateAsync(Guid id, string name, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var testType = await testTypeRepository.GetAsync(id);
            testType.Name = name;

            testType.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await testTypeRepository.UpdateAsync(testType);
        }
    }
}
