using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestGroups
{
    public class TestGroupManager(ITestGroupRepository testGroupRepository) : DomainService
    {
        public virtual async Task<TestGroup> CreateAsync(string code, string name)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var testGroup = new TestGroup(GuidGenerator.Create(), code, name);
            return await testGroupRepository.InsertAsync(testGroup);
        }

        public virtual async Task<TestGroup> UpdateAsync(Guid id, string code, string name, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var testGroup = await testGroupRepository.GetAsync(id);
            testGroup.Code = code;
            testGroup.Name = name;

            testGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await testGroupRepository.UpdateAsync(testGroup);
        }

        public async Task<TestGroup> UpdateAsync(Guid id, string code, string name, object concurrencyStamp)
        {
            throw new NotImplementedException();
        }
    }
}
