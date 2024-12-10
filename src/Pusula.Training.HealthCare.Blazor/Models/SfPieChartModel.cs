using System;
using System.Collections.Generic;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Models;

public class SfPieChartModel(string label, double percentage)
{
    public string Label { get; init; } = label;
    public double Percent { get; init; } = percentage;
    public string DataLabel { get; init; } = $"{label}: {percentage:N2}%";

    public static IEnumerable<SfPieChartModel> FromGroup<TKey, TSource>(
        int totalCount,
        IEnumerable<IGrouping<TKey, TSource>> groupData,
        Func<TSource, string> label
    ) where TSource : class
    {
        ICollection<SfPieChartModel> models = [];
        foreach (var data in groupData)
        {
            models.Add(new SfPieChartModel(label(data.First()), GetPercentage(data.Count(), totalCount)));
        }

        return models;
    }

    private static double GetPercentage(double current, double total) => current / total * 100;
}