""" Link.

Do not edit this file by hand.
This is generated by parsing api.html service doc.
"""
from ambra_sdk.exceptions.service import AccountNotSet
from ambra_sdk.exceptions.service import AccountUserNotFound
from ambra_sdk.exceptions.service import ChargeFailed
from ambra_sdk.exceptions.service import ChargeRequired
from ambra_sdk.exceptions.service import DecryptFailed
from ambra_sdk.exceptions.service import Disabled
from ambra_sdk.exceptions.service import Expired
from ambra_sdk.exceptions.service import InvalidAction
from ambra_sdk.exceptions.service import InvalidCharge
from ambra_sdk.exceptions.service import InvalidCredentials
from ambra_sdk.exceptions.service import InvalidEmail
from ambra_sdk.exceptions.service import InvalidFieldName
from ambra_sdk.exceptions.service import InvalidJson
from ambra_sdk.exceptions.service import InvalidPhiField
from ambra_sdk.exceptions.service import InvalidPhone
from ambra_sdk.exceptions.service import InvalidRegexp
from ambra_sdk.exceptions.service import InvalidSource
from ambra_sdk.exceptions.service import InvalidUploadMatch
from ambra_sdk.exceptions.service import IpBlocked
from ambra_sdk.exceptions.service import LinkNotFound
from ambra_sdk.exceptions.service import MissingFields
from ambra_sdk.exceptions.service import MissingInfo
from ambra_sdk.exceptions.service import MissingParameters
from ambra_sdk.exceptions.service import NoFilter
from ambra_sdk.exceptions.service import NotFound
from ambra_sdk.exceptions.service import NotHash
from ambra_sdk.exceptions.service import NotList
from ambra_sdk.exceptions.service import NotPermitted
from ambra_sdk.exceptions.service import Validate
from ambra_sdk.service.query import QueryO
from ambra_sdk.service.query import QueryOP

