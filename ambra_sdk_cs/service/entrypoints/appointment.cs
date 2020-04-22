""" Appointment.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import FilterNotFound
from ambra_sdk.exceptions.service import InvalidCondition
from ambra_sdk.exceptions.service import InvalidCustomfield
from ambra_sdk.exceptions.service import InvalidDateTime
from ambra_sdk.exceptions.service import InvalidField
from ambra_sdk.exceptions.service import InvalidRange
from ambra_sdk.exceptions.service import InvalidSortField
from ambra_sdk.exceptions.service import InvalidSortOrder
from ambra_sdk.exceptions.service import MissingFields
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.exceptions.service import NotPermitted
from ambra_sdk.service.query import QueryO
from ambra_sdk.service.query import QueryOPSF

class Appointment:
    """Appointment."""

    def __init__(self, api):
        self._api = api

    
    def add(
        self,
        account_id,
        end_time,
        patient_id,
        start_time,
        customfield_param=None,
        description=None,
        user_id=None,
    ):
        """Add.
        :param account_id: uuid of the account to add them to
        :param end_time: End date and time of the appointment
        :param patient_id: Id of the patient to create the appointment for
        :param start_time: Start date and time of the appointment
        :param customfield_param: Custom field(s) (optional)
        :param description: Description of the appointment (optional)
        :param user_id: Id of the user to create the appointment for (optional defaults to current user)
        """
        request_data = {
           "patient_id": patient_id,
           "description": description,
           "start_time": start_time,
           "user_id": user_id,
           "end_time": end_time,
           "account_id": account_id,
        }
        if customfield_param is not None:
            customfield_param_dict = {"{prefix}{k}".format(prefix="customfield-", k=k): v for k,v in customfield_param.items()}
            request_data.update(customfield_param_dict)
	
        errors_mapping = {}
        errors_mapping["INVALID_CUSTOMFIELD"] = InvalidCustomfield("Invalid custom field(s) name or value were passed. The error_subtype holds an array of the error details")
        errors_mapping["INVALID_DATE_TIME"] = InvalidDateTime("The timestamp is invalid")
        errors_mapping["INVALID_RANGE"] = InvalidRange("An invalid time range was specified")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The object was not found. The error_subtype holds the type of object not found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to add a appointment to the account")
        query_data = {
            "api": self._api,
            "url": "/appointment/add",
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
        :param uuid: The appointment uuid
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The appointment can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view this appointment")
        query_data = {
            "api": self._api,
            "url": "/appointment/get",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def set(
        self,
        customfields,
        description,
        end_time,
        start_time,
        user_id,
        uuid,
    ):
        """Set.
        :param customfields: An array of the custom fields associated with this appointment. Each object has the following fields (This is only returned if the group has custom fields)
        :param description: Description of the appointment
        :param end_time: End date and time of the appointment
        :param start_time: Start date and time of the appointment
        :param user_id: Id of the user
        :param uuid: The appointment uuid
        """
        request_data = {
           "uuid": uuid,
           "description": description,
           "customfields": customfields,
           "start_time": start_time,
           "user_id": user_id,
           "end_time": end_time,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_CUSTOMFIELD"] = InvalidCustomfield("Invalid custom field(s) name or value were passed. The error_subtype holds an array of the error details")
        errors_mapping["INVALID_DATE_TIME"] = InvalidDateTime("The timestamp is invalid")
        errors_mapping["INVALID_RANGE"] = InvalidRange("An invalid time range was specified")
        errors_mapping["NOT_FOUND"] = NotFound("The appointment can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to edit the appointment")
        query_data = {
            "api": self._api,
            "url": "/appointment/set",
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
        :param uuid: The appointment uuid
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The appointment can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to delete the appointment")
        query_data = {
            "api": self._api,
            "url": "/appointment/delete",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def list(
        self,
        account_id,
    ):
        """List.
        :param account_id: uuid of the account
        """
        request_data = {
           "account_id": account_id,
        }
	
        errors_mapping = {}
        errors_mapping["FILTER_NOT_FOUND"] = FilterNotFound("The filter can not be found. The error_subtype will hold the filter UUID")
        errors_mapping["INVALID_CONDITION"] = InvalidCondition("The condition is not support. The error_subtype will hold the filter expression this applies to")
        errors_mapping["INVALID_FIELD"] = InvalidField("The field is not valid for this object. The error_subtype will hold the filter expression this applies to")
        errors_mapping["INVALID_SORT_FIELD"] = InvalidSortField("The field is not valid for this object. The error_subtype will hold the field name this applies to")
        errors_mapping["INVALID_SORT_ORDER"] = InvalidSortOrder("The sort order for the field is invalid. The error_subtype will hold the field name this applies to")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The account can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view appointments in this account")
        query_data = {
            "api": self._api,
            "url": "/appointment/list",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        query_data["paginated_field"] = "appointments"
        return QueryOPSF(**query_data)
    