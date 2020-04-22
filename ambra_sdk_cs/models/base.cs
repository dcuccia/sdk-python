// """Base model and field."""

// import json
// from abc import ABC, abstractmethod
// from collections.abc import Iterable
// from copy import copy
// from functools import partial
// from importlib import import_module
// from typing import Any, Dict, List, Optional, Type

// from ambra_sdk.service.filtering import Filter, FilterCondition
// from ambra_sdk.service.sorting import Sorter, SortingOrder

namespace AmbraSdk.Models
{
    public class BaseField : ABC
    {
        // """Base Field."""

        public object _python_type = null;

        public BaseField(string description)
        {
            // """Init.

            // :param description: field description
            // """
            _description = description;
        }

        public abstract void validate(object value);
            // """Validate value.

            // :param value: value for validation
            // """

        public FieldDescriptor _get_descriptor()
        {
            return new FieldDescriptor(
                field=this
            );
        }
    }

    public interface IWithSorting
    {
        // """With sorting field mixin."""

        string _name { get; }
        string _full_name { get; }
    }

    public static class IWithSortingExtensions
    {
        public Sorter desc(this IWithSorting self, bool use_full_name = false)// -> Sorter:
        {
            // """Desc sorting.

            // :param full_name: use full name
            // :returns: sorter
            // """
            field_name = use_full_name ? _full_name : _name;
            return new Sorter(
                field_name: field_name,
                order: SortingOrder.descending
            );
        }

        public Sorter asc(this IWithSorting self, bool use_full_name = false)// -> Sorter:
        {
            // """Asc sorting.

            // :param full_name: use full name
            // :returns: sorter
            // """
            field_name = use_full_name ? _full_name : _name;
            return new Sorter(
                field_name: field_name,
                order: SortingOrder.ascending
            );
        }
    }

    public interface IWithFiltering //:  // NOQA:WPS214
    {
        // """With filtering field mixin."""

        string _name { get; }
        string _full_name { get; }
        BaseField _field { get; }
    }

    public static class IWithFilteringExtensions
    {
        public static Filter like(this IWithFilteringExtensions self, string value, bool use_full_name = true)
        {
            // """Get like filter.

            // :param value: filtering value
            // :param full_name: use full name for filtering
            // :throw news ValueError: value is not string
            // :return: Filter
            // """
            if (_field._python_type != str)
                throw new ValueError("Use like for not string field");

            field_name = use_full_name ? _full_name : _name;
            return new Filter(
                field_name: field_name,
                condition: FilterCondition.like,
                value: value
            );
        }

        //def __getattr__(self, attribute)
        public static object this[string key] 
        {
            get
            {
                // """Get attr.

                // Redefined for automatic pick filter.

                // :param attribute: attr
                // :return: filtering function

                // :throw news AttributeError: Unknown attribute
                // """
                var conditions = FilterCondition.__members__;  // NOQA:WPS609
                var standart_filters = new[]
                {
                    "equals",
                    "equals_or_null",
                    "not_equals",
                    "not_equals_or_null",
                    "gt",
                    "ge",
                    "lt",
                    "le",
                };
                var filters_with_seq = new[]
                {
                    "in_condition",
                    "in_or_null",
                };
                var condition = conditions.get(attribute);
                if (condition == null)
                    throw new AttributeError();
                if (standart_filters.Contains(attribute))
                    return partial(self._standart_filter, condition: condition);
                else if (filters_with_seq.Contains(attribute))
                    return partial(self._filter_with_seq, condition: condition);
                throw new AttributeError();
            }
        }

