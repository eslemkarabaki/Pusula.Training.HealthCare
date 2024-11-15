using JetBrains.Annotations;
using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Check.Length(name, nameof(name), AppointmentTypeConsts.NameMaxLength, AppointmentTypeConsts.NameMinLength);

            var appointmentType = await appointmentTypeRepository.GetAsync(id);

            appointmentType.Name = name;

            appointmentType.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentTypeRepository.UpdateAsync(appointmentType);
        }
        #endregion
    }
}
