using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterScript : MonoBehaviour
{
    public FilterActivityManager Filters;
    
    public void ToggleFilter_Indoor(bool toggle)
    {
        string filterValue = "Indoor activity";
        if (toggle == true)
        {
            Filters.AddFilter(filterValue);
        }
        else
        {
            Filters.RemoveFilter(filterValue);
        }
    }

    public void ToggleFilter_Outdoor(bool toggle)
    {
        string filterValue = "Outdoor activity";
        if (toggle == true)
        {
            Filters.AddFilter(filterValue);
        }
        else
        {
            Filters.RemoveFilter(filterValue);
        }
    }

    public void ToggleFilter_Warm(bool toggle)
    {
        string filterValue = "Warn activity";
        if (toggle == true)
        {
            Filters.AddFilter(filterValue);
        }
        else
        {
            Filters.RemoveFilter(filterValue);
        }
    }

    public void ToggleFilter_Cold(bool toggle)
    {
        string filterValue = "Cold activity";
        if (toggle == true)
        {
            Filters.AddFilter(filterValue);
        }
        else
        {
            Filters.RemoveFilter(filterValue);
        }
    }

    public void ToggleFilter_Other(bool toggle)
    {
        string filterValue = "Other activity";
        if (toggle == true)
        {
            Filters.AddFilter(filterValue);
        }
        else
        {
            Filters.RemoveFilter(filterValue);
        }
    }

    public void ToggleFilter_All(bool toggle)
    {
        if (toggle == true)
        {
            Filters.AddFilter("A");
            Filters.AddFilter("B");
            Filters.AddFilter("C");
            Filters.AddFilter("D");
            Filters.AddFilter("E");
        }
        else
        {
            Filters.ClearFilter();
        }

    }


}
