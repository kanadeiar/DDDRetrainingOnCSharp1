using Kanadeiar.Common;
using System.Runtime.CompilerServices;
using Task1.QuestionnaireSub.DataAccessLayer.Data;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;

[assembly: InternalsVisibleTo("Task1TransactionScript.QuestionnaireSub.Tests.EndToEnd")]
namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule;

public class Questionnaire(int id, string? surName, string? name, int age, int height, int weight)
{
    private readonly string _surName = surName!.Require(surName!.Length is > 3 and < 90, () =>
        throw new ApplicationException("Фамилия должна быть длинной от 3 до 90 символов"));
    private readonly string _name = name!.Require(name!.Length is > 3 and < 90, () =>
        throw new ApplicationException("Имя должно быть длинной от 3 до 90 символов"));
    private readonly int _age = age.Require(age is > 1 and < 100, () =>
        throw new ApplicationException("Возраст должен быть от 1 до 100 лет"));
    private readonly int _height = height.Require(height is > 50 and < 200, () =>
        throw new ApplicationException("Рост должнен быть от 50 до 200 см"));
    private readonly int _weight = weight.Require(weight is > 10 and < 200, () =>
        throw new ApplicationException("Вес должен быть от 10 до 200 кг"));

    public IEnumerable<(string message, string text)> GetFormattedTexts()
    {
        var source = new FormatCodesSource();

        foreach (var each in source.GetVariants())
        {
            var code = FormatCode.Create(each.code);

            yield return (each.message, text: code.FormatText(deconstruct()));
        }
    }

    internal (int, string, string, int, int, int) deconstruct() => (id, _surName, _name, _age, _height, _weight);
}