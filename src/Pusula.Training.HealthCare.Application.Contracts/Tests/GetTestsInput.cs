using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.Tests
{
    public class GetTestsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public Guid? TestGroupId { get; set; }

        public string ToQueryParameterString(string? culture = null)
        {
            var parameters = new System.Text.StringBuilder();
            if (!string.IsNullOrWhiteSpace(culture))
            {
                parameters.Append($"&culture={culture}");
            }

            parameters.Append($"&FilterText={System.Web.HttpUtility.UrlEncode(FilterText)}");
            parameters.Append($"&Code={System.Web.HttpUtility.UrlEncode(Code)}");
            parameters.Append($"&Name={System.Web.HttpUtility.UrlEncode(Name)}");
            parameters.Append($"&TestGroupId={TestGroupId}");
            return parameters.ToString();
        }
    }
}
