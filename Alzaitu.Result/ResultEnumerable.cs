using System;
using System.Collections.Generic;

namespace Alzaitu.Result
{
    /// <inheritdoc />
    /// <summary>
    /// Enumerate the failures and successes in an IEnumerable seperately
    /// </summary>
    /// <typeparam name="TResult">The type returned on success</typeparam>
    /// <typeparam name="TError">The type returned on failure</typeparam>
    public class ResultEnumerable<TResult, TError> : IDisposable
    {
        private readonly Queue<TResult> _resultList = new Queue<TResult>();
        private readonly Queue<TError> _errorList = new Queue<TError>();

        private readonly IEnumerator<Result<TResult, TError>> _enumerable;

        public ResultEnumerable(IEnumerable<Result<TResult, TError>> enumerable)
        {
            _enumerable = enumerable.GetEnumerator();
        }

        /// <summary>
        /// Enumerate the successful results, caching the failures to be enumerated later
        /// </summary>
        /// <returns>A stream of successful result values</returns>
        public IEnumerable<TResult> EnumerateSuccess()
        {
            while (true)
            {
                lock (_enumerable)
                {
                    if (!_enumerable.MoveNext()) break;

                    lock (_resultList)
                    {
                        while (_resultList.Count > 0)
                        {
                            var cacheResult = _resultList.Dequeue();
                            yield return cacheResult;
                        }
                    }

                    switch (_enumerable.Current)
                    {
                        case Err<TResult, TError> err:
                            lock (_errorList)
                                _errorList.Enqueue(err.Error);
                            break;
                        case Ok<TResult, TError> ok:
                            yield return ok.Value;
                            break;
                    }
                }
            }

            lock (_resultList)
            {
                while (_resultList.Count > 0)
                {
                    var cacheResult = _resultList.Dequeue();
                    yield return cacheResult;
                }
            }
        }

        /// <summary>
        /// Enumerate the unsuccessful results, caching the successes to be enumerated later
        /// </summary>
        /// <returns>A stream of error values</returns>
        public IEnumerable<TError> EnumerateFailures()
        {
            while (true)
            {
                lock (_enumerable)
                {
                    if (!_enumerable.MoveNext()) break;

                    lock (_errorList)
                    {
                        while (_errorList.Count > 0)
                        {
                            var cacheResult = _errorList.Dequeue();
                            yield return cacheResult;
                        }
                    }

                    switch (_enumerable.Current)
                    {
                        case Err<TResult, TError> err:
                            yield return err.Error;
                            break;
                        case Ok<TResult, TError> ok:
                            lock (_resultList)
                                _resultList.Enqueue(ok.Value);
                            break;
                    }
                }
            }

            lock (_errorList)
            {
                while (_errorList.Count > 0)
                {
                    var cacheResult = _errorList.Dequeue();
                    yield return cacheResult;
                }
            }
        }

        public void Dispose()
        {
            _enumerable.Dispose();
        }
    }
}
