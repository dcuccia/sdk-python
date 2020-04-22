// """Storage Api namespace."""
// from typing import Any, Dict, Set, Tuple
// from requests import Response
// from ambra_sdk.storage.image import Image
// from ambra_sdk.storage.study import Study


using AmbraSdk.Extensions;

namespace AmbraSdk.Storage
{
    public class Storage : Api
    {
        public const string STORAGE_API_VERSION = "LBL0038 v8.0 2019-07-17";

        // """Storage api namespace."""

        private const string STORAGE_BASE_URL = $@"https://{engine_fqdn}/api/v3/storage";
        
        private Api _base_api;

        public Storage(Api api)
        {    // """Init.

            // :param api: base api
            // """
            _base_api = api;
        }

        public string format_url(string url_template, string[][] kwargs)// -> str:
        {   // """Format url by template.

            // :param url_template: url template
            // :param kwargs: template arguments

            // :return: formated url
            // """
            engine_fqdn = kwargs.pop("engine_fqdn");
            base_url = self.STORAGE_BASE_URL.format(
                engine_fqdn: engine_fqdn
            );
            url = url_template.format(kwargs);
            return $"{base_url}{url}".format(base_url=base_url, url=url);
        }

        public (string, IDictionary<string, object>) get_url_and_request(
            string url_template,
            string[] url_arg_names,
            string[] request_arg_names,
            IDictionary<string, object> args
        )// -> Tuple[str, Dict[str, Any]]:
        {    // """Get url and request arguments from template and locals.

            // :param url_template: url template
            // :param url_arg_names: url arg names for template
            // :param request_arg_names: param arg names
            // :param args: args dict
            // :return: (url, request dict)
            // """
            var url_args = new Dictionary<string, string>(
                args.items().Where(item => url_arg_names.Contains(item.arg_name))
            );
            var url = format_url(url_template, *url_args);
            var request_data = new Dictionary<string, string>(
                args.items().Where(item => request_arg_names.Contains(item.arg_name) && item.arg_value != null);
            );

            return (url, request_data);
        }

        public Response delete(string url, IDictionary<string, string> kwargs)// -> Response:
        {    // """Delete from storage.
            // :param url: url
            // :param kwargs: delete kwargs
            // :return: response obj
            // """
            var response = _base_api.storage_delete(
                url,
                required_sid: true,
                kwargs
            );

            return response;  //// NOQA:WPS331
        }

        public Response get(string url, IDictionary<string, string> kwargs)// -> Response:
        {    // """Get from storage.

            // :param url: url
            // :param kwargs: delete kwargs
            // :return: response obj
            // """
            response: Response = _base_api.storage_get(
                url,
                required_sid: true,
                kwargs: kwargs
            );
            return response; // // NOQA:WPS331
        }

        public Response post(string url, IDictionary<string, string> kwargs)// -> Response:
        {
            // """Post To storage.

            // :param url: url
            // :param kwargs: delete kwargs
            // :return: response obj
            // """
            response: Response = _base_api.storage_post(
                url,
                required_sid: true,
                kwargs : kwargs
            );
            return response;  //// NOQA:WPS331
        }
    }
}