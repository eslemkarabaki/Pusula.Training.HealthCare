using System;
using System.Collections.Generic;
namespace Pusula.Training.HealthCare.HistorysTrackable
{
    

    public class HistoryService
    {
        private readonly List<HistoryEntry> _history = new List<HistoryEntry>();
        private string? _currentValue;

        public string CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _history.Add(new HistoryEntry(DateTime.Now, _currentValue));
                    _currentValue = value;
                }
            }
        }

        public IReadOnlyList<HistoryEntry> History => _history.AsReadOnly();
    }
}
