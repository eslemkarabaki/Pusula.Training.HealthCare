using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public static class RadiologyExaminationConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RadiologyExamination." : string.Empty);
        }

        public const int NameMaxLength = 128; 
        public const int MaxCodeLength = 32; 
    }
}
