namespace DrReview.Modules.User.Infrastructure.User.Entities
{
    using System;
    using DrReview.Common.Results;

    public partial class User
    {
        public Result<User> Update(
                                  string firstName,
                                  string lastName)
        {
            if (string.IsNullOrEmpty(firstName.Trim()) || string.IsNullOrEmpty(lastName.Trim())
                || firstName.Trim().Length > 200 || lastName.Trim().Length > 200)
            {
                return Result.Invalid<User>("Invalid parameters");
            }

            this.FirstName = firstName.Trim();
            this.LastName = lastName.Trim();
            this.ModifiedOn = DateTime.UtcNow;


            return Result.Ok(this);
        }
    }
}
