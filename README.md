# Alzaitu.Result

[![Build Status](https://travis-ci.org/OrionNebula/Alzaitu.Result.svg?branch=master)](https://travis-ci.org/OrionNebula/Alzaitu.Result)

Rust-style error handling in C# using pattern matching.

An improvement over output parameters and exceptions. Reserve exceptions for cases that are truly exceptional, and make sure the user acknowledges any errors before they can use the result value.

## Example

```cs
public Result<string, Error> SomeMethod()
{
    // logic
    if (!error)
        return new Ok<string, Error>(someResult);
    else
        return new Err<string, Error>(new Error("Bad things happened"));
}

public void Consume()
{
    switch (SomeMethod())
    {
        case Ok<string, Error> ok:
            Console.WriteLine(ok.Value);
            break;
        case Err<string, Error> err:
            // Panic!
            break;
    }
}
```