""" Training.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import AllDone
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.service.query import QueryO

class Training:
    """Training."""

    def __init__(self, api):
        self._api = api

    
    def todo(
        self,
    ):
        """Todo.
        """
        request_data = {
        }
	
        errors_mapping = {}
        errors_mapping["ALL_DONE"] = AllDone("No more training is needed")
        query_data = {
            "api": self._api,
            "url": "/training/todo",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def done(
        self,
        account_id,
        additional_parameters,
        form_number,
    ):
        """Done.
        :param account_id: Id of the account the training is for
        :param additional_parameters: All additional parameters will be logged as part of the TRAINING_DONE user audit event
        :param form_number: The formstack id of the form
        """
        request_data = {
           "form_number": form_number,
           "account_id": account_id,
        }
        if additional_parameters is not None:
            additional_parameters_dict = {"{prefix}{k}".format(prefix="", k=k): v for k,v in additional_parameters.items()}
            request_data.update(additional_parameters_dict)
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The form was not found for this user")
        query_data = {
            "api": self._api,
            "url": "/training/done",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    