using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public static class AppointmentReportConsts
    {

        private const string DefaultSorting = "{0}ReportDate desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "AppointmentReport." : string.Empty);
        }

        public const int PriorityNotesMinLength = 1;
        public const int PriorityNotesMaxLength = 128;

        public const int DoctorNotesMinLength = 1;
        public const int DoctorNotesMaxLength = 128;

        
    }
}
