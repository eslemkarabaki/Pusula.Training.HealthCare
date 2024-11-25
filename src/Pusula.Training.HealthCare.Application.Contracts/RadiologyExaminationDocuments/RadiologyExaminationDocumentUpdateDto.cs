using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RadiologyExaminationDocumentConsts.DocumentNameMaxLength)]
        public string DocumentName { get; set; } = null!;
        [Required]
        [StringLength(RadiologyExaminationDocumentConsts.DocumentPathMaxLength)]
        public string DocumentPath { get; set; } = null!;
        [Required]
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public IFormFile? File { get; set; }
        public Guid RadiologyExaminationProcedureId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}
