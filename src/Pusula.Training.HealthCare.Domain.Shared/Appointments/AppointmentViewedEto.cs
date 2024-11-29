using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentViewedEto:EtoBase
    {
        public Guid Id { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}
