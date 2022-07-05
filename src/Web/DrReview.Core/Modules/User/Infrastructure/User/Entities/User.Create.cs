namespace DrReview.Modules.User.Infrastructure.User.Entities
{
    using System;
    using System.Collections.Generic;
    using CSharpVitamins;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;

    public partial class User
    {
        public static Result<User> Create(Guid uid,
                                          string firstName,
                                          string lastName,
                                          string emailAddress)
        {
            User user = new User(
                    id: default,
                    uid: uid,
                    suid: new ShortGuid(uid),
                    deletedOn: null,
                    modifiedOn: DateTime.UtcNow,
                    firstName: firstName,
                    lastName: lastName,
                    emailAddress: emailAddress);

            return Result.Ok(user);
        }
    }
}
