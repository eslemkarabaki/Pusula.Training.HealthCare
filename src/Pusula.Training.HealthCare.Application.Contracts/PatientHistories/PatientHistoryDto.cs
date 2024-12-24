using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.Educations;
using Pusula.Training.HealthCare.Jobs;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PatientHistories;

public class PatientHistoryDto : EntityDto<Guid>
{
    public Guid PatientId { get; set; }

    public Guid? JobId { get; set; }
    public JobDto? Job { get; set; }

    public Guid? EducationId { get; set; }
    public EducationDto? Education { get; set; }

    public bool MedicinesNotExist { get; set; }
    public bool OperationsNotExist { get; set; }
    public bool VaccinesNotExist { get; set; }
    public bool BloodTransfusionsNotExist { get; set; }
    public bool AllergiesNotExist { get; set; }

    public IList<EnumPatientHabit>? Habits { get; set; }
    public IList<EnumSpecialCase>? SpecialCases { get; set; }
    public IList<EnumBodyDevice>? BodyDevices { get; set; }
    public IList<EnumTherapy>? Therapies { get; set; }

    public ICollection<PatientHistoryMedicineDto>? Medicines { get; set; }
    // public ICollection<PatientHistoryAllergyDto> Allergies { get; set; }
    // public ICollection<PatientHistoryBloodTransfusionDto> BloodTransfusions { get; set; }
    // public ICollection<PatientHistoryVaccineDto> Vaccines { get; set; }
    // public ICollection<PatientHistoryOperationDto> Operations { get; set; }
}