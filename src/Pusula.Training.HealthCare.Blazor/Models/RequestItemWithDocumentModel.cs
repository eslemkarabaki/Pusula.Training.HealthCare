using Microsoft.AspNetCore.Http;

namespace Pusula.Training.HealthCare.Blazor.Models
{
    public class RequestItemWithDocumentModel
    { 
        public string Result { get; set; }
        public IFormFile? File { get; set; }
    }
}
