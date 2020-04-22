// """Sorting."""

// from enum import Enum
// from typing import Dict, NamedTuple


using System.Collections.Generic;

public static class SortingOrder
{ 
    //"""Sorting orders."""

    public static string ascending = "asc";
    public static string descending = "desc";
}

public class Sorter : NamedTuple
{ 
    //"""Sorter."""

    string field_name;
    SortingOrder order = SortingOrder.ascending;

    public override ToString()
    { 
        //"""Get string represenation.

        //:return: string repr
        //"""
        return $"{field_name}-{order}".format(
            field_name: field_name,
            order: order.value
        );
    }
}

public interface IWithSorting
{ 
    //"""With sorting mixin."""

    IDictionary<string, string> _request_data { get; }
}

public static class IWithSortingExtensions
{ 
    public static IWithSortingExtensions sort_by(this IWithSortingExtensions self, Sorter sorter_obj)
    { 
        //"""Sort by sorter.

        //:param sorter_obj: sorter object
        //:return: self object
        //"""
        var sort_by = self._request_data.get("sort_by");
        if (sort_by == null)
            sort_by = (string)sorter_obj;
        else
            sort_by = $"{sort_by},{new_field}".format(
                sort_by: sort_by,
                new_field: (string)sorter_obj
            );
        self._request_data["sort_by"] = sort_by;
        return self;
    }
}