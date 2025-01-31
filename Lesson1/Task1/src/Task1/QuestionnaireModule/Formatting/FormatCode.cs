namespace Task1.QuestionnaireModule.Formatting;

public abstract record FormatCode
{
    public static FormatCode GluedLine => new GluedLineCode();
    public static FormatCode Formatted => new FormattedCode();
    public static FormatCode Interpolated => new InterpolatedCode();

    public abstract string FormatText(string surName, string name, int age, int height, int weight);
    
    private record GluedLineCode : FormatCode
    {
        public override string FormatText(string surName, string name, int age, int height, int weight) => 
            surName + " " + name + " " + age + " лет " + height + " см " + weight + " кг";
    }

    private record FormattedCode : FormatCode
    {
        public override string FormatText(string surName, string name, int age, int height, int weight) => 
            string.Format("{0} {1} {2} лет {3} см {4} кг", surName, name, age, height, weight);
    }

    private record InterpolatedCode : FormatCode
    {
        public override string FormatText(string surName, string name, int age, int height, int weight) => 
            $"{surName} {name} {age} лет {height} см {weight} кг";
    }
}