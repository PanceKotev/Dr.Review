namespace DrReview.Common.Extensions
{
    using System;

    public static class IdExtensions
    {
        public static bool IsValidId(this long id)
        {
            return id > 0;
        }

        public static bool IsValidUid(this Guid uid)
        {
            return !uid.Equals(Guid.Empty);
        }
    }
}
