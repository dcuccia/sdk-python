// """Ambra storage and service API."""

using System.Collections.Generic;

namespace AmbraSdk
{
    public class Credentials
    {  
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        string Username { get; }
        string Password { get; }
    }

    public static class ApiExtensions
    {
        public static Api with_sid(
            this Api cls,
            string url,
            string sid
        ) //-> "Api":
        {
            // """Create Api with sid.

            // :param url: api url
            // :param sid: session id

            // :return: Api
            // """

            cls.url = url;
            cls.sid = sid;
            return cls;
        }

        public static Api with_creds(
            this Api cls,
            string url,
            string username,
            string password
        ) //-> "Api":
        {
            // """Create Api with (username, password) credentials.

            // :param url: api url
            // :param username: username credential
            // :param password: password credential

            // :return: Api
            // """
            cls.url = url;
            cls.username = username;
            cls.password = password;

            return cls;
        }
    }

    public class Api // // NOQA:WPS214,WPS230
    {    
        public const string __version__ = "3.20.3.0-3";

        // from footer in api.html
        public const string API_VERSION = "LBL0022 v37.0 2020-04-15";

        // equal for API_VERSION based on dictionary.html
        public const string MODELS_VERSION = "LBL0022 v37.0 2020-04-15";

        // from storage_api.html
        public const string STORAGE_VERSION = "LBL0038 v8.0 2019-07-17";
        
        public static ILogger logger = logging.getLogger(nameof(Api));
        // """Ambra API."""

