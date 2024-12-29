using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Volo.Abp.Data;

namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceManager(IInsuranceRepository insuranceRepository) : DomainService
    {
        #region CreateAsync
        public virtual async Task<Insurance> CreateAsync(string name)
        {
            Check.Length(name, nameof(name), InsuranceConsts.NameMaxLength, InsuranceConsts.NameMinLength);

            var insurance = new Insurance(
                GuidGenerator.Create(), name);

            return await insuranceRepository.InsertAsync(insurance);

        }
        #endregion

        #region UpdateAsync
        public virtual async Task<Insurance> UpdateAsync(
            Guid id, string name,
            [CanBeNull] string? concurrencyStamp = null)
        {
            var insurance = await insuranceRepository.GetAsync(id);

            insurance.SetName(name);

            insurance.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await insuranceRepository.UpdateAsync(insurance);
        }
        #endregion
    }
}