        public static bool operator ==(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return value1.Equals(value2);  // todo: how to overload? what"s needed?
        }
        public static bool operator !=(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return !value1.Equals(value2);  // todo: how to overload? what"s needed?
        }
        public static bool operator >(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return value1 > value2;  // todo: how to overload? what"s needed?
        }
        public static bool operator >=(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return value1 >= value2;  // todo: how to overload? what"s needed?
        }
        public static bool operator<(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return value1 < value2;  // todo: how to overload? what"s needed?
        }
        public static bool operator <=(IWithFilteringExtensions value1, IWithFilteringExtensions value2)  // NOQA:D105
        {    
            return value1 <= value2;  // todo: how to overload? what"s needed?
        }

        public static bool _standart_filter(
            this IWithFilteringExtensions self,
            string value,
            string condition,
            bool use_full_name = false
        )
        {
            var value = _field.validate(value);
            var field_name = use_full_name ? _full_name : _name;
            return new Filter(
                field_name: field_name,
                condition: condition,
                value: value
            );
        }

        public static Filter _filter_with_seq(
            this IWithFilteringExtensions self, 
            string[] values, 
            string condition, 
            bool use_full_name = false)
        {
            field_name = use_full_name ? _full_name : _name;
            return new Filter(
                field_name: field_name,
                condition: condition,
                value: json.dumps(values)
            );
        }
    }

    public interface IWithOnly
    {
        // """With only field mixin."""

        string _name { get; }
        string _lower_parent_name { get; }
    }

    public static class IWithOnlyExtensions
    {
        public static IDictioary<string, IList<string>> get_only(this IWithOnlyExtensions self)// -> Dict[str, List[str]]:
        {
            // """Get dict for only method.

            // :return: only dict
            // """
            return new Dictionary<string, IList<string>> 
            {
                { _lower_parent_name, new[]{ _name } }
            };
        }
    }

    public class FieldDescriptor : IWithSorting, IWithFiltering, IWithOnly  // NOQA:WPS214
    {
        // """Field descriptor."""

        public FieldDescriptor(string field)
        {
            // """Init.

            // :param field: field
            // """
            _field = field;

            // This is init in model __new__
            _parent = null;
            _name = null;

            __doc__ = "{field_type}({description})".format(
                field_type: field.__class__.__name__,
                description: field._description
            );
        }

        public static IList<FieldDescriptor> parents() // todo: what"s the purpose of this? is it correct?
        {
            // """Get parents.

            // :return: List of parent
            // """
            var parent_list = new List<FieldDescriptor>();
            var parent = _parent;
            while (true)
            {
                if (parent != null)
                    parent_list.append(parent);
                else
                    break;
                parent = parent._parent;
            }
            return parent_list;
        }

        // todo: why an accessor for both a class and for an instance?
        // who calls this? are the types correct? (probably not...)
        public static FieldDescriptor GetFieldDescriptorOrFieldValue(FieldDescriptor self, FieldDescriptor instance, FieldDescriptor owner) 
        {
            // """Get from instance.

            // :param instance: object
            // :param owner: owner

            // :return: field descriptor or field value (if instance exist)
            // """
            if (instance == null)
                return this;

            return instance.__dict__[self.name];  // NOQA:WPS609
        }

        public static void SetFieldDescriptorOrFieldValue(FieldDescriptor self, FieldDescriptor instance, FieldDescriptor value)
        {
            // """Set to instance.

            // :param instance: instance
            // :param value: value
            // """
            if (value != null)
                value = _field.validate(value);
            instance.__dict__[self.name] = value;  // NOQA:WPS609
        }

        public static void SetName(FieldDescriptor self, FieldDescriptor owner, string name)
        {
            // """Set name.

            // :param owner: owner
            // :param name: name
            // """
            self.name = name;
        }

        public string _full_name
        {
            get 
            {
                parents = self.parents();
                path = ".".join(reversed(parents.Select(parents => parent._name).ToArray()));
                return $"{path}.{name}".format(path: path, name: _name);
            }
        }

        public string _parent_name
        {
            get
            {
                return _parent._name;
            }
        }

        
        public string _lower_parent_name
        { 
            get
            {
                return _parent_name.lower();
            }
        }
    }

