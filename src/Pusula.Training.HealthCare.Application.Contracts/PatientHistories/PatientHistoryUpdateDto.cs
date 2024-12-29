using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.Educations;
using Pusula.Training.HealthCare.Jobs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistories;

public class PatientHistoryUpdateDto : EntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid PatientId { get; set; }

    public Guid? JobId { get; set; }
    public JobUpdateDto? Job { get; set; } = new();

    public Guid? EducationId { get; set; }
    public EducationUpdateDto? Education { get; set; } = new();

    public ICollection<EnumPatientHabit> Habits { get; set; } = [];
    public ICollection<EnumSpecialCase> SpecialCases { get; set; } = [];
    public ICollection<EnumBodyDevice> BodyDevices { get; set; } = [];
    public ICollection<EnumTherapy> Therapies { get; set; } = [];

    public bool MedicinesNotExist { get; set; }
    public bool OperationsNotExist { get; set; }
    public bool VaccinesNotExist { get; set; }
    public bool BloodTransfusionsNotExist { get; set; }
    public bool AllergiesNotExist { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;

    public bool HasEnum<TEnum>(Func<PatientHistoryUpdateDto, ICollection<TEnum>> enums, TEnum value) =>
        enums(this).Contains(value);

    public void ToggleEnum<TEnum>(Func<PatientHistoryUpdateDto, ICollection<TEnum>> enums, TEnum value)
    {
        if (HasEnum(enums, value))
            enums(this).Remove(value);
        else
            enums(this).Add(value);
    }

    public void SetEnum<TEnum>(Func<PatientHistoryUpdateDto, ICollection<TEnum>> enums, TEnum value)
    {
        enums(this).RemoveAll(e => !e.Equals(value));
        ToggleEnum(enums, value);
    }
}