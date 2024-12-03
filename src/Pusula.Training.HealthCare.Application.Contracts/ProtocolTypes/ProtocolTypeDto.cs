using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class ProtocolTypeDto
    {
        public Guid Id { get;  set; }
        public virtual string? Name { get;  set; }
    }
}
