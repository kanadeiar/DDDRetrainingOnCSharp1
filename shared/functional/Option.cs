using System.Diagnostics;

namespace Kanadeiar.Common;

public interface INone
{
    public string Message { get; }
}

public interface ISome<out T>
{
    public T Value { get; }
}

public class Option<T>(T value) : ISome<T>
{
    T ISome<T>.Value => value;
}

public class None<T>(string message) : Option<T>(default!), INone
{
    string INone.Message => message;
}

public class Option() : Option<bool>(false)
{
    public static Option<T> Some<T>(T value) => new(value);

    public static Option<T> None<T>(string message) =>
        new None<T>(message);

    public static Option Some() => new();

    public static Option None(string message) =>
        new None(message);
}

public class None(string message) : Option, INone
{
    string INone.Message => message;
}

public static class OptionSupport
{
    public static T TryGetValue<T>(this Option<T> option, Func<INone, T> noneFunc)
    {
        return option switch
        {
            INone none => noneFunc(none),
            ISome<T> some => some.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };
    }

    public static T TryGetValue<T>(this Option<T> option, Func<T> noneFunc)
    {
        return option switch
        {
            INone _ => noneFunc(),
            ISome<T> some => some.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };
    }

    public static T Throw<T>(this Option<T> option, Func<INone, Exception> exceptionFunc)
    {
        return option switch
        {
            INone none => throw exceptionFunc(none),
            ISome<T> some => some.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };
    }

    public static T Throw<T>(this Option<T> option, Func<Exception> exceptionFunc)
    {
        return option switch
        {
            INone _ => throw exceptionFunc(),
            ISome<T> some => some.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };
    }
}