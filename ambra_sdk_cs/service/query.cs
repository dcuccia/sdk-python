// """Query objects."""

// from typing import Any, Dict, Optional

// from box import Box

// from ambra_sdk.exceptions.service import PreconditionFailed
// from ambra_sdk.service.filtering import WithFilter
// from ambra_sdk.service.only import WithOnly
// from ambra_sdk.service.response import IterableResponse, check_response
// from ambra_sdk.service.sorting import WithSorting

using System.Collections.Generic;

namespace AmbraSdk.Service
{
    public class Query
    {
        private const int DEFAULT_ROWS_IN_PAGINATION_PAGE = 100;
        // """Simple query."""

        public Query(  //// NOQA: WPS211
            Api api,
            string url,
            IDictionary<string, object> request_data,
            IDictionary<string, PreconditionFailed> errors_mapping,
            bool required_sid = false
        )
        {
            // """Query initialization.

            // :param api: Api instance
            // :param url: query url
            // :param request_data: data for request
            // :param errors_mapping: map of error name and exception
            // :param required_sid: is sid requred for this query
            // """
            _api = api;
            _url = url;
            _request_data = request_data;
            _required_sid = required_sid;
            _errors_mapping = errors_mapping;
        }

        public Box get()// -> Box:
        {
            // """Get response object.

            // :return: response object
            // """
            response = self._api.service_post(
                url: _url,
                required_sid: _required_sid,
                data: _request_data
            );

            response = check_response(response, self._errors_mapping);

            response_json = response.json();

            if (response_json.ContainsKey("status")) // todo: fix call
            {
                response_json.pop("status");
            }

            return Box(response_json);
        }
    }

    public class QueryP : Query
    {
        // """Query with pagination."""

        public QueryP(  //// NOQA: WPS211
            Api api,
            string url,
            IDictionary<string, object> request_data,
            IDictionary<string, PreconditionFailed> errors_mapping,
            string paginated_field,
            bool required_sid = false
        )
        {
            // """Query initialization.

            // :param api: Api instance
            // :param url: query url
            // :param request_data: data for request
            // :param errors_mapping: map of error name and exception
            // :param required_sid: is sid requred for this query
            // :param paginated_field: field for pagination
            // """
            super().__init__(
                api=api,
                url=url,
                request_data=request_data,
                errors_mapping=errors_mapping,
                required_sid=required_sid
            );
            self._paginated_field = paginated_field;
            self._rows_in_page = DEFAULT_ROWS_IN_PAGINATION_PAGE;
        }

        public Query set_rows_in_page(int rows_in_page)
        {
            // """Set number of rows in one page.

            // :param  rows_in_page: number of rows
            // :return: self object
            // :raises ValueError: number of rows should be 0 < n < 5000
            // """
            // // From api.html:
            // //
            // // The default is 100 or 1000 depending on
            // // the object type and the maximum is 5000
            if (rows_in_page > 5000)
                throw new ValueError("Max rows in page is 5000");
            if (rows_in_page < 0)
                throw new ValueError("Negative rows in page");
            _rows_in_page = rows_in_page;
            return this;
        }

        public IterableResponse all()// -> IterableResponse:  // NOQA: A003
        {
            // """Get iterable response.

            // :returns: iterable response object
            // """
            return IterableResponse(
                _api.service_post,
                _url,
                _required_sid,
                _request_data,
                _errors_mapping,
                _paginated_field,
                _rows_in_page
            );
        }

        public Box? first()// -> Optional[Box]:
        {
            // """Get First element of sequence.

            // :returns: Response object
            // """
            return this.all().first();
        }
    }

    public class QueryO : Query, IWithOnly 
    {
    //    """Query with only fields."""
    }

    public class QueryOF : Query, IWithOnly, IWithFilter
    {
            // """Query with filtering."""
    }

    public class QueryOS : Query, IWithSorting
    {
            // """Query with sorting."""
    }

    public class QueryOS : Query, IWithOnly, IWithFilter, IWithSorting
    {
        // """Query with filtering and sorting."""
    }

    public class QueryOP : QueryP, IWithOnly
    {
        // """Query with pagination and only fields."""
    }

    public class QueryOPF : QueryP, IWithOnly, IWithFilter
    {
        // """Query with pagination and filtering."""
    }

    public class QueryOPS : QueryP, IWithOnly, IWithSorting
    {
        // """Query with pagination and sorting."""
    }

    public class QueryOPSF : QueryP, IWithOnly, IWithSorting, IWithFilter
    {
        // """Query with pagination sorting and filtering."""
    }

    public static string get_query_cls_name(
        bool with_pagination,
        bool with_filtering,
        bool with_sorting
    )// -> str:
    {
        // """Get query class name.

        // :param with_pagination: have pagination
        // :param with_filtering: have filtering
        // :param with_sorting: have sorting
        // :return: class name
        // """
        var cls_name = "QueryO";
        if (with_pagination)
            cls_name += "P";  //// NOQA: WPS336
        if (with_sorting)
            cls_name += "S";  //// NOQA: WPS336
        if (with_filtering)
            cls_name += "F";  //// NOQA: WPS336
        return cls_name;
    }
}