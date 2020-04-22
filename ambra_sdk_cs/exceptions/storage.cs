// """Ambra storage exceptions."""

// from ambra_sdk.exceptions.base import AmbraResponseException


namespace AmbraSdk.Exceptions
{
    public class PermissionDenied : AmbraResponseException
    {
        // """Permission denied."""

        public PermissionDenied(string description = null)
            : base(code: 403, description ?? "Access denied. Wrong sid")
        {
            // """Init.

            // :param description: response description
            // """
        }
    }

    public class NotFound : AmbraResponseException
    {
        // """Not found."""

        public NotFound(string description = null)
            : base(code: 404, description ?? "Not found")
        {
            // """Init.

            // :param description: response description
            // """
        }
    }

    public class UnsupportedMediaType : AmbraResponseException
    {
        // """Unsupported media type."""

        public UnsupportedMediaType(string description = null)
            : base(code: 415, description ?? "Video was not found encapsulated in the DICOM file.")
        {
            // """Init.

            // :param description: response description
            // """
        }
    }

    public class PreconditionFailed : AmbraResponseException
    {
        // """Unsupported media type."""

        public PreconditionFailed(string description = null)
            : base(code: 412, description ?? "Precondition failed.")
        {
            // """Init.

            // :param description: response description
            // """
        }
    }
}