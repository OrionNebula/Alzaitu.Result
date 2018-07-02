namespace Alzaitu.Result.Examples
{
    public struct ParseError
    {
        public string OriginalString;

        public ParseError(string originalString)
        {
            OriginalString = originalString;
        }
    }

    public static class Parse
    {
        public static Result<int, ParseError> ParseInt(string str)
        {
            if (!int.TryParse(str, out var result))
                return new Err<int, ParseError>(new ParseError(str));

            return new Ok<int, ParseError>(result);
        }
    }
}
