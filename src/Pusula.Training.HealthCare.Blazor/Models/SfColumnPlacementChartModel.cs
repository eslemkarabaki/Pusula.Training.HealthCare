using System;
using System.Collections.Generic;
using System.Linq;

namespace Pusula.Training.HealthCare.Blazor.Models;

public class SfColumnPlacementChartModel(string x, int y, string name)
{
    public string X { get; init; } = x;
    public int Y { get; init; } = y;
    public string Name { get; init; } = name;

    public static IEnumerable<SfColumnPlacementChartModel> FromGroup<TKey, TSource>(
        IEnumerable<IGrouping<TKey, TSource>> groupData,
        Func<TSource, string> y,
        Func<TSource, string> name
    ) where TSource : class
    {
        ICollection<SfColumnPlacementChartModel> models = [];
        foreach (var data in groupData)
        {
            models.Add(new SfColumnPlacementChartModel(y(data.First()), data.Count(), name(data.First())));
        }

        return models;
    }
}