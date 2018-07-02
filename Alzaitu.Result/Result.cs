using System;

namespace Alzaitu.Result
{
    public interface IResult<out TResult, out TError>
    {
    }

    public class Ok<TResult, TError> : IResult<TResult, TError>
    {
        public TResult Value { get; }

        public Ok(TResult value)
        {
            Value = value;
        }

        public static implicit operator TResult(Ok<TResult, TError> ok)
        {
            return ok.Value;
        }
    }

    public class Err<TResult, TError> : IResult<TResult, TError>
    {
        public TError Error { get; }

        public Err(TError error)
        {
            Error = error;
        }

        public static implicit operator TError(Err<TResult, TError> err)
        {
            return err.Error;
        }
    }
}
