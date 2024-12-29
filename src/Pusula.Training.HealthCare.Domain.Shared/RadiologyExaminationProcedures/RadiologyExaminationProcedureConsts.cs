using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public static class RadiologyExaminationProcedureConsts
    {
        private const string DefaultSorting = "{0}Result asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RadiologyExaminationProcedure." : string.Empty);
        }

        public const int MaxResultLength = 1024; 
        public const int MinResultLength = 1;
        public static readonly DateTime MinResultDate = new DateTime(2000, 1, 1);
        public static readonly DateTime MaxResultDate = DateTime.MaxValue;


    }
}
