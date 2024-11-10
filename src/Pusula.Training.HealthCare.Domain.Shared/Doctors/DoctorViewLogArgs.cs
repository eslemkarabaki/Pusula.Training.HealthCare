using System;
using Volo.Abp.BackgroundJobs;

namespace Pusula.Training.HealthCare.Doctors;

[BackgroundJobName("doctor-view-log")]
public class DoctorViewLogArgs
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
