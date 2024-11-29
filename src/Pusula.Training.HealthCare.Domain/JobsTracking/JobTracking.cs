using Pusula.Training.HealthCare.Examinations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.JobsTracking
{
    public class JobTracking : FullAuditedAggregateRoot<Guid>
    {
        public virtual string TaskName { get; set; } // Görev Adı
        public virtual string Explanation { get; set; } // Açıklama
        public virtual string DesignatedPerson { get; set; } // Atanan Kişi
        public virtual string Situation { get; set; } // Durum
        public virtual string Priority { get; set; } // Öncelik
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string TagsCategories { get; set; } // Etiketler/Kategoriler
        public virtual string Progress { get; set; } // İlerleme Durumu
        public virtual string Comments { get; set; } // Yorumlar
        public virtual string FileAttachments { get; set; } // Dosya Ekleri
        public virtual DateTime TimeTracking { get; set; } // Zaman Takibi

        // Constructor
        protected JobTracking()
        {
            TaskName = string.Empty;
            Explanation = string.Empty;
            DesignatedPerson = string.Empty;
            Situation = string.Empty;
            Priority = string.Empty;
            TagsCategories = string.Empty;
            Progress = string.Empty;
            Comments = string.Empty;
            FileAttachments = string.Empty;
        }

        public JobTracking(string? taskName, string? explanation, string? designatedPerson, string? situation, string? priority, DateTime startDate, DateTime endDate, string? tagsCategories, string? progress, string? comments, string? fileAttachments, DateTime timeTracking)
        {

            Check.NotNull(taskName, nameof(taskName));
            Check.NotNull(explanation, nameof(explanation));
            Check.NotNull(designatedPerson, nameof(designatedPerson));
            Check.NotNull(situation, nameof(situation));
            Check.NotNull(priority, nameof(priority));
            Check.NotNull(tagsCategories, nameof(tagsCategories));
            Check.NotNull(progress, nameof(progress));
            Check.NotNull(comments, nameof(comments));
            Check.NotNull(fileAttachments, nameof(fileAttachments));

            
            TaskName = taskName;
            Explanation = explanation;
            DesignatedPerson = designatedPerson;
            Situation = situation;
            Priority = priority;
            StartDate = startDate;
            EndDate = endDate;
            TagsCategories = tagsCategories;
            Progress = progress;
            Comments = comments;
            FileAttachments = fileAttachments;
        }
    }
}

