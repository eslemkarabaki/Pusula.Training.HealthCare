using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class GetProtocolTypeInput : PagedAndSortedResultRequestDto
    {
        public Guid Id { get; set; }
        public virtual string? Name { get; set; }
    }
}
