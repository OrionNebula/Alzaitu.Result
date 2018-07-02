using System;
using Xunit;
using Alzaitu.Result;

namespace Alzaitu.Result.Tests
{
    public class UnitTests
    {
        [Fact]
        public void TestGood()
        {
            switch (GoodMethod())
            {
                case Ok<string, string> ok:
                    Assert.True(ok.Value.Equals("Hello there!"), "Result should be the correct value");
                    break;
                case Err<string, string> _:
                    Assert.True(false, "The good method should not error");
                    break;
                default:
                    Assert.True(false, "Result must match one of the two");
                    break;
            }
        }

        [Fact]
        public void TestBad()
        {
            switch (BadMethod())
            {
                case Ok<string, string> _:
                    Assert.True(false, "The bad method should not succeed");
                    break;
                case Err<string, string> err:
                    Assert.True(err.Error.Equals("General Kenobi!"), "Error should be the correct value");
                    break;
                default:
                    Assert.True(false, "Result must match one of the two");
                    break;
            }
        }

        private IResult<string, string> GoodMethod()
        {
            return new Ok<string, string>("Hello there!");
        }

        private IResult<string, string> BadMethod()
        {
            return new Err<string, string>("General Kenobi!");
        }
    }
}
