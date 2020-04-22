// """Ambra service exceptions."""

// from ambra_sdk.exceptions.base import AmbraResponseException

namespace AmbraSdk.Exceptions
{
    public class AuthorizationRequired : AmbraResponseException
    {
        // """Authorization required."""

        public AuthorizationRequired(string description = null)
            : base(code: 401, description ??
                    "The call needs a valid, logged " + 
                    "in session id (sid) or valid basic " + 
                    "authentication user name and password.")
        {
        }
            // """Init.

            // :param description: response description
            // """
    }

    public class MethodNotAllowed : AmbraResponseException
    {
        // """Method not allowed."""

        public MethodNotAllowed(string description = null)
            : base(code: 405, description ??  "The call must be a POST not a GET")
        {

            // """Init.

            // :param description: response description
            // """
        }
    }

    public class PreconditionFailed : AmbraResponseException
    {
        // """Precondition failed."""

        public PreconditionFailed(string description = null, object error_subtype = null, object error_data = null) // todo: types?
            : base(code: 412, description ?? 
                    "The returned json data structure will " +
                    "have the status flag set to ERROR. " +
                    "The error_type and the optional error_subtype " +
                    "fields will hold tokens that describe the error. " +
                    "The optional error_data field can hold additional error data.")
        {
            // """Init.

            // :param description: response description
            // :param error_subtype: error subtype
            // :param error_data: error data
            // """
            self.error_subtype = error_subtype;
            self.error_data = error_data;
        }

        public void set_additional_info(object error_subtype, object error_data) // todo: types?
        {
            // """Set additional error info.

            // :param error_subtype: error subtype
            // :param error_data: error data
            // """
            self.error_subtype = error_subtype;
            self.error_data = error_data;
        }

        public override ToString()
        {
            // """Get string represenation of exception.

            // :return: string repr
            // """
            return (
                $"{description}.\n" +
                $"Error subtype: {error_subtype}\n" +
                $"Error data: {error_data}\n"
            ).format(
                description: self.description,
                error_subtype: self.error_subtype,
                error_data: self.error_data
            );
        }
    }

    public class InvalidField : PreconditionFailed { }
        // """InvalidField.

        // The field is not valid for this object.
        // The error_subtype will hold the filter expression
        // this applies to.
        // """


    public class InvalidCondition : PreconditionFailed { }
        // """InvalidCondition.

        // The condition is not support.
        // The error_subtype will hold the filter expression
        // this applies to.
        // """


    public class FilterNotFound : PreconditionFailed { }
        // """FilterNotFound.

        // The filter can not be found.
        // The error_subtype will hold the filter UUID.
        // """


    public class InvalidSortField : PreconditionFailed { }
        // """InvalidSortField.

        // The field is not valid for this object.
        // The error_subtype will hold the field name this applies to.
        // """


    public class InvalidSortOrder : PreconditionFailed { }
        // """InvalidSortOrder.

        // The sort order for the field is invalid.
        // The error_subtype will hold the field name this applies to.
        // """


