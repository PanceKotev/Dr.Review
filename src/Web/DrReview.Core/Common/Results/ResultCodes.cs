﻿namespace DrReview.Common.Results
{
    public static class ResultCodes
    {
        #region Common

        public const string UserNotFound = "USER_NOT_FOUND";

        public const string LocationNotFound = "LOCATION_NOT_FOUND";

        public const string NoPermission = "NO_PERMISSION";

        public const string InvalidEntityReference = "INVALID_ENTITY_REFERENCE";

        public const string RangeDateFromBeforeToDate = "RANGE_DATE_FROM_BEFORE_TO_DATE";

        #endregion

        #region Review

        public const string DoctorNotFound = "DOCTOR_NOT_FOUND";

        public const string InvalidReviewScore = "INVALID_REVIEW_SCORE";

        public const string OneReviewPerReviewee = "ONE_REVIEW_PER_REVIEWEE";

        public const string ReviewNotFound = "REVIEW_NOT_FOUND";

        #endregion

        #region ScheduleSubscription

        public const string ScheduleSubscriptionAlreadyExists = "SCHEDULE_SUBSCRIPTION_ALREADY_EXISTS";

        public const string ScheduleSubscriptionNotFound = "SCHEDULE_SUBSCRIPTION_NOT_FOUND";

        public const string ScheduleSubscriptionsNotFound = "SCHEDULE_SUBSCRIPTIONs_NOT_FOUND";


        #endregion
    }
}
