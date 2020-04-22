// """Only fields."""

// import json
// from typing import Dict, List, Union

// from ambra_sdk.models.base import FieldDescriptor
using System.Collections.Generic;

namespace AmbraSdk.Service
{
    public interface IOnlyField<T>
    {
        T Value { get; } // string, IDictionary<string, List<string>>, or FieldDescriptor
    }
    public class OnlyFieldString : IOnlyField<string>
    {
        public string Value { get; }
    }
    public class OnlyFieldDictionary : OnlyField<IDictionary<string, IList<string>>>
    {
        public IDictionary<string, IList<string>> Value { get; }
    }
    public class OnlyFieldDescriptor : OnlyField<FieldDescriptor>
    {
        public FieldDescriptor Value { get; }
    }

    public interface IOnlyFields<T>
    {   
        T Value { get; } // can be OnlyField, or IList<OnlyField>
    }

    public class OnlyFieldsField : IOnlyFields<IOnlyField> 
    {
        public IOnlyField Value { get; }
    }

    public class OnlyFieldsList : IOnlyFields<IList<IOnlyField>> 
    {
        public IList<IOnlyField> Value { get; }
    }

    public interface IWithOnly
    {
        // """With only fields mixin."""
        IDictionary<string, IList<IWithOnly>> _request_data { get; private set; }
    }

    public static class IDictionaryExtensions
    {
        public static IEnumerable<(TKey, TValue)> items(this IDictionary<TKey, TValue> dictionary)
        {
            foreach (var kvp in dictionary.KeyValuePairs)
            {
                yield return (kvp.Key, kvp.Value);
            }
        }
    }

    public static class IWithOnlyExtensions
    {
        public static IWithOnly only_top_field(this IWithOnly self, string field)
        {
            // """Request only one top field.

            // :param field: top field name
            // :return: self object
            // """
            if (self._request_data.TryGetValue("fields._top", out var top_fields))
            {    
                top_fields = json.loads(top_fields);
                top_fields.append(field);
            }
            else
            {
                top_fields = new List<string>();
            }
            self._request_data["fields._top"] = json.dumps(top_fields);
            return self;
        }

        public static IWithOnly only_top_fields(this IWithOnly self, IList<string> fields)
        {
            // """Request only some fields on top level.

            // :param fields: list of top fields
            // :return: self object
            // """
            top_fields = self._request_data.get("fields._top");
            if (top_fields != null)
            {
                top_fields = json.loads(top_fields);
                top_fields.extend(fields);
            }
            else
            {
                top_fields = fields;
            }
            self._request_data["fields._top"] = json.dumps(top_fields);
            return self;
        }

        public static IWithOnly only_struct_fields(this IWithOnly self, IDictionary<string, IList<string>> fields)
        {    // """Request only some fields of structs.

            // :param fields: dict of struct fields.
            //                Name of struct: list of fields
            // :return: self object
            // """
            foreach ((var struct_name, var struct_fields) in fields.items())
            {
                struct_name = "fields.{struct_name}".format(
                    struct_name: struct_name
                );
                struct_fields_list = self._request_data.get(struct_name);
                if (struct_fields_list != null)
                {
                    struct_fields_list = json.loads(struct_fields_list);
                    struct_fields_list.extend(struct_fields);
                }
                else
                {
                    struct_fields_list = struct_fields;
                }
                struct_fields_list = sorted(set(struct_fields_list));
                self._request_data[struct_name] = json.dumps(struct_fields_list);
            }

            return self;
        }

        public static IWithOnly only(this IWithOnly self, OnlyFields fields) // // NOQA:WPS231
        {
            // """Request only fields.

            // :Example:

            // >>> Api.Namespace.method.only("field1")
            // >>> Api.Namespace.method.only(["field1", "field2"])
            // >>> Api.Namespace.method.only({"some_struct": ["field1", "field2"]})
            // >>> Api.Namespace.method.only(
            //         [
            //             {"some_struct1": ["field11", "field12"]},
            //             {"some_struct2": ["field21", "field22"]},
            //             "top_field1",
            //             "top_field2",
            //         ]
            //     )
            // >>> Api.Namespace.method.only(Model.field) // Create structed field
            // >>> Api.Namespace.method.only(["field1", Model.field])
            // >>> Api.Namespace.method.only(
            //         [
            //             Model.field,
            //             Model.field2
            //             {"some_struct3": ["field31", "field32"]},
            //             "top_field2",
            //         ]
            //     )

            // :param fields: Some of the OnlyFields variant
            // :return: self object

            // :raises ValueError: Unknown field type
            // """
            var fields_list = fields is IOnlyFields<IList<IOnlyField>>
                ? fields
                : new IOnlyFields<IList<OnlyField>> { Value = new List<IOnlyField> ( fields ) };

            var top_fields = new List<string>();
            var struct_fields = new Dictionary<string, IList<string>>();
            foreach( var field in fields_list)
            {
                if (field is IOnlyField<string> onlyField)
                {    
                    top_fields.append(onlyField);
                }
                else if (field is IOnlyField<FieldDescriptor> descriptor)
                {
                    struct_dict = field.get_only();
                    struct_fields = self._add_struct(struct_fields, struct_dict);
                }
                else if (field is IOnlyField<IDictionary<string, IList<string>>> dictionary) // todo: where is dictionary used?
                {
                    struct_fields = self._add_struct(struct_fields, fields);
                }
                else
                {
                    throw new ValueError();
                }
            }

            if (top_fields != null)
            {
                self.only_top_fields(top_fields);
            }

            if (struct_fields != null)
            {
                self.only_struct_fields(struct_fields);
            }

            return self;
        }

        public static IWithOnly _add_struct(this IWithOnly self, IDictionary<string, IList<IWithOnly>> base_dict, IDictionary<string, IList<IWithOnly>> new_dict)
        {
            foreach ((var struct_name, var new_fields) in new_dict.items())
            {
                fields = base_dict.get(struct_name, new List<IWithOnly>());
                fields.extend(new_fields);
                base_dict[struct_name] = sorted(set(fields));
            }
            return base_dict;
        }
    }
}