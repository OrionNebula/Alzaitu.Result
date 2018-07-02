// ReSharper disable UnusedTypeParameter

namespace Alzaitu.Result
{
    /// <summary>
    /// The high-level Result type for methods that return a value.
    /// </summary>
    /// <typeparam name="TResult">The type of the result on success</typeparam>
    /// <typeparam name="TError">The type of the result on failure</typeparam>
    public abstract class Result<TResult, TError>
    {
        internal Result() { }
    }

    /// <summary>
    /// The high-level Result type for methods that return void.
    /// </summary>
    /// <typeparam name="TError">The type of the result on failure</typeparam>
    public abstract class Result<TError>
    {
        internal Result() { }
    }

    /// <inheritdoc />
    /// <summary>
    /// Returned when the method has succeeded.
    /// </summary>
    /// <typeparam name="TResult">The type of the result on success.</typeparam>
    /// <typeparam name="TError">The type of the result on failure.</typeparam>
    public sealed class Ok<TResult, TError> : Result<TResult, TError>
    {
        /// <summary>
        /// The result value of some operation.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Construct a new Ok with the specified result value.
        /// </summary>
        /// <param name="value">The result of some operation.</param>
        public Ok(TResult value)
        {
            Value = value;
        }

        public static implicit operator TResult(Ok<TResult, TError> ok) => ok.Value;
    }

    /// <inheritdoc />
    /// <summary>
    /// Returned when the method has succeeded.
    /// </summary>
    /// <typeparam name="TError">The type of the result on failure.</typeparam>
    public sealed class Ok<TError> : Result<TError>
    {
        public Ok() { }
    }

    /// <inheritdoc />
    /// <summary>
    /// Returned when the method has failed.
    /// </summary>
    /// <typeparam name="TResult">The type of the result on success.</typeparam>
    /// <typeparam name="TError">The type of the result on failure.</typeparam>
    public sealed class Err<TResult, TError> : Result<TResult, TError>
    {
        /// <summary>
        /// The error value from some operation.
        /// </summary>
        public TError Error { get; }

        /// <summary>
        /// Construct a new Err with the specified error value.
        /// </summary>
        /// <param name="error">The error from some operation.</param>
        public Err(TError error)
        {
            Error = error;
        }

        public static implicit operator TError(Err<TResult, TError> err) => err.Error;
    }

    /// <inheritdoc />
    /// <summary>
    /// Returned when the method has failed.
    /// </summary>
    /// <typeparam name="TError">The type of the result on failure.</typeparam>
    public sealed class Err<TError> : Result<TError>
    {
        /// <summary>
        /// The error value from some operation.
        /// </summary>
        public TError Error { get; }

        /// <summary>
        /// Construct a new Err with the specified error value.
        /// </summary>
        /// <param name="error">The error from some operation.</param>
        public Err(TError error)
        {
            Error = error;
        }

        public static implicit operator TError(Err<TError> err) => err.Error;
    }
}