class Link:
    """Link."""

    def __init__(self, api):
        self._api = api

    
    def list(
        self,
        account_id=None,
        study_id=None,
        user_id=None,
    ):
        """List.
        :param account_id: account_id
        :param study_id: study_id
        :param user_id: user_id

        Notes:
        (study_id OR user_id OR account_id) - uuid of the study, user or account to get the links for
        """
        request_data = {
           "study_id": study_id,
           "user_id": user_id,
           "account_id": account_id,
        }
	
        errors_mapping = {}
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The account can not be found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view this list")
        query_data = {
            "api": self._api,
            "url": "/link/list",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        query_data["paginated_field"] = "links"
        return QueryOP(**query_data)
    
    def add(
        self,
        prompt_for_anonymize,
        acceptance_required=None,
        account_id=None,
        action=None,
        anonymize=None,
        charge_amount=None,
        charge_currency=None,
        charge_description=None,
        email=None,
        filter=None,
        include_priors=None,
        max_hits=None,
        meeting_id=None,
        message=None,
        minutes_alive=None,
        mobile_phone=None,
        namespace_id=None,
        notify=None,
        parameters=None,
        password=None,
        password_is_dob=None,
        password_max_attempts=None,
        pin_auth=None,
        referer=None,
        share_code=None,
        share_on_view=None,
        skip_email_prompt=None,
        study_id=None,
        upload_match=None,
        use_share_code=None,
    ):
        """Add.
        :param prompt_for_anonymize: Flag to prompt if the anonymization rules should be applied on ingress
        :param acceptance_required: Flag that acceptance of TOS is required (optional)
        :param account_id: account_id
        :param action: action
        :param anonymize: Anonymization rules to the applied to any STUDY_UPLOAD done with this link. Rules are formatted as per the rules parameter in /namespace/anonymize  (optional)
        :param charge_amount: Amount to charge in pennies before the link can be accessed (optional)
        :param charge_currency: Charge currency (optional)
        :param charge_description: Charge description (optional)
        :param email: Email the link to this address (optional)
        :param filter: filter
        :param include_priors: Include prior studies (optional)
        :param max_hits: The maximum number of times the link can be used (optional)
        :param meeting_id: UUID of the meeting to associate the link with (optional)
        :param message: Message to include in the email (optional)
        :param minutes_alive: The maximum number of minutes the link will be alive for (optional)
        :param mobile_phone: Send the link to this phone number (optional)
        :param namespace_id: namespace_id
        :param notify: Comma or space separated list of additional emails to notify of link usage (optional)
        :param parameters: JSON array of parameters to add to the redirect URL or return in /namespace/share_code if an upload (optional)
        :param password: Link password (optional)
        :param password_is_dob: Flag that the password is the patient_birth_date for the study (study_id is required) (optional)
        :param password_max_attempts: The maximum number of failed password attempt (optional)
        :param pin_auth: An account member email and PIN authentication is required (optional)
        :param referer: The link can only be accessed from the specified referer. The referer can be a regexp to match multiple referers (optional)
        :param share_code: share code for a STUDY_UPLOAD (optional)
        :param share_on_view: Flag to share the study with the email after it is viewed (optional)
        :param skip_email_prompt: Skip the prompt for email step (optional)
        :param study_id: study_id
        :param upload_match: A JSON hash of DICOM tags and regular expressions they must match uploaded against this link (optional)
        :param use_share_code: Flag to use the namespace share code settings for a STUDY_UPLOAD (optional)

        Notes:
        (study_id OR filter AND account_id OR namespace_id) - uuid of the study or a JSON hash of the study filter expression and the account_id or namespace_id if the action is STUDY_UPLOAD
        action - Link action (STUDY_LIST OR STUDY_VIEW OR STUDY_UPLOAD)
        """
        request_data = {
           "share_on_view": share_on_view,
           "max_hits": max_hits,
           "charge_description": charge_description,
           "study_id": study_id,
           "action": action,
           "anonymize": anonymize,
           "pin_auth": pin_auth,
           "use_share_code": use_share_code,
           "parameters": parameters,
           "message": message,
           "include_priors": include_priors,
           "email": email,
           "mobile_phone": mobile_phone,
           "referer": referer,
           "charge_amount": charge_amount,
           "meeting_id": meeting_id,
           "password": password,
           "password_is_dob": password_is_dob,
           "acceptance_required": acceptance_required,
           "filter": filter,
           "skip_email_prompt": skip_email_prompt,
           "share_code": share_code,
           "prompt_for_anonymize": prompt_for_anonymize,
           "charge_currency": charge_currency,
           "minutes_alive": minutes_alive,
           "upload_match": upload_match,
           "account_id": account_id,
           "namespace_id": namespace_id,
           "password_max_attempts": password_max_attempts,
           "notify": notify,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_ACTION"] = InvalidAction("An invalid action was passed")
        errors_mapping["INVALID_CHARGE"] = InvalidCharge("The charge is invalid. The error_subtype holds the details on the error")
        errors_mapping["INVALID_EMAIL"] = InvalidEmail("An invalid email address was passed")
        errors_mapping["INVALID_FIELD_NAME"] = InvalidFieldName("The field name in the rules hash is invalid. The error_subtype holds the invalid field name")
        errors_mapping["INVALID_JSON"] = InvalidJson("The field is not in valid JSON format. The error_subtype holds the name of the field")
        errors_mapping["INVALID_PHI_FIELD"] = InvalidPhiField("The password_is_phi field is invalid or a study_id was not passed")
        errors_mapping["INVALID_PHONE"] = InvalidPhone("An invalid cellular phone number was passed")
        errors_mapping["INVALID_REGEXP"] = InvalidRegexp("Invalid regular expression. The error_subtype holds the invalid regexp.")
        errors_mapping["INVALID_UPLOAD_MATCH"] = InvalidUploadMatch("The upload_match is invalid. The error_subtype holds the details on the error")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The patient or study could not be found. The error_subtype holds the uuid that can not be found")
        errors_mapping["NOT_HASH"] = NotHash("The rules field is not a hash")
        errors_mapping["NOT_LIST"] = NotList("The field is not a JSON array. The error_subtype holds the name of the field")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to create links")
        errors_mapping["VALIDATE"] = Validate("A validation error. The error_subtype holds the details on the error")
        query_data = {
            "api": self._api,
            "url": "/link/add",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def get(
        self,
        acceptance_required,
        account_id,
        action,
        anonymize,
        charge_amount,
        charge_currency,
        charge_description,
        description,
        email,
        filter,
        has_password,
        include_priors,
        is_meeting,
        max_hits,
        message,
        minutes_alive,
        mobile_phone,
        namespace_id,
        namespace_name,
        notify,
        parameters,
        password_is_dob,
        password_max_attempts,
        pin_auth,
        prompt_for_anonymize,
        redirect_url,
        referer,
        share_on_view,
        skip_email_prompt,
        study_id,
        upload_match,
        url,
        use_share_code,
        user_id,
        uuid,
    ):
        """Get.
        :param acceptance_required: Flag that acceptance of TOS is required
        :param account_id: The account id
        :param action: Link action
        :param anonymize: Any anonymization rules
        :param charge_amount: Amount to charge in pennies before the link can be accessed
        :param charge_currency: Charge currency
        :param charge_description: Charge description
        :param description: Description of the link
        :param email: Email address the link was sent to
        :param filter: The filter expression
        :param has_password: Flag if the link has a password or not
        :param include_priors: Include prior studies
        :param is_meeting: Flag if the link is for a meeting
        :param max_hits: The maximum number of times the link can be used
        :param message: Message to include in the email
        :param minutes_alive: The maximum number of minutes the link will be alive for
        :param mobile_phone: Cellular phone number the link was sent to
        :param namespace_id: Id of the namespace for upload links
        :param namespace_name: Name of the namespace for upload links
        :param notify: Comma or space separated list of additional emails to notify of link usage
        :param parameters: JSON array parameters to add to the redirect URL
        :param password_is_dob: Flag that the password is the patient_birth_date for the study
        :param password_max_attempts: The maximum number of failed password attempt
        :param pin_auth: An account member email and PIN authentication is required
        :param prompt_for_anonymize: Flag to prompt if the anonymization rules should be applied on ingress
        :param redirect_url: URL for the /link/redirect API  which will take you directly to the study viewer or uploader
        :param referer: The link can only be accessed from the specified referer
        :param share_on_view: Flag to share the study with the email after it is viewed
        :param skip_email_prompt: Skip the prompt for email step
        :param study_id: uuid of the study
        :param upload_match: A JSON hash of DICOM tags and regular expressions they must match uploaded against this link
        :param url: URL for the link which will take you to the UI entry point for links to enter email, password etc.
        :param use_share_code: Flag to use the namespace share code settings for a STUDY_UPLOAD
        :param user_id: The user id
        :param uuid: Id of the link
        """
        request_data = {
           "redirect_url": redirect_url,
           "share_on_view": share_on_view,
           "max_hits": max_hits,
           "url": url,
           "charge_description": charge_description,
           "study_id": study_id,
           "action": action,
           "is_meeting": is_meeting,
           "anonymize": anonymize,
           "pin_auth": pin_auth,
           "use_share_code": use_share_code,
           "parameters": parameters,
           "message": message,
           "mobile_phone": mobile_phone,
           "email": email,
           "include_priors": include_priors,
           "referer": referer,
           "charge_amount": charge_amount,
           "uuid": uuid,
           "password_is_dob": password_is_dob,
           "acceptance_required": acceptance_required,
           "filter": filter,
           "skip_email_prompt": skip_email_prompt,
           "prompt_for_anonymize": prompt_for_anonymize,
           "namespace_name": namespace_name,
           "charge_currency": charge_currency,
           "description": description,
           "minutes_alive": minutes_alive,
           "upload_match": upload_match,
           "user_id": user_id,
           "account_id": account_id,
           "has_password": has_password,
           "namespace_id": namespace_id,
           "password_max_attempts": password_max_attempts,
           "notify": notify,
        }
	
        errors_mapping = {}
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to view the link")
        query_data = {
            "api": self._api,
            "url": "/link/get",
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
        :param uuid: Id of the link
        """
        request_data = {
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to delete the link")
        query_data = {
            "api": self._api,
            "url": "/link/delete",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def status(
        self,
        uuid,
        link_charge_id=None,
    ):
        """Status.
        :param uuid: Id of the link
        :param link_charge_id: The uuid of the prior charge against this link (optional)
        """
        request_data = {
           "link_charge_id": link_charge_id,
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_SOURCE"] = InvalidSource("The referer is invalid")
        errors_mapping["IP_BLOCKED"] = IpBlocked("An IP whitelist blocked access")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        query_data = {
            "api": self._api,
            "url": "/link/status",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    
    def session(
        self,
        uuid,
        email_address=None,
        link_charge_id=None,
        password=None,
    ):
        """Session.
        :param uuid: Id of the link
        :param email_address: The users email (optional)
        :param link_charge_id: The uuid of the prior charge against this link (optional)
        :param password: Password if needed (optional)
        """
        request_data = {
           "link_charge_id": link_charge_id,
           "uuid": uuid,
           "password": password,
           "email_address": email_address,
        }
	
        errors_mapping = {}
        errors_mapping["CHARGE_REQUIRED"] = ChargeRequired("A charge is required to access this link")
        errors_mapping["DISABLED"] = Disabled("This call is disabled for the account, you must use /link/redirect")
        errors_mapping["INVALID_CREDENTIALS"] = InvalidCredentials("Invalid password or email if pin_auth is required.")
        errors_mapping["INVALID_SOURCE"] = InvalidSource("The referer is invalid")
        errors_mapping["IP_BLOCKED"] = IpBlocked("An IP whitelist blocked access")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        query_data = {
            "api": self._api,
            "url": "/link/session",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    
    def redirect(
        self,
        uuid,
        link_charge_id=None,
        password=None,
    ):
        """Redirect.
        :param uuid: Id of the link
        :param link_charge_id: The uuid of the prior charge against this link (optional)
        :param password: Password if needed (optional)
        """
        request_data = {
           "link_charge_id": link_charge_id,
           "uuid": uuid,
           "password": password,
        }
	
        errors_mapping = {}
        errors_mapping["EXPIRED"] = Expired("The link has expired")
        errors_mapping["INVALID_CREDENTIALS"] = InvalidCredentials("Invalid password.")
        errors_mapping["INVALID_SOURCE"] = InvalidSource("The referer is invalid")
        errors_mapping["IP_BLOCKED"] = IpBlocked("An IP whitelist blocked access")
        errors_mapping["LINK_NOT_FOUND"] = LinkNotFound("The link was not found")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("The configuration of this link requires the link UI be used instead of direct access")
        query_data = {
            "api": self._api,
            "url": "/link/redirect",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def external(
        self,
        u,
        v=None,
    ):
        """External.
        :param u: The uuid of the user_account record to create the guest link as
        :param v: 
        A JSON hash with the following keys pairs. The JSON must be encrypted and base64 encoded:
        filter.*=&gt;Filter field(s) as per the /study/list to specify the study(s) to construct the link for.
        The include_priors link option value can be passed as a key.
        Any additional fields will the saved in the study audit trail and the following fields email_address, redirect_url, integration_key and skip_email_prompt will be available in /namespace/share_code if this is an upload link .
        """
        request_data = {
           "u": u,
           "v": v,
        }
	
        errors_mapping = {}
        errors_mapping["ACCOUNT_NOT_SET"] = AccountNotSet("The account is not setup for the integration")
        errors_mapping["ACCOUNT_USER_NOT_FOUND"] = AccountUserNotFound("The account user record was not found")
        errors_mapping["DECRYPT_FAILED"] = DecryptFailed("The decryption failed")
        errors_mapping["INVALID_SOURCE"] = InvalidSource("The referer is invalid")
        errors_mapping["MISSING_PARAMETERS"] = MissingParameters("The u or v parameter is missing")
        errors_mapping["NOT_HASH"] = NotHash("The v parameter is not a JSON hash")
        errors_mapping["NO_FILTER"] = NoFilter("A filter expressions was not passed")
        query_data = {
            "api": self._api,
            "url": "/link/external",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    
    def sso(
        self,
        u,
        v,
    ):
        """Sso.
        :param u: The uuid of the user_account record
        :param v: An encrypted JSON hash as per the instructions in the SSO to a PHR account with a study share section of the documentation
        """
        request_data = {
           "u": u,
           "v": v,
        }
	
        errors_mapping = {}
        errors_mapping["ACCOUNT_NOT_SET"] = AccountNotSet("The account is not setup for the integration")
        errors_mapping["ACCOUNT_USER_NOT_FOUND"] = AccountUserNotFound("The account user record was not found")
        errors_mapping["DECRYPT_FAILED"] = DecryptFailed("The decryption failed")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["MISSING_INFO"] = MissingInfo("User information is missing from the hash")
        errors_mapping["NOT_HASH"] = NotHash("The v parameter is not a JSON hash")
        query_data = {
            "api": self._api,
            "url": "/link/sso",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    
    def sid(
        self,
        email,
        uuid,
    ):
        """Sid.
        :param email: Email address to associate with this usage
        :param uuid: The uuid of the link usage
        """
        request_data = {
           "uuid": uuid,
           "email": email,
        }
	
        errors_mapping = {}
        errors_mapping["NOT_FOUND"] = NotFound("The usage was not found")
        query_data = {
            "api": self._api,
            "url": "/link/sid",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    
    def mail(
        self,
        email,
        uuid,
    ):
        """Mail.
        :param email: Email address
        :param uuid: The uuid of the link
        """
        request_data = {
           "uuid": uuid,
           "email": email,
        }
	
        errors_mapping = {}
        errors_mapping["INVALID_EMAIL"] = InvalidEmail("Enter a valid email address")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        errors_mapping["NOT_PERMITTED"] = NotPermitted("You are not permitted to do this")
        query_data = {
            "api": self._api,
            "url": "/link/mail",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": True,
        }
        return QueryO(**query_data)
    
    def charge(
        self,
        charge_token,
        uuid,
    ):
        """Charge.
        :param charge_token: The stripe charge token
        :param uuid: The uuid of the link
        """
        request_data = {
           "charge_token": charge_token,
           "uuid": uuid,
        }
	
        errors_mapping = {}
        errors_mapping["CHARGE_FAILED"] = ChargeFailed("The charge failed. The error_subtype holds the details on the error")
        errors_mapping["MISSING_FIELDS"] = MissingFields("A required field is missing or does not have data in it. The error_subtype holds a array of all the missing fields")
        errors_mapping["NOT_FOUND"] = NotFound("The link was not found")
        query_data = {
            "api": self._api,
            "url": "/link/charge",
            "request_data": request_data,
            "errors_mapping": errors_mapping,
            "required_sid": False,
        }
        return QueryO(**query_data)
    