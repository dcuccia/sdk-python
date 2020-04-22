// """Filtering."""

// from enum import Enum
// from typing import Dict, List, NamedTuple, Union
using System.Collections.Generic;

namespace AmbraSdk.Service
{
    public static class filter_condition
    {
        // """Filter conditions."""

        public static string equals = "equals";
        public static string equals_or_null = "equals_or_null";
        public static string not_equals = "not_equals";
        public static string not_equals_or_null = "not_equals_or_null";
        public static string like = "like";
        public static string gt = "gt";
        public static string ge = "ge";
        public static string lt = "lt";
        public static string le = "le";
        public static string in_condition = "in";
        public static string in_or_null = "in_or_null";
    }

    public interface IListOrT<T>{ public T Value { get; } }
    public class ListOrT<T> : IListOrT<IList<T>>, IListOrT<T> { }

    public class Filter //(NamedTuple)
    {
        // """Filter rule."""
        public string field_name { get; }
        public FilterCondition condition { get; }
        ListOrT<string> value { get; } // Union[str, List[str]]  //// NOQA:WPS110
    }

    public interface IWithFilter
    {
        // """With Filter mixin."""

        IDictionary<string> _request_data { get; }
    }

    public static class IWithFilterExtensions
    {
        public static IWithFilterExtensions filter_by(this IWithFilterExtensions self, Filter filter_obj)
        {
            // """Filter by filter.

            // :param filter_obj: filter object
            // :return: Self object
            // """
            self._request_data[$"filter.{filter_name}.{filter_condition}".format(
                filter_name: filter_obj.field_name,
                filter_condition: filter_obj.condition.value
            )] = filter_obj.value;

            return self;
        }
    }    
}