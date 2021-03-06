""" Analytics.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import InvalidCount
from ambra_sdk.exceptions.service import InvalidEndDate
from ambra_sdk.exceptions.service import InvalidParameters
from ambra_sdk.exceptions.service import InvalidPeriod
from ambra_sdk.exceptions.service import MissingFields
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.exceptions.service import NotPermitted
from ambra_sdk.service.query import QueryO

class Analytics:
    """Analytics."""

    def __init__(self, api):
        self._api = api

    
    def study(
        self,
        count,
        account_id=None,
        end_date=None,
        namespace_id=None,
        period=None,
    ):
        """Study.
        :param count: The number of periods to get
        :param account_id: account_id
        :param end_date: The end date, default is today if not passed (optional)
        :param namespace_id: namespace_id
        :param period: period

        Notes:
        period - The time period (day OR week OR month OR year)
        (account_id OR namespace_id) - The account or namespace to get the analytics for
        """
        request_data = {
           "period": period,
           "count": count,
           "end_date": end_date,
           "account_id": account_id,
           "namespace_id": namespace_id,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_COUNT"] = InvalidCount("Invalid or excessive count value")
        errors_mapping["INVALID_END_DATE"] = InvalidEndDate("An invalid period")
        errors_mapping["INVALID_PARAMETERS"] = InvalidParameters("Only pass a account_id or namespace_id")
        errors_mapping["INVALID_PERIOD"] = InvalidPeriod("An invalid period")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The account or namespace can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view analytics for this account or namespace")
        query_data = {
            "api": self._api,
            "url": "/analytics/study",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def patient_portal(
        self,
        account_id,
        count,
        end_date=None,
        patient_id=None,
        period=None,
    ):
        """Patient portal.
        :param account_id: The account id
        :param count: The number of periods to get
        :param end_date: The end date, default is today if not passed (optional)
        :param patient_id: Patient filter (optional)
        :param period: period

        Notes:
        period - The time period (day OR week OR month OR year)
        """
        request_data = {
           "patient_id": patient_id,
           "period": period,
           "count": count,
           "end_date": end_date,
           "account_id": account_id,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_COUNT"] = InvalidCount("Invalid or excessive count value")
        errors_mapping["INVALID_END_DATE"] = InvalidEndDate("An invalid period")
        errors_mapping["INVALID_PERIOD"] = InvalidPeriod("An invalid period")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The account or patient can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view analytics for this account or namespace")
        query_data = {
            "api": self._api,
            "url": "/analytics/patient/portal",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def radreport(
        self,
        account_id,
        count,
        end_date=None,
        namespace_id=None,
        period=None,
        user_id=None,
    ):
        """Radreport.
        :param account_id: The account id
        :param count: The number of periods to get
        :param end_date: The end date, default is today if not passed (optional)
        :param namespace_id: Namespace filter (optional)
        :param period: period
        :param user_id: User filter (optional)

        Notes:
        period - The time period (day OR week OR month OR year)
        """
        request_data = {
           "period": period,
           "count": count,
           "end_date": end_date,
           "user_id": user_id,
           "account_id": account_id,
           "namespace_id": namespace_id,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_COUNT"] = InvalidCount("Invalid or excessive count value")
        errors_mapping["INVALID_END_DATE"] = InvalidEndDate("An invalid period")
        errors_mapping["INVALID_PERIOD"] = InvalidPeriod("An invalid period")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The account or patient can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view analytics for this account or namespace")
        query_data = {
            "api": self._api,
            "url": "/analytics/radreport",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    