        public Api(  //// NOQA:WPS211
            string url,
            string username = null,
            string password = null,
            string sid = null
        )
        {   // """Init api.

            // :param url: api url
            // :param sid: session id
            // :param username: username credential
            // :param password: password credential
            // """
            _api_url = url; // string
            _creds = null; // Optional[Credentials]
            _sid = sid; // Optional[str]
            if (username != null && password != null)
            {
                _creds = Credentials(username=username, password=password);
            }
            _service_session = null; // Optional[requests.Session]
            _storage_session = null; // Optional[requests.Session]

            // _init_request_params();
            // https://urllib3.readthedocs.io/en/latest/reference/urllib3.util.html//module-urllib3.util.retry
            self.service_retry_params = new Dictionary<string, object>{
                {"total", 10},
                {"connect", 5},
                {"read", 5},
                {"status", 5},
                {"status_forcelist", new[]{ 500, 502, 503, 504}},
                {"backoff_factor", 0.1},
            };

            // Merge studies
            // /study/{namespace}/{studyUid}/merge?sid={sid}&secondary_study_uid={secondary_study_uid}&delete_secondary_study={0,1}
            // Can return 500 and its ok .....
            self.storage_retry_params = new Dictionary<string, object>{
                {"total", 10},
                {"connect", 5},
                {"read", 5},
                {"status", 5},
                {"status_forcelist", new[]{ 500, 502, 503, 504}},
                {"backoff_factor", 0.1},
            };
        }
        public requests.Session service_session//(self) -> requests.Session:
        {
            get
            {
                // """Service session.

                // :return: service session
                // """
                if (self._service_session == null)
                {
                    self._service_session = requests.Session();
                    retries = Retry(self.service_retry_params);
                    adapter = HTTPAdapter(max_retries: retries);
                    self._service_session.mount("http://", adapter);
                    self._service_session.mount("https://", adapter);
                }
                return self._service_session;
            }
        }
        private requests.Session _storage_session;
        public requests.Session storage_session
        { 
            get//-> requests.Session:
            {
                // """Storage session.

                // :return: storage session
                // """
                if (_storage_session == null)
                {
                    _storage_session = requests.Session();
                    var retries = Retry(**self.storage_retry_params);
                    var adapter = HTTPAdapter(max_retries=retries);
                    _storage_session.mount("http://", adapter);
                    _storage_session.mount("https://", adapter);
                }
                return _storage_session;
            }
        }
        public requests.Response storage_get(
            string url,
            bool required_sid,
            IDictionary<string, object> kwargs
        ) //-> requests.Response:
        { 
            // """Get from storage.

            // :param url: url
            // :param required_sid: is this method required sid
            // :param kwargs: request arguments
            // :return: response obj
            // """
            if (required_sid != null)
            {
                // Sid passed always in url params (?sid=...)
                request_params = kwargs.pop("params");
                // Get or create new sid
                request_params["sid"] = self.sid;
                kwargs["params"] = request_params;

                try
                {
                    return self.storage_session.get(
                        url: url,
                        kwargs
                    );
                }
                catch(PermissionDenied pd)
                {
                    logger.info("Try update sid");
                    request_params["sid"] = self.get_new_sid();
                    kwargs["params"] = request_params;
                }
            }

            return self.storage_session.get(url: url, kwargs);
        }
        public requests.Response storage_delete(
            string url,
            bool required_sid,
            IDictionary<string, object> kwargs
        )// -> requests.Response:
        {
            // """Delete from storage.

            // :param url: url
            // :param required_sid: is this method required sid
            // :param kwargs: request arguments
            // :return: response obj
            // """
            if (required_sid != null)
            {
                // Sid passed always in url params (?sid=...)
                request_params = kwargs.pop("params");
                // Delete or create new sid
                request_params["sid"] = self.sid;
                kwargs["params"] = request_params;

                try
                {
                    return self.storage_session.delete(
                        url: url,
                        kwargs
                    );
                }
                catch(PermissionDenied pd)
                {
                    logger.info("Try update sid");
                    request_params["sid"] = self.get_new_sid();
                    kwargs["params"] = request_params;
                }
            }

            return self.storage_session.delete(url: url, kwargs);
        }
        public requests.Response storage_post(
            string url,
            bool required_sid,
            IDictionary<string, object> kwargs
        ) //-> requests.Response:
        {
            // """Post to storage.

            // :param url: url
            // :param required_sid: is this method required sid
            // :param kwargs: request arguments
            // :return: response obj
            // """
            if (required_sid != null)
            {
                // Sid passed always in url params (?sid=...)
                request_params = kwargs.pop("params");
                // Post or create new sid
                request_params["sid"] = self.sid;
                kwargs["params"] = request_params;

                try
                {
                    return self.storage_session.post(
                        url: url,
                        kwargs
                    );
                }
                catch (PermissionDenied pd)
                {
                    logger.info("Try update sid");
                    request_params["sid"] = self.get_new_sid();
                    kwargs["params"] = request_params;
                }
            }

            return self.storage_session.post(url: url, kwargs);
        }
        public requests.Response service_post(
            string url,
            bool required_sid,
            IDictionary<string, object> kwargs
        ) // -> requests.Response:
        {
            // """Post data to url.

            // :param url: method url
            // :param required_sid: is this method required sid
            // :param kwargs: request arguments
            // :return: response
            // """
            var url2 = $"{base_url}{entrypoint_url}".format(
                base_url: self._api_url,
                entrypoint_url: url
            );
            if (required_sid)
            {
                request_data = kwargs.pop("data");
                request_data["sid"] = self.sid;
                kwargs["data"] = request_data;
                try
                {
                    return self.service_session.post(url: url2, kwargs);
                }
                catch(InvalidCredentials ic) 
                {
                    logger.info("Try update sid");
                    request_data["sid"] = get_new_sid();
                    kwargs["data"] = request_data;
                }
            }
            return self.service_session.post(url: url2, kwargs);
        }
        public string sid// -> str:
        {
            get
            {
                // """Get or create new sid property.

                // :return: sid
                // """
                if (_sid == null)
                    return get_new_sid();
                return _sid;
            }
        }
        public void logout()
        {
            // """Logout."""
            Session.logout();
            _sid = null;
        }
        public string get_new_sid//(self) -> str:
        {
            get
            {            
                // """Get new sid.

                // :raises RuntimeError: Missined credentials
                // :return: sid
                // """
                if (_creds == null)
                    throw new RuntimeError("Missed credentials");
                var new_sid = self.Session.get_sid(
                    self._creds.username,
                    self._creds.password
                ); // -> str
                _sid = new_sid;
                return new_sid;
            }
        }

        // private _init_request_params()
        // {
        //     // https://urllib3.readthedocs.io/en/latest/reference/urllib3.util.html//module-urllib3.util.retry
        //     self.service_retry_params = new Dictionary<string, object>{
        //         {"total", 10},
        //         {"connect", 5},
        //         {"read", 5},
        //         {"status", 5},
        //         {"status_forcelist", new[]{ 500, 502, 503, 504}},
        //         {"backoff_factor", 0.1},
        //     };
        //     // Merge studies
        //     // /study/{namespace}/{studyUid}/merge?sid={sid}&secondary_study_uid={secondary_study_uid}&delete_secondary_study={0,1}
        //     // Can return 500 and its ok .....
        //     self.storage_retry_params = new Dictionary<string, object>{
        //         {"total", 10},
        //         {"connect", 5},
        //         {"read", 5},
        //         {"status", 5},
        //         {"status_forcelist", new[]{ 500, 502, 503, 504}},
        //         {"backoff_factor", 0.1},
        //     };
        // }
    }
}