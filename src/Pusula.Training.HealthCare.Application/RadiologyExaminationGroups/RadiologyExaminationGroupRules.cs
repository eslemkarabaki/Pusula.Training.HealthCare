using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroupRules(IRadiologyExaminationGroupRepository radiologyExaminationGroupRepository)
    {
        public async Task EnsureNameNotExistsAsync(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                await radiologyExaminationGroupRepository.AnyAsync(e => e.Name == name))
            {
                throw new UserFriendlyException("A radiology examination group with the same name already exists.");
            }
        }

        public void EnsureNameIsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new UserFriendlyException("The name field cannot be empty. Please provide a valid name.");
            }

            if (name.Length > 100)
            {
                throw new UserFriendlyException("The name is too long. Please provide a name with fewer than 100 characters.");
            }
        }

        public void EnsureDescriptionIsValid(string description)
        {
            if (description?.Length > 500)
            {
                throw new UserFriendlyException("The description is too long. Please provide a description with fewer than 500 characters.");
            }
        }

        public async Task EnsureRadiologyExaminationGroupExistsAsync(Guid id)
        {
            var exists = await radiologyExaminationGroupRepository.AnyAsync(e => e.Id == id);
            if (!exists)
            {
                throw new UserFriendlyException("The specified radiology examination group could not be found. Please check the ID and try again.");
            }
        }
    }

}
