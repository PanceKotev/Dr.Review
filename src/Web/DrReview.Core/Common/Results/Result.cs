namespace DrReview.Common.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using DrReview.Common.Results.Enums;

    public class Result : ResultCommonLogic
    {
        internal Result(ResultType resultType, string message)
            : base(resultType, isFailure: true, message: message)
        {
        }

        private Result()
            : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
        }

        public static Result Conflicted(string message)
        {
            return new Result(ResultType.Conflicted, message);
        }

        public static Result<T> Conflicted<T>(string message)
        {
            return new Result<T>(ResultType.Conflicted, message);
        }

        public static Result Failed(string message)
        {
            return new Result(ResultType.InternalError, message);
        }

        public static Result<T> Failed<T>(string message)
        {
            return new Result<T>(ResultType.InternalError, message);
        }

        public static Result FirstFailureOrOk(params Result[] results)
        {
            if (results.Any(f => f.IsFailure))
            {
                return results.First(f => f.IsFailure);
            }

            return Ok();
        }

        public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results)
        {
            if (results.Any(x => x.IsFailure))
            {
                return results.First(x => x.IsFailure);
            }

            return Ok();
        }

        public static Result Forbidden(string message)
        {
            return new Result(ResultType.Forbidden, message);
        }

        public static Result<T> Forbidden<T>(string message)
        {
            return new Result<T>(ResultType.Forbidden, message);
        }

        public static Result Invalid(string message)
        {
            return new Result(ResultType.Invalid, message);
        }

        public static Result<T> Invalid<T>(string message)
        {
            return new Result<T>(ResultType.Invalid, message);
        }

        public static Result NotFound(string message)
        {
            return new Result(ResultType.NotFound, message);
        }

        public static Result<T> NotFound<T>(string message)
        {
            return new Result<T>(ResultType.NotFound, message);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> FromError<T>(ResultCommonLogic result)
        {
            return new Result<T>(result.ResultType, result.Message);
        }

        public static Result Unauthorized(string message)
        {
            return new Result(ResultType.Unauthorized, message);
        }

        public static Result<T> Unauthorized<T>(string message)
        {
            return new Result<T>(ResultType.Unauthorized, message);
        }
    }

    public class Result<T> : ResultCommonLogic
    {
        internal Result(ResultType resultType, string message)
            : base(resultType, isFailure: true, message: message)
        {
            Value = Empty;
        }

        internal Result(T value)
            : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
            Value = value;
        }

        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        public T Value { get; }

        private static T Empty => default!;

        public static implicit operator T(Result<T> result) => result.Value;

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Result.Ok();
            }

            return new Result(result.ResultType, result.Message);
        }
    }

    public abstract class ResultCommonLogic
    {
        protected ResultCommonLogic(ResultType resultType, bool isFailure, string message)
        {
            if (isFailure)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(nameof(message), "There must be error message for failure.");
                }

                if (resultType == ResultType.Ok)
                {
                    throw new ArgumentException("There should be error type for failure.", nameof(resultType));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(message));
                }

                if (resultType != ResultType.Ok)
                {
                    throw new ArgumentException("There should be no error type for success.", nameof(resultType));
                }
            }

            ResultType = resultType;
            IsFailure = isFailure;
            Message = message;
        }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public bool IsNotFound => IsFailure && HttpStatusCode == HttpStatusCode.NotFound;

        public string Message { get; }

        public ResultType ResultType { get; }

        public HttpStatusCode HttpStatusCode
        {
            get
            {
                HttpStatusCode statusCode = ResultType switch
                {
                    ResultType.Ok => HttpStatusCode.OK,
                    ResultType.NotFound => HttpStatusCode.NotFound,
                    ResultType.Forbidden => HttpStatusCode.Forbidden,
                    ResultType.Conflicted => HttpStatusCode.Conflict,
                    ResultType.Invalid => HttpStatusCode.NotAcceptable,
                    ResultType.Unauthorized => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError,
                };
                return statusCode;
            }
        }
    }
}
