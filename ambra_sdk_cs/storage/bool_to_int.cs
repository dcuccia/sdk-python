// """Help functions."""

// from typing import Optional
namespace AmbraSdk.Storage
{
    public static class ConverterHelpers
    { 
        public static int bool_to_int(bool bool_value)// -> Optional[int]:
        {
            // """Cast bool to int value.

            // :param bool_value: some bool value
            // :return: int represenation
            // """
            return bool_value ? 1 : 0;
        }
    }
}
