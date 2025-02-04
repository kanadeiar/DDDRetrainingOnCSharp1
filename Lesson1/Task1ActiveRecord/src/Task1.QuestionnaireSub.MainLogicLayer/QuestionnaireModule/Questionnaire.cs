using Kanadeiar.Common;
using Task1.QuestionnaireSub.DataAccessLayer;
using Task1.QuestionnaireSub.DataAccessLayer.Contracts;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Formatting;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Models;

namespace Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule;

public class Questionnaire(int recordId, string? surName, string? name, int age, int height, int weight)
{
    public int RecordId => recordId;

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
    
    public static Questionnaire Create(string? surName, string? name, int age, int height, int weight)
    {
        var id = Registry.Storage.NextIdentity();
        var result = new Questionnaire(id, surName, name, age, height, weight);

        return result;
    }

    public static Result<Questionnaire> Find(int id)
    {
        try
        {
            var entry = Registry.Storage.Load(id);
            if (entry is null) return Result.Fail<Questionnaire>($"Анкета с идентификатором {id} не найдена");

            var result = new Questionnaire(entry.Id,
                entry.SurName,
                entry.Name,
                entry.Age,
                entry.Height,
                entry.Weight);

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Fail<Questionnaire>("Не удалось найти анкету в базе данных. Ошибка: " + e);
        }
    }

    public Result Add()
    {
        try
        {
            var entity = new QuestionnaireEntry
            {
                Id = RecordId,
                SurName = surName,
                Name = name,
                Age = age,
                Height = height,
                Weight = weight,
            };

            Registry.Storage.Save(entity);

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail("Не удалось добавить анкету в хранилище. Ошибка: " + e);
        }
    }

    public FormattedTexts FormatTexts()
    {
        var texts = FormatCode.AllCodes().Select(c => c.FormatText(this));

        return new FormattedTexts(texts);
    }

    internal (int, string, string, int, int, int) deconstruct() => (RecordId, _surName, _name, _age, _height, _weight);
}