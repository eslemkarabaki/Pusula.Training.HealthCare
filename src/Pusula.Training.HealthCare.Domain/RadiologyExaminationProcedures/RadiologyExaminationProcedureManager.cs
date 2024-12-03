using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class RadiologyExaminationProcedureManager(IRadiologyExaminationProcedureRepository radiologyExaminationProcedureRepository) : DomainService
    {
        public virtual async Task<RadiologyExaminationProcedure> CreateAsync(
            string result,
            DateTime resultDate,
            Guid doctorId,
            Guid protocolId,
            Guid RadiologyExaminationId
           )
        {
            var radiologyExaminationProcedure = new RadiologyExaminationProcedure(
            GuidGenerator.Create(),
            result,
            resultDate,
            doctorId,
            protocolId,
            RadiologyExaminationId
            );

            return await radiologyExaminationProcedureRepository.InsertAsync(radiologyExaminationProcedure);
        }

        public virtual async Task<RadiologyExaminationProcedure> UpdateAsync(
            Guid id,
            string result,
            DateTime resultDate,
            Guid doctorId,
            Guid protocolId,
            Guid RadiologyExaminationId)
        {
            var radiologyExaminationProcedure = await radiologyExaminationProcedureRepository.FindAsync(id);

            radiologyExaminationProcedure!.Result = result;
            radiologyExaminationProcedure!.ResultDate = resultDate;
            radiologyExaminationProcedure.DoctorId = doctorId;
            radiologyExaminationProcedure.ProtocolId = protocolId;
            radiologyExaminationProcedure.RadiologyExaminationId = RadiologyExaminationId;

            return await radiologyExaminationProcedureRepository.UpdateAsync(radiologyExaminationProcedure);
        }
    }
}
