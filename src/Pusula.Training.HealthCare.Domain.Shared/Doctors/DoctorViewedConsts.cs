using System;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorViewedEto : EtoBase
{
    public Guid Id { get; set; } 

    public DateTime ViewedAt { get; set; }   
}
