using Kanadeiar.Common;

namespace RefactoringLab.Common;

public abstract record ExampleCode
{
    public Option<int> CreateWithOption(ExampleCode code)
    {
        return code.Require(code != null) switch
        {
            FirstCode => Option.Some(1),
            SecondCode => Option.Some(2),
            _ => Option.None<int>("none"),
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

    public Option ExampleAction(bool value)
    {
        return value 
            ? Option.Some() 
            : Option.None("Ничего нет");
    }

    public static ExampleCode First => new FirstCode();
    public static ExampleCode Second(int id) => new SecondCode(id);

    private record FirstCode : ExampleCode;
    private record SecondCode(int id) : ExampleCode;
}