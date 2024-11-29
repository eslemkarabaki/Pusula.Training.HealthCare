using System;
using Volo.Abp.Application.Dtos;


namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class GetRadiologyExaminationDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
        public DateTime? UploadDate { get; set; }
        public Guid RadiologyExaminationProcedureId { get; set; }


        public GetRadiologyExaminationDocumentsInput()
        {
        }
    }
}
