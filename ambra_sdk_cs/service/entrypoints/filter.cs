""" Filter.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import FilterNotFound
from ambra_sdk.exceptions.service import InvalidCondition
from ambra_sdk.exceptions.service import InvalidField
from ambra_sdk.exceptions.service import InvalidParameters
from ambra_sdk.exceptions.service import InvalidSortField
from ambra_sdk.exceptions.service import InvalidSortOrder
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.exceptions.service import NotPermitted
from ambra_sdk.service.query import QueryO
from ambra_sdk.service.query import QueryOPSF

class Filter:
    """Filter."""

    def __init__(self, api):
        self._api = api

    
    def list(
        self,
        account_id,
        type,
    ):
        """List.
        :param account_id: Limit to global filters and filters within the account namespaces
        :param type: The type of filter to list
        """
        request_data = {
           "account_id": account_id,
           "type": type,
        }
	
        errors_mapping = {}
        errors_mapping["FILTER_NOT_FOUND"] = FilterNotFound("The filter can not be found. The error_subtype will hold the filter UUID")
        errors_mapping["INVALID_CONDITION"] = InvalidCondition("The condition is not support. The error_subtype will hold the filter expression this applies to")
        errors_mapping["INVALID_FIELD"] = InvalidField("The field is not valid for this object. The error_subtype will hold the filter expression this applies to")
        errors_mapping["INVALID_SORT_FIELD"] = InvalidSortField("The field is not valid for this object. The error_subtype will hold the field name this applies to")
        errors_mapping["INVALID_SORT_ORDER"] = InvalidSortOrder("The sort order for the field is invalid. The error_subtype will hold the field name this applies to")
        query_data = {
            "api": self._api,
            "url": "/filter/list",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        query_data["paginated_field"] = "filters"
        return QueryOPSF(**query_data)
    
    def add(
        self,
        configuration,
        name,
        type,
    ):
        """Add.
        :param configuration: The configuration as a JSON data structure
        :param name: The name of the filter
        :param type: The type of the filter
        """
        request_data = {
           "type": type,
           "configuration": configuration,
           "name": name,
        }
	
        errors_mapping = {}
        query_data = {
            "api": self._api,
            "url": "/filter/add",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def get(
        self,
        uuid,
    ):
        """Get.
        :param uuid: The filter uuid
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The filter can not be found")
        query_data = {
            "api": self._api,
            "url": "/filter/get",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def set(
        self,
        uuid,
        configuration=None,
        name=None,
        type=None,
    ):
        """Set.
        :param uuid: The filter uuid
        :param configuration: The configuration as a JSON data structure (optional)
        :param name: The name of the filter (optional)
        :param type: The type of the filter (optional)
        """
        request_data = {
           "uuid": uuid,
           "configuration": configuration,
           "name": name,
           "type": type,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The filter can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not the owner of the filter")
        query_data = {
            "api": self._api,
            "url": "/filter/set",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def delete(
        self,
        uuid,
    ):
        """Delete.
        :param uuid: The filter uuid
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The filter can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not the owner of the filter")
        query_data = {
            "api": self._api,
            "url": "/filter/delete",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def share(
        self,
        uuid,
        account_id=None,
        group_id=None,
        location_id=None,
        user_id=None,
    ):
        """Share.
        :param uuid: The filter uuid
        :param account_id: account_id
        :param group_id: group_id
        :param location_id: location_id
        :param user_id: user_id

        Notes:
        (account_id OR location_id OR group_id OR user_id) - uuid of the account, location, group or user to share this filter with
        """
        request_data = {
           "group_id": group_id,
           "uuid": uuid,
           "location_id": location_id,
           "user_id": user_id,
           "account_id": account_id,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_PARAMETERS"] = InvalidParameters("Only pass a account_id or a location_id or a group_id or a user_id")
        errors_mapping["NOT_FOUND"] = NotFound("The filter or share object can not be found. The error_subtype holds a the name of the key that can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not the owner of the filter or are not permitted to share a filter with the destination")
        query_data = {
            "api": self._api,
            "url": "/filter/share",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def share_stop(
        self,
        uuid,
        account_id=None,
        group_id=None,
        location_id=None,
        user_id=None,
    ):
        """Share stop.
        :param uuid: The filter uuid
        :param account_id: account_id
        :param group_id: group_id
        :param location_id: location_id
        :param user_id: user_id

        Notes:
        (account_id OR location_id OR group_id OR user_id) - uuid of the account, location, group or user to stop sharing this filter with
        """
        request_data = {
           "group_id": group_id,
           "uuid": uuid,
           "location_id": location_id,
           "user_id": user_id,
           "account_id": account_id,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The filter can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not the owner of the filter")
        query_data = {
            "api": self._api,
            "url": "/filter/share/stop",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def share_list(
        self,
        uuid,
    ):
        """Share list.
        :param uuid: The filter uuid
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The filter can not be found")
        query_data = {
            "api": self._api,
            "url": "/filter/share/list",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    