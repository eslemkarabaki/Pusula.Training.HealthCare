using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationAnamnez : Entity<Guid>
    {
        public virtual Guid ExaminationId { get;  set; }
        public virtual string Complaint { get;  set; } // Şikayet
        public virtual string History { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime Date { get; set; }
        protected ExaminationAnamnez() 
        { 
         Complaint = string.Empty;
         History = string.Empty;
        } 
        public ExaminationAnamnez(Guid id, Guid examinationId, string complaint, string history, DateTime startDate)
        {
            Check.NotDefaultOrNull<Guid>(id, nameof(id));
            Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
            Check.NotNull(complaint, nameof(complaint));
            Check.Length(complaint, nameof(complaint), ExaminationAnamnezConsts.ComplaintNumberMaxLength, 0);
            Check.NotNull(history, nameof(history));
            Check.Length(history, nameof(history), ExaminationAnamnezConsts.HistoryNumberMaxLength, 0);
            Check.NotDefaultOrNull<DateTime>(startDate, nameof(StartDate));
            Id = id;
            ExaminationId = examinationId;
            Complaint = complaint;
            History = history;
            StartDate = startDate;
            Date = DateTime.Now;
        }
    }
}
