using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentCreateDto
    {  
        public string Path { get; set; }  
        public Guid ItemId { get; set; }
        public DateTime UploadDate { get; set; }
        public IFormFile File { get; set; } = null!;

    }
}
