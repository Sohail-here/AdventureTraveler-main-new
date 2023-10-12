using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayEntry : MonoBehaviour
{
    [SerializeField] BirthdayScrollView myScrollView = default;
    [SerializeField] int minRange; // The minest value.
    [SerializeField] int maxRange; // Count of items.

    void Start()
    {
        var items = Enumerable.Range(minRange, maxRange)
            .Select(i => new BirthdayItemData($"{i}"))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}