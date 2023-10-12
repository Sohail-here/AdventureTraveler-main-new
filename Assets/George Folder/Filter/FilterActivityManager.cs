using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterActivityManager : MonoBehaviour
{

    public List<string> Filter;

    private void Start()
    {
        Filter = new List<string>();
    }

    public void ShowOnlyFilteredActivities()
    {
        string[] _Filter = Filter.ToArray();
        if (Filter.Count == 0)
        {
            _Filter = null;
        }

        ActivityDisplayManager.Instance.DisplayVendorsByFilter(_Filter);
    }

    public void AddFilter(string filterKey)
    {
        Filter.Add(filterKey);
    }

    public void ClearFilter()
    {
        Filter.Clear();
    }

    public void RemoveFilter(string filterKey)
    {
        Filter.Remove(filterKey);
    }

}
