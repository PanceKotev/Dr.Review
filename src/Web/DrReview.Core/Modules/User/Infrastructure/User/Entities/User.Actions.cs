namespace DrReview.Modules.User.Infrastructure.User.Entities
{
    using CSharpVitamins;
    using DrReview.Common.Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

            return Result.Ok(this);
        }
    }
}
