// """Ambra exceptions."""
using System;

namespace AmbraSdk.Exceptions
{
    public class AmbraException : System.Exception
    {
        // """Base ambra exception."""
    }

    public class AmbraResponseException : AmbraException
    {
        // """Ambra response exception."""

        public AmbraResponseException(int code, string description = null)
            : base($"{code}. {description}".format(
                code: code,
                description: description))
        {
            // """Init.

            // :param code: response code
            // :param description: error description
            // """
            this.code = code;
            this.description = description;
        }
    }
}