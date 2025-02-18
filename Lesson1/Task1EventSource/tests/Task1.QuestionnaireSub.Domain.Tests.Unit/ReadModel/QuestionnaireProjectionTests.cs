using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Task1.QuestionnaireSub.Domain.Model.Values;
using Task1.QuestionnaireSub.Domain.Model.Events;
using Task1.QuestionnaireSub.Domain.ReadModel;

namespace Task1.QuestionnaireSub.Domain.Tests.Unit.ReadModel;

public class QuestionnaireProjectionTests
{
    [Theory(DisplayName = "Тестирование форматирования текста")]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestFormatText(string surName, string name, int age, int height, int weight)
    {
        var expectedId = Guid.NewGuid();
        var ev = new QuestionnaireCreated(new QuestionnaireId(expectedId), new QuestionnaireName(surName, name),
            new AgeValue(age), new HeightValue(height), new WeightValue(weight));
        
        var projection = new QuestionnaireProjection(ev);

        projection.GluedLine.Should().Be("Тестов Тест 60 лет 150 см 80 кг");
        projection.Formatted.Should().Be("Тестов Тест 60 лет 150 см 80 кг");
        projection.Interpolated.Should().Be("Тестов Тест 60 лет 150 см 80 кг");
    }
}