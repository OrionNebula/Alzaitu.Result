using System;
using System.Collections.Generic;
using System.Linq;

namespace Alzaitu.Result
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Given a set of optionally returning objects, ignore the failures and produce the successes
        /// </summary>
        /// <typeparam name="TResult">The type returned on successs</typeparam>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <returns>A lazy-loaded stream of values</returns>
        public static IEnumerable<TResult>
            WhereSuccess<TResult, TError>(this IEnumerable<Result<TResult, TError>> ths) => ths.WhereSuccess(null);

        /// <summary>
        /// Given a set of optionally returning objects, ignore the failures and produce the successes
        /// </summary>
        /// <typeparam name="TResult">The type returned on successs</typeparam>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <param name="onError">A callback to invoke whenever an error is encountered</param>
        /// <returns>A lazy-loaded stream of values</returns>
        public static IEnumerable<TResult> WhereSuccess<TResult, TError>(this IEnumerable<Result<TResult, TError>> ths, Action<TError> onError)
        {
            foreach (var result in ths)
            {
                switch (result)
                {
                    case Err<TResult, TError> err:
                        onError?.Invoke(err.Error);
                        break;
                    case Ok<TResult, TError> ok:
                        yield return ok.Value;
                        break;
                }
            }
        }

        /// <summary>
        /// Given a set of optionally returning objects, ignore the successes and produce the failures
        /// </summary>
        /// <typeparam name="TResult">The type returned on successs</typeparam>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <returns>A lazy-loaded stream of values</returns>
        public static IEnumerable<TError>
            WhereFailure<TResult, TError>(this IEnumerable<Result<TResult, TError>> ths) => ths.WhereFailure(null);

        /// <summary>
        /// Given a set of optionally returning objects, ignore the successes and produce the failures
        /// </summary>
        /// <typeparam name="TResult">The type returned on successs</typeparam>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <param name="onOk">A callback to invoke whenever a success is encountered</param>
        /// <returns>A lazy-loaded stream of values</returns>
        public static IEnumerable<TError> WhereFailure<TResult, TError>(this IEnumerable<Result<TResult, TError>> ths, Action<TResult> onOk)
        {
            foreach (var result in ths)
            {
                switch (result)
                {
                    case Err<TResult, TError> err:
                        yield return err.Error;
                        break;
                    case Ok<TResult, TError> ok:
                        onOk?.Invoke(ok.Value);
                        break;
                }
            }
        }

        /// <summary>
        /// Given a set of optionally returning objects, ignore the successes and produce the failures
        /// </summary>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <returns>A lazy-loaded stream of values</returns>
        public static IEnumerable<TError> WhereFailure<TError>(this IEnumerable<Result<TError>> ths)
        {
            return ths.OfType<Err<TError>>().Select(x => x.Error);
        }

        /// <summary>
        /// Convert an IEnumerable to a ResultEnumerable
        /// </summary>
        /// <typeparam name="TResult">The type returned on success</typeparam>
        /// <typeparam name="TError">The type returned on failure</typeparam>
        /// <param name="ths">The IEnumerable to transform</param>
        /// <returns>A ResultEnumerable for the given object stream</returns>
        public static ResultEnumerable<TResult, TError> ToResultEnumerable<TResult, TError>(this IEnumerable<Result<TResult, TError>> ths)
        {
            return new ResultEnumerable<TResult, TError>(ths);
        }
    }
}
