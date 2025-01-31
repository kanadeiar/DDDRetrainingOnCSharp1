using Kanadeiar.Common;
using Task1.QuestionnaireModule.Formatting;

namespace Task1.QuestionnaireModule;

public class Questionnaire(string surName, string name, int age, int height, int weight)
{
    private readonly string _surName = surName.Require(surName.Length is > 3 and < 90, () =>
        throw new ApplicationException("Фамилия должна быть длинной от 3 до 90 символов"));
    private readonly string _name = name.Require(name.Length is > 3 and < 90, () =>
        throw new ApplicationException("Имя должно быть длинной от 3 до 90 символов"));
    private readonly int _age = age.Require(age is > 1 and < 100, () =>
        throw new ApplicationException("Возраст должен быть от 1 до 100 лет"));
    private readonly int _height = height.Require(height is > 50 and < 200, () =>
        throw new ApplicationException("Рост должнен быть от 50 до 200 см"));
    private readonly int _weight = weight.Require(weight is > 10 and < 200, () =>
        throw new ApplicationException("Вес должен быть от 10 до 200 кг"));

    public string GetFormattedText(FormatCode code) => code.FormatText(_surName, _name, _age, _height, _weight);

    internal (string, string, int, int, int) deconstruct() => (_surName, _name, _age, _height, _weight);
}