    public class ModelDescriptor
    {
        // """Model descriptor."""

        public ModelDescriptor(string model_name, string field_name)
        {
            // """Init.

            // :param model_name: model name
            // :param field_name: filed name of this descriptor
            // """
            _model_name = model_name;
            _field_name = field_name;

            _from_model = null;
            _model = null;

            _parent = null;
        }

        public ModelDescriptor from_model() // todo: type correct??
        {
            // """Get base model for this descriptor.

            // Lazy import model from models.generated.

            // :return: from model
            // """
            if (_from_model == null)
            {
                generated_models = import_module("ambra_sdk.models.generated");
                _from_model = getattr(generated_models, _model_name);
            }

            return _from_model;
        }

        
        public ModelDescriptor model // todo: type correct??
        {
            get
            {
                //  """Get model for this descriptor.

                // Lazy create new model type

                // :return: model
                // """
                if (_model == null)
                    new_model = type(
                        from_model.__name__,  // NOQA:WPS609
                        from_model.__bases__,  // NOQA:WPS609
                        dict(from_model.__dict__)  // NOQA:WPS609
                    );
                    new_model._name = _field_name;
                    new_model._parent = _parent;
                    _model = new_model;
                return _model;
            }
        }
           
        public object GetFromInstance(object instance) // todo: input and return types?
        {
            // """Get from instance.

            // :param instance: object
            // :param owner: owner

            // :return: model or field value (if instance exist)
            // """
            if (instance != null)
                return self.model;
            return instance.__dict__[self.name];  // NOQA:WPS609
        }

        public void SetToInstance(object instance, object value) // todo: input types?
        {
            // """Set to instance.

            // :param instance: instance
            // :param value: value

            // :throw news ValueError: Wronk type of value
            // """
            if (value != null && !isinstance(value, self.from_model))
                throw new ValueError();
            instance.__dict__[self.name] = value;  // NOQA:WPS609
        }

        public void SetName(string name)
        {
            // """Set name.

            // :param owner: owner
            // :param name: name
            // """
            self.name = name;
        }
    }

    public class FK : BaseField
    {
        // """Foreign key field."""

        public FK(Model model, string[] args, IDictionary<string, object> kwargs) // todo: model type?
            : base(args, kwargs)
        {
            // """Init.

            // :param model: Foreign model
            // :param args: args
            // :param kwargs: kwargs
            // """
            this.model = model;
        }

        public void validate(object value)
        {
            // """Validate.

            // :param value: value
            // :throw news RuntimeError: Dont use validate for this type of fields
            // """
            throw new RuntimeError("FK validated in descriptor");
        }
    }

    public abstract class BaseMetaModel
    {
        // """Ambra base meta model."""

        public BaseMetaModel(object cls, string name, object bases, object attrs) // todo: types?  // NOQA:D102
        {
            var children = new List<IDescriptor>();
            foreach ( (attr_name, attr) in attrs.items())
            {
                if (attr is FK)
                {
                    // Need copy class for set parent
                    model_descriptor = ModelDescriptor(
                        model_name: attr.model,
                        field_name: attr_name
                    );
                    attrs[attr_name] = model_descriptor;
                    children.append(model_descriptor);
                }
                else if (attr is FieldDescriptor)
                {
                    // Need copy descriptor for set parent
                    descriptor = copy(attr);
                    attrs[attr_name] = descriptor;
                    children.append(descriptor);
                }
                else if (attr is BaseField)
                {
                    descriptor = attr._get_descriptor();
                    descriptor._name = attr_name;
                    attrs[attr_name] = descriptor;
                    children.append(descriptor);
                }
            }
            attrs["_name"] = name;
            instance = super().__new__(cls, name, bases, attrs);
            foreach(var child in children)
            {
                child._parent = instance;
            }
            return instance;
        }
    }

    public class BaseModel : BaseMetaModel
    {
        //    """Ambra base model."""

        object _parent = null;
    }
}