using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Events;
using Task1.QuestionnaireSub.Domain.QuestionnaireAggregate.Values;

namespace Task1.QuestionnaireSub.Domain.Tests.Unit.QuestionnaireAggregate;

public class QuestionnaireTests
{
    [Theory(DisplayName = "Тестирование создания новой анкеты и события")]
    [InlineAutoMoqData(1, "Тестов", "Тест", 60, 150, 80)]
    public void TestCreate(int id, string surName, string name, int age, int height, int weight)
    {
        var actual = Questionnaire.CreateNew(new QuestionnaireId(id), new QuestionnaireNameValue(surName, name), new AgeValue(age),
            new HeightValue(height), new WeightValue(weight));

        var entry = actual.Deconstruct();
        entry.Id.Should().Be(id);
        entry.SurName.Should().Be(surName);
        entry.Name.Should().Be(name);
        entry.Age.Should().Be(age);
        entry.Height.Should().Be(height);
        entry.Weight.Should().Be(weight);
        var events = actual.TakeEvents();
        events.Count().Should().Be(1);
        var created = events.Last() as QuestionnaireCreated;
        created.Id.Should().Be(new QuestionnaireId(id));
    }

    [Theory(DisplayName = "Тестирование нарушения инвариантов анкеты")]
    [InlineAutoMoqData(0, "Тестов", "Тест", 60, 150, 80)]
    [InlineAutoMoqData(1, "Т", "Тест", 60, 150, 80)]
    [InlineAutoMoqData(1, "Тестов", "Т", 60, 150, 80)]
    [InlineAutoMoqData(1, "Тестов", "Тест", -1, 150, 80)]
    [InlineAutoMoqData(1, "Тестов", "Тест", 60, -1, 80)]
    [InlineAutoMoqData(1, "Тестов", "Тест", 60, 150, -1)]
    public void TestCreateNewQuestionnaire_WhenInvariantError(int id, string surName, string name, int age, int height, int weight)
    {
        var act = () =>
        {
            _ = Questionnaire.CreateNew(new QuestionnaireId(id), new QuestionnaireNameValue(surName, name), new AgeValue(age),
                new HeightValue(height), new WeightValue(weight));
        };

        act.Should().Throw<ApplicationException>();
    }
}