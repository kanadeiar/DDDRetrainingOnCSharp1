using System.Diagnostics;

namespace Kanadeiar.Common;

public static class Contracts
{
    [DebuggerHidden]
    public static T Require<T>(this T on, bool check)
    {
        if (check == false) throw new ArgumentException($"Контроль корректности аргумента не пройден. Тип: " + nameof(T));

        return on;
    }

    [DebuggerHidden]
    public static T RequireNot<T>(this T on, bool check)
    {
        if (check) throw new ArgumentException($"Контроль корректности аргумента не пройден. Тип: " + nameof(T));

        return on;
    }

    [DebuggerHidden]
    public static T Require<T>(this T on, bool check, Action exceptionFunc)
    {
        if (check == false) exceptionFunc();

        return on;
    }

    [DebuggerHidden]
    public static T RequireNot<T>(this T on, bool check, Action exceptionFunc)
    {
        if (check) exceptionFunc();

        return on;
    }
}