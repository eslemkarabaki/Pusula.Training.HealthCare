using System;
using Volo.Abp.Application.Dtos;


namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class GetRadiologyExaminationDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; } 
        public string? Path { get; set; }
        public DateTime? UploadDate { get; set; }
        public Guid ItemId { get; set; }


        public GetRadiologyExaminationDocumentsInput()
        {
        }
    }
}
