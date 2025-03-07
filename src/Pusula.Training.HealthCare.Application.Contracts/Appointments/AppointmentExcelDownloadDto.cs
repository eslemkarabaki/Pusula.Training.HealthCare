﻿using System;
using System.Collections.Generic;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;
        public string? FilterText { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<EnumAppointmentStatus>? Statuses { get; set; }
        public string? Notes { get; set; }
        public Guid? AppointmentTypeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? PatientId { get; set; }

        public AppointmentExcelDownloadDto()
        {

        }
    }
}
