using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeCreateDto
    {
        [Required]
        [StringLength(AppointmentTypeConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
