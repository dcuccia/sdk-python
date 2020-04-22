
// from datetime import date, datetime

// from ambra_sdk.models.base import FK, BaseField  // NOQA F401
namespace AmbraSdk.Models
{
    public class StringField : BaseField
    {
        public string validate(object value) => value as string;// // NOQA WPS110
    }
    public class BooleanField : BaseField
    {
        public string validate(object value) => value as bool;// // NOQA WPS110
    }
    public class IntegerField : BaseField
    {
        public string validate(object value) => value as int;// // NOQA WPS110
    }
    public class FloatField : BaseField
    {
        public string validate(object value) => value as float;// // NOQA WPS110
    }
    public class JsonBField : BaseField
    {
        public string validate(object value) => value as byte[];// // NOQA WPS110
    }
    // public class DateField : BaseField
    // {
    //     public string validate(object value) => value as DateTime;// // NOQA WPS110
    // }
    public class DateTimeField : BaseField
    {
        public string validate(object value) => value as DateTime;// // NOQA WPS110
    }
    public class DictionaryField<TKey, TResult> : BaseField
    {
        public string validate(object value) => value as IDictionary<TKey, TResult>;// // NOQA WPS110
    }
}