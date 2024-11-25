using JetBrains.Annotations;
using Pusula.Training.HealthCare.Tests;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Tests
{
    public class TestManager(ITestRepository testRepository) : DomainService
    {
        public virtual async Task<Test> CreateAsync(string code, string name, Guid GroupId, Guid testTypeId)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(GroupId, nameof(GroupId));
            Check.NotNull(testTypeId, nameof(testTypeId));

            var test = new Test(GuidGenerator.Create(), code, name, GroupId, testTypeId);
            return await testRepository.InsertAsync(test);
        }

        public virtual async Task<Test> UpdateAsync(Guid id, string code, string name, Guid groupId, Guid testTypeId, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(groupId, nameof(groupId));
            Check.NotNull(testTypeId, nameof(testTypeId));

            var test = await testRepository.GetAsync(id);
            test.Code = code;
            test.Name = name;
            test.GroupId = groupId;
            test.TestTypeId = testTypeId;

            test.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await testRepository.UpdateAsync(test);
        }
    }
}
