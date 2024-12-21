using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Medicines;

public class Medicine : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    public ICollection<PatientHistoryMedicine> Medicines { get; set; }

    protected Medicine()
    {
        Name = string.Empty;
        Medicines = [];
    }

    public Medicine(Guid id, string name) : base(id)
    {
        SetName(name);
        Medicines = [];
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), MedicineConsts.NameMaxLength);
}