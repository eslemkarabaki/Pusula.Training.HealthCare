using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public static class AppointmentConsts
    {
        private const string DefaultSorting = "{0}StartTime desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Appointment." : string.Empty);
        }

        public const int NoteMinLength = 1;
        public const int NoteMaxLength = 128;
    }
}