    public class InvalidHl7Field : PreconditionFailed { }
    public class Disabled : PreconditionFailed { }
    public class DupShareCode : PreconditionFailed { }
    public class NotAttending : PreconditionFailed { }
    public class AlreadyThin : PreconditionFailed { }
    public class NamespaceNotFound : PreconditionFailed { }
    public class InvalidUrl : PreconditionFailed { }
    public class InvalidVanity : PreconditionFailed { }
    public class InvalidDicomTagObject : PreconditionFailed { }
    public class InvalidSchedule : PreconditionFailed { }
    public class ParseFailed : PreconditionFailed { }
    public class InvalidSource : PreconditionFailed { }
    public class InvalidRange : PreconditionFailed { }
    public class NeedsAnyOrAll : PreconditionFailed { }
    public class UnableToValidate : PreconditionFailed { }
    public class InvalidAmount : PreconditionFailed { }
    public class NotConfigured : PreconditionFailed { }
    public class WithNotFound : PreconditionFailed { }
    public class NoHl7Support : PreconditionFailed { }
    public class NotFound : PreconditionFailed { }
    public class InvalidTag : PreconditionFailed { }
    public class Accepted : PreconditionFailed { }
    public class SidUserNotFound : PreconditionFailed { }
    public class InvalidSettingValue : PreconditionFailed { }
    public class NotAvailable : PreconditionFailed { }
    public class NoNodeOverride : PreconditionFailed { }
    public class NoValue : PreconditionFailed { }
    public class InvalidDate : PreconditionFailed { }
    public class AuthFailed : PreconditionFailed { }
    public class InvalidPhiField : PreconditionFailed { }
    public class InvalidLookup : PreconditionFailed { }
    public class NotPermitted : PreconditionFailed { }
    public class InvalidCredentials : PreconditionFailed { }
    public class Already : PreconditionFailed { }
    public class NotList : PreconditionFailed { }
    public class InvalidReplacement : PreconditionFailed { }
    public class AlreadyDone : PreconditionFailed { }
    public class DestinationNotFound : PreconditionFailed { }
    public class OnlyOneFlag : PreconditionFailed { }
    public class AccountNotSet : PreconditionFailed { }
    public class AllDone : PreconditionFailed { }
    public class InUse : PreconditionFailed { }
    public class TokenFailed : PreconditionFailed { }
    public class NotMember : PreconditionFailed { }
    public class InvalidValue : PreconditionFailed { }
    public class DuplicateName : PreconditionFailed { }
    public class InvalidStatus : PreconditionFailed { }
    public class InvalidLink : PreconditionFailed { }
    public class InvalidFilterField : PreconditionFailed { }
    public class InvalidRegexp : PreconditionFailed { }
    public class NoFreshArchive : PreconditionFailed { }
    public class UnableToGenerate : PreconditionFailed { }
    public class InvalidMetric : PreconditionFailed { }
    public class ByNotFound : PreconditionFailed { }
    public class Lockout : PreconditionFailed { }
    public class StudyNotFound : PreconditionFailed { }
    public class InvalidCaseStatus : PreconditionFailed { }
    public class DuplicateOrderBy : PreconditionFailed { }
    public class InvalidEndDate : PreconditionFailed { }
    public class InvalidEvent : PreconditionFailed { }
    public class BadPassword : PreconditionFailed { }
    public class PinExpired : PreconditionFailed { }
    public class InvalidPassword : PreconditionFailed { }
    public class InvalidAction : PreconditionFailed { }
    public class RoleNotFound : PreconditionFailed { }
    public class InvalidOtherNamespaces : PreconditionFailed { }
    public class InvalidDateTime : PreconditionFailed { }
    public class MissingInformation : PreconditionFailed { }
    public class InvalidCount : PreconditionFailed { }
    public class DuplicateEmail : PreconditionFailed { }
    public class NodeNotSetup : PreconditionFailed { }
    public class AccountNotFound : PreconditionFailed { }
    public class IncompleteFilter : PreconditionFailed { }
    public class InvalidVendor : PreconditionFailed { }
    public class ChargeRequired : PreconditionFailed { }
    public class NoFilter : PreconditionFailed { }
    public class NoAttachment : PreconditionFailed { }
    public class HasDestinations : PreconditionFailed { }
    public class DelayOrMatch : PreconditionFailed { }
    public class NotInAccount : PreconditionFailed { }
    public class NoDicomTagDefined : PreconditionFailed { }
    public class InvalidCharge : PreconditionFailed { }
    public class InvalidCustomfield : PreconditionFailed { }
    public class RecentNamespaceSplit : PreconditionFailed { }
    public class NotThin : PreconditionFailed { }
    public class MissingInfo : PreconditionFailed { }
    public class NotANumber : PreconditionFailed { }
    public class InvalidJson : PreconditionFailed { }
    public class InvalidPeriod : PreconditionFailed { }
    public class InProcess : PreconditionFailed { }
    public class Locked : PreconditionFailed { }
    public class InvalidFilter : PreconditionFailed { }
    public class Stale : PreconditionFailed { }
    public class OnlyAll : PreconditionFailed { }
    public class SidUserNotInAccount : PreconditionFailed { }
    public class CanNotPromote : PreconditionFailed { }
    public class InvalidGatewayType : PreconditionFailed { }
    public class NoOtherRoleEdit : PreconditionFailed { }
    public class ChargeFailed : PreconditionFailed { }
    public class InvalidManualRoles : PreconditionFailed { }
    public class InvalidObject : PreconditionFailed { }
    public class OneZipOnly : PreconditionFailed { }
    public class Blocked : PreconditionFailed { }
    public class InvalidSearchSource : PreconditionFailed { }
    public class InvalidPermissionValue : PreconditionFailed { }
    public class InvalidPin : PreconditionFailed { }
    public class Phantom : PreconditionFailed { }
    public class AlreadyUsed : PreconditionFailed { }
    public class InvalidCode : PreconditionFailed { }
    public class AlreadyExists : PreconditionFailed { }
    public class InvalidPhone : PreconditionFailed { }
    public class InvalidUploadMatch : PreconditionFailed { }
    public class ShareFailed : PreconditionFailed { }
    public class NotEnabled : PreconditionFailed { }
    public class InvalidToken : PreconditionFailed { }
    public class InvalidParameters : PreconditionFailed { }
    public class NotSupported : PreconditionFailed { }
    public class NotWithCron : PreconditionFailed { }
    public class ValidationFailed : PreconditionFailed { }
    public class InvalidEmail : PreconditionFailed { }
    public class InvalidConfiguration : PreconditionFailed { }
    public class InvalidOptions : PreconditionFailed { }
    public class GtZero : PreconditionFailed { }
    public class SfdcMissingFields : PreconditionFailed { }
    public class InvalidHl7Object : PreconditionFailed { }
    public class InvalidInteger : PreconditionFailed { }
    public class InvalidDicomTag : PreconditionFailed { }
    public class MissingFields : PreconditionFailed { }
    public class Pending : PreconditionFailed { }
    public class NotDisabled : PreconditionFailed { }
    public class ReportError : PreconditionFailed { }
    public class PendingMustMatch : PreconditionFailed { }
    public class DuplicateVanity : PreconditionFailed { }
    public class Validate : PreconditionFailed { }
    public class PdfFailed : PreconditionFailed { }
    public class InvalidMethod : PreconditionFailed { }
    public class InvalidPermission : PreconditionFailed { }
    public class InvalidTimeZone : PreconditionFailed { }
    public class LinkNotFound : PreconditionFailed { }
    public class InvalidLinkage : PreconditionFailed { }
    public class InvalidNodeType : PreconditionFailed { }
    public class ExamNotFound : PreconditionFailed { }
    public class InvalidFieldName : PreconditionFailed { }
    public class NoUserOverride : PreconditionFailed { }
    public class InsufficientCriteria : PreconditionFailed { }
    public class WhitelistLockout : PreconditionFailed { }
    public class InvalidCurrency : PreconditionFailed { }
    public class MissingParameters : PreconditionFailed { }
    public class InvalidHl7 : PreconditionFailed { }
    public class ErrorCreatingStudy : PreconditionFailed { }
    public class TryLater : PreconditionFailed { }
    public class AlreadyMember : PreconditionFailed { }
    public class PasswordReset : PreconditionFailed { }
    public class InvalidHl7Segment : PreconditionFailed { }
    public class NoOauth : PreconditionFailed { }
    public class Running : PreconditionFailed { }
public 
    public class CaptchaFailed : PreconditionFailed { }
    public class Singleton : PreconditionFailed { }
    public class ReportFailed : PreconditionFailed { }
    public class UserNotFound : PreconditionFailed { }
    public class NotASearch : PreconditionFailed { }
    public class CustomNotHash : PreconditionFailed { }
    public class AccountUserNotFound : PreconditionFailed { }
    public class Failed : PreconditionFailed { }
    public class InvalidDelay : PreconditionFailed { }
    public class Retrieve : PreconditionFailed { }
    public class InvalidTemplate : PreconditionFailed { }
    public class SfdcNotHash : PreconditionFailed { }
    public class InvalidOption : PreconditionFailed { }
    public class InvalidType : PreconditionFailed { }
    public class OtherOauth : PreconditionFailed { }
    public class InvalidSid : PreconditionFailed { }
    public class LookupFailed : PreconditionFailed { }
    public class InvalidCron : PreconditionFailed { }
    public class NoQueryDestination : PreconditionFailed { }
    public class InvalidFlag : PreconditionFailed { }
    public class InvalidTransformCondition : PreconditionFailed { }
    public class InvalidNpi : PreconditionFailed { }
    public class NotEmpty : PreconditionFailed { }
    public class Expired : PreconditionFailed { }
    public class NodeNotFound : PreconditionFailed { }
    public class InvalidCdBurnInfo : PreconditionFailed { }
    public class NotHash : PreconditionFailed { }
    public class NotSysadminOrSupport : PreconditionFailed { }
    public class IsDeployed : PreconditionFailed { }
    public class InvalidUuid : PreconditionFailed { }
    public class DupAetitle : PreconditionFailed { }
    public class InvalidMessage : PreconditionFailed { }
    public class InvalidSetting : PreconditionFailed { }
    public class IpBlocked : PreconditionFailed { }
    public class InvalidBucket : PreconditionFailed { }
    public class InvalidLanguage : PreconditionFailed { }
    public class DecryptFailed : PreconditionFailed { }
    public class ScheduleIsOff : PreconditionFailed { }
    public class NotReady : PreconditionFailed { }
    public class RouteNotMatched : PreconditionFailed { }
}