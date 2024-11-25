using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentCreateDto
    {
        [StringLength(RadiologyExaminationDocumentConsts.DocumentNameMaxLength)]
        public string DocumentName { get; set; } = null!;
        public Guid RadiologyExaminationProcedureId { get; set; }

        [Required]
        public IFormFile File { get; set; } = null!;

    }
}
