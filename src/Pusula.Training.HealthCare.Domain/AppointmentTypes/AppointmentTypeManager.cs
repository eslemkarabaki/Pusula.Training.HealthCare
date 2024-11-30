using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeManager(IAppointmentTypeRepository appointmentTypeRepository):DomainService
    {
        #region CreateAsync
        public virtual async Task<AppointmentType> CreateAsync(string name)
        {
            Check.Length(name, nameof(name), AppointmentTypeConsts.NameMaxLength, AppointmentTypeConsts.NameMinLength);

            var appointmentType = new AppointmentType(
                GuidGenerator.Create(), name);

            return await appointmentTypeRepository.InsertAsync(appointmentType);

        }
        #endregion

        #region UpdateAsync
        public virtual async Task<AppointmentType> UpdateAsync(
            Guid id, string name,
            [CanBeNull] string? concurrencyStamp = null)
        {
            var appointmentType = await appointmentTypeRepository.GetAsync(id);

            appointmentType.SetName(name);

            appointmentType.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentTypeRepository.UpdateAsync(appointmentType);
        }
        #endregion
    }
}
