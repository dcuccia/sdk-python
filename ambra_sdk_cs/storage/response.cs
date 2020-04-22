// """Storage respone objects."""
// from typing import Any, Dict, Optional, Set
// from requests import Response
// from ambra_sdk.exceptions.storage import (
//     AmbraResponseException,
//     NotFound,
//     PermissionDenied,
// )

using System.Collections.Generic;

namespace AmbraSdk.Storage
{
    public static class ResponseExtensions
    {
        // todo: is there actually a Response type?
        public static Response check_response(
            this Response response,
            List<string> url_arg_names,
            IDictionary<int, object> errors_mapping = null
        ) //-> Response:
        {    // """Check response on errors.
            // :param response: response obj
            // :param url_arg_names: set of arguments in url
            // :param errors_mapping: map of error name and exception
            // :return: response object
            // :raises AmbraResponseException: Unknown exception
            // :raises PermissionDenied: Permission denied
            // :raises NotFound: Url or args is wrong
            // :raises exception: Some ambra storage response exception

            // """
            var status_code = response.status_code;
            if (status_code == 200)
                return response;

            // 202 - Accepted
            if (status_code == 202)
                return response;

            if (errors_mapping?.TryGetValue(status_code, out var exception))
            {
                throw new Exception(exception);
            }
            else if (status_code == 404)
            {
                throw new NotFoundException($"Url or some of {url_arg_names} is wrong");
            }
            else if (status_code == 403)
            {
                throw new AccessDeniedException("Access denied. Wrong sid");
            }
            throw new AmbraResponseException(
                code: status_code,
                description: "Unknown status code"
            );
        }
    }
}