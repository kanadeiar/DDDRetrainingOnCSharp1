using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Task1.QuestionnaireSub.Domain.Model;
using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.Model.Values;

namespace Task1.QuestionnaireSub.Domain.Tests.Unit.Model;

public class QuestionnaireItemTests
{
    [Theory(DisplayName = "Тестирование события создания новой анкеты")]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestCreate(string surName, string name, int age, int height, int weight)
    {
        var expectedId = Guid.NewGuid();

        var sut = new QuestionnaireItem(new QuestionnaireId(expectedId), new QuestionnaireName(surName, name), new AgeValue(age), new HeightValue(height), new WeightValue(weight));

        sut.AggregateId.Id.Should().Be(expectedId);
        sut.Version.Should().Be(-1);
        var changes = sut.Changes();
        changes.Count.Should().Be(1);
        var last = (QuestionnaireCreated)changes.Last();
        last.Id.Id.Should().Be(expectedId);
        last.Name.SurName.Should().Be(surName);
        last.Name.Name.Should().Be(name);
        last.Age.Age.Should().Be(age);
        last.Height.Height.Should().Be(height);
        last.Weight.Weight.Should().Be(weight);
    }

    [Theory(DisplayName = "Тестирование нарушения инвариантов анкеты")]
    [InlineAutoMoqData("Т", "Тест", 60, 150, 80)]
    [InlineAutoMoqData("Тестов", "Т", 60, 150, 80)]
    [InlineAutoMoqData("Тестов", "Тест", -1, 150, 80)]
    [InlineAutoMoqData("Тестов", "Тест", 60, -1, 80)]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, -1)]
    public void TestCreateNewQuestionnaire_WhenInvariantError(string surName, string name, int age, int height, int weight)
    {
        var act = () =>
        {
            _ = new QuestionnaireItem(new QuestionnaireId(Guid.NewGuid()), new QuestionnaireName(surName, name), new AgeValue(age),
                new HeightValue(height), new WeightValue(weight));
        };

        act.Should().Throw<ApplicationException>();
    }
}