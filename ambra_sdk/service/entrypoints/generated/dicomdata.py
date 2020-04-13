""" Dicomdata.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import FilterNotFound
from ambra_sdk.exceptions.service import InvalidCondition
from ambra_sdk.exceptions.service import InvalidField
from ambra_sdk.exceptions.service import MissingFields
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.exceptions.service import NotPermitted
from ambra_sdk.service.query import QueryO
from ambra_sdk.service.query import QueryOPF

class Dicomdata:
    """Dicomdata."""

    def __init__(self, api):
        self._api = api

    
    def list(
        self,
        dicom_tags=None,
        namespace_id=None,
        study_id=None,
    ):
        """List.
        :param dicom_tags: A JSON list of the DICOM tags to return (optional)
        :param namespace_id: namespace_id
        :param study_id: study_id

        Notes:
        (study_id OR namespace_id) - uuid of the study or namespace to search
        """
        request_data = {
           'study_id': study_id,
           'dicom_tags': dicom_tags,
           'namespace_id': namespace_id,
        }
	
        errors_mapping = {}
        errors_mapping['FILTER_NOT_FOUND'] = FilterNotFound('The filter can not be found. The error_subtype will hold the filter UUID')
        errors_mapping['INVALID_CONDITION'] = InvalidCondition('The condition is not support. The error_subtype will hold the filter expression this applies to')
        errors_mapping['INVALID_FIELD'] = InvalidField('The field is not valid for this object. The error_subtype will hold the filter expression this applies to')
        errors_mapping['MISSING_FIELDS'] = MissingFields('A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields')
        errors_mapping['NOT_PERMITTED'] = NotPermitted('You are not permitted to view the DICOM data')
        query_data = {
            'api': self._api,
            'url': '/dicomdata/list',
            'request_data': request_data,
            'errors_mapping': errors_mapping,
            'required_sid': True,
        }
        query_data['paginated_field'] = 'dicomdatas'
        return QueryOPF(**query_data)
    
    def get(
        self,
        customfields,
        uuid,
        dicom_tags=None,
    ):
        """Get.
        :param customfields: An array of the custom fields associated with this dicomdata. Each object has the following fields (This is only returned if the dicomdata has custom fields)
        :param uuid: Id of the DICOM data
        :param dicom_tags: A JSON list of the DICOM tags to return (optional)
        """
        request_data = {
           'uuid': uuid,
           'customfields': customfields,
           'dicom_tags': dicom_tags,
        }
	
        errors_mapping = {}
        errors_mapping['MISSING_FIELDS'] = MissingFields('A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields')
        errors_mapping['NOT_FOUND'] = NotFound('The DICOM data was not found.')
        errors_mapping['NOT_PERMITTED'] = NotPermitted('You are not permitted to view the DICOM data')
        query_data = {
            'api': self._api,
            'url': '/dicomdata/get',
            'request_data': request_data,
            'errors_mapping': errors_mapping,
            'required_sid': True,
        }
        return QueryO(**query_data)
    