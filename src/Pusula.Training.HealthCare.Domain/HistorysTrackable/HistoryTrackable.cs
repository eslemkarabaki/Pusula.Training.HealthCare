using System;
using Volo.Abp;
namespace Pusula.Training.HealthCare.HistorysTrackable
{
    public class HistoryEntry
    {
        public virtual DateTime Timestamp { get; set; }
        public virtual string? Value { get; set; }

        protected HistoryEntry() {
            Value = string.Empty;
        }
        public HistoryEntry(DateTime timestamp, string? value)
        {
            
          Check.NotNull(value, nameof(value));
            Timestamp = timestamp;
            Value = value;
        }
    }
}
