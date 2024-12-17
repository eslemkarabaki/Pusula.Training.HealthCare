using System;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class ExaminationPhysicalManager
    {
        private readonly IExaminationPhysicalRepository _repository;

        public ExaminationPhysicalManager(IExaminationPhysicalRepository repository)
        {
            _repository = repository;
        }

        public async Task<ExaminationPhysical> CreateAsync(
            Guid examinationId,
            float? weight,
            float? height,
            float? bodyMassIndex,
            float? vitalAge,
            float? fever,
            float? pulse,
            float? systolicBloodPressure,
            float? diastolicBloodPressure,
            float? spo2,
            string physicalNote)
        {
            // Validasyon işlemleri yapılabilir.
           

            // Yeni ExaminationPhysical nesnesi oluşturuluyor
            var examinationPhysical = new ExaminationPhysical(
                Guid.NewGuid(),
                examinationId,
                weight,
                height,
                bodyMassIndex,
                vitalAge,
                fever,
                pulse,
                systolicBloodPressure,
                diastolicBloodPressure,
                spo2,
                physicalNote
            );

            // Veritabanına kaydediyoruz
            await _repository.InsertAsync(examinationPhysical);

            return examinationPhysical;
        }
    }
}
