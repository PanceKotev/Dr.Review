﻿namespace DrReview.Common.Results
{
    public static class ResultCodes
    {
        #region Common

        public const string UserNotFound = "USER_NOT_FOUND";

        public const string NoPermission = "NO_PERMISSION";

        #endregion

        #region Review

        public const string DoctorNotFound = "DOCTOR_NOT_FOUND";

        public const string InvalidReviewScore = "INVALID_REVIEW_SCORE";

        public const string OneReviewPerReviewee = "ONE_REVIEW_PER_REVIEWEE";

        public const string ReviewNotFound = "REVIEW_NOT_FOUND";

        #endregion
    }
}
