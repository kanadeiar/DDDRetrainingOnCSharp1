using System.Runtime.CompilerServices;
using Kanadeiar.Common;
using Task1.Data;
using Task1.QuestionnaireModule;

[assembly: InternalsVisibleTo("Task1.Tests.EndToEnd")]
namespace Task1.Services;

public class QuestionnaireService
{
    public Option<Questionnaire> InputFromConsole()
    {
        try
        {
            var surName = ConsoleHelper.ReadLineFromConsole("Введите фамилию")!;
            var name = ConsoleHelper.ReadLineFromConsole("Введите имя")!;
            var age = ConsoleHelper.ReadNumberFromConsole<int>("Введите возраст");
            var height = ConsoleHelper.ReadNumberFromConsole<int>("Введите рост");
            var weight = ConsoleHelper.ReadNumberFromConsole<int>("Введите вес");

            var result = new Questionnaire(surName, name, age, height, weight);

            return Option.Some(result);
        }
        catch (Exception e)
        {
            return Option.None<Questionnaire>("Не удалось получить анкету с консоли. Ошибка: " + e);
        }
    }

    public Option PrintToConsole(Questionnaire questionnaire)
    {
        try
        {
            var source = new FormatCodesSource();

            foreach (var each in source.GetVariants())
            {
                var text = questionnaire.GetFormattedText(each.Item2);

                ConsoleHelper.PrintValueWithMessage(each.Item1, text);
            }

            return Option.Some();
        }
        catch (Exception e)
        {
            return Option.None("Не удалось распечатать результаты в консоли. Ошибка: " + e);
        }
    }
}