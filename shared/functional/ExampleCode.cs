namespace Kanadeiar.Common;

public abstract record ExampleCode
{
    public Result<int> CreateWithOption(ExampleCode code)
    {
        return code.Require(code != null) switch
        {
            FirstCode => Result.Ok(1),
            SecondCode => Result.Ok(2),
            _ => Result.Fail<int>("none"),
        };
    }

    public string Create(ExampleCode code)
    {
        return code.RequireNot(code == null) switch
        {
            FirstCode => "first",
            SecondCode { id: 1 } => "number one",
            SecondCode(var id) => "second " + id,
            _ => "none",
        };
    }

    public Result ExampleAction(bool value)
    {
        return value 
            ? Result.Ok() 
            : Result.Fail("Ничего нет");
    }

    public static ExampleCode First => new FirstCode();
    public static ExampleCode Second(int id) => new SecondCode(id);

    private record FirstCode : ExampleCode;
    private record SecondCode(int id) : ExampleCode;
}