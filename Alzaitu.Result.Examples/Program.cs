using System;
using System.Linq;

namespace Alzaitu.Result.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var res = Enumerable.Range(0, 5).Select(x => Parse.ParseInt(Console.ReadLine())).ToList()
                .ToResultEnumerable())
            {
                foreach (var i in res.EnumerateSuccess())
                {
                    Console.WriteLine("{0} is a real integer", i);
                }

                foreach (var err in res.EnumerateFailures())
                {
                    Console.WriteLine("\"{0}\" is not a real integer", err.OriginalString);
                }
            }

            var num = new Random().Next(1, 11);

            while (true)
            {
                Console.Write("Enter a number: ");

                switch (Parse.ParseInt(Console.ReadLine()))
                {
                    case Err<int, ParseError> err:
                        Console.WriteLine(@"""{0}"" isn't a number!", err.Error.OriginalString);
                        break;
                    case Ok<int, ParseError> ok:
                        switch (GuessNumber(num, ok))
                        {
                            case Err<string> err:
                                Console.WriteLine(err.Error);
                                break;
                            case Ok<string> _:
                                Console.WriteLine("Nice job!");
                                return;
                        }
                        break;
                }
            }
        }

        private static Result<string> GuessNumber(int real, int guess)
        {
            if (guess > real)
                return new Err<string>("Too high, try again!");
            if (guess < real)
                return new Err<string>("Too low, try again!");
            return new Ok<string>();
        }
    }
}
