using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Kanadeiar.Common;
using Task1.QuestionnaireSub.DataAccessLayer;
using Task1.QuestionnaireSub.DataAccessLayer.Contracts;
using Task1.QuestionnaireSub.DataAccessLayer.Data;
using Task1.QuestionnaireSub.ServiceLayer.Services;

namespace Test1.QuestionnaireSub.Tests.Integration.ServiceLayer.Services;

public class QuestionnaireServiceTests
{
    [Theory(DisplayName = "Тестирование создания новой анкеты в репозитории")]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestCreateNewQuestionnaire(string surName, string name, int age, int height, int weight)
    {
        var sut = new QuestionnaireService();

        var actualId = sut.CreateNewQuestionnaire(surName, name, age, height, weight)
            .Throw(() => new ApplicationException());

        var actual = Registry.Storage.Load(actualId);
        actual.SurName.Should().Be(surName);
        actual.Name.Should().Be(name);
        actual.Age.Should().Be(age);
        actual.Height.Should().Be(height);
        actual.Weight.Should().Be(weight);
    }

    [Theory(DisplayName = "Тестирование инвариантов анкеты")]
    [InlineAutoMoqData("Т", "Тест", 60, 150, 80)]
    [InlineAutoMoqData("Тестов", "Т", 60, 150, 80)]
    [InlineAutoMoqData("Тестов", "Тест", -1, 150, 80)]
    [InlineAutoMoqData("Тестов", "Тест", 60, -1, 80)]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, -1)]
    public void TestCreateNewQuestionnaire_WhenInvariantError(string surName, string name, int age, int height, int weight)
    {
        var sut = new QuestionnaireService();

        var result = sut.CreateNewQuestionnaire(surName, name, age, height, weight);

        result.Should().BeOfType<Fail<int>>();
    }

    [Theory(DisplayName = "Тестирование создания текстовых даных из анкеты")]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestFormattedTexts(string surName, string name, int age, int height, int weight)
    {
        var expected = new QuestionnaireEntry
        { Id = Registry.Storage.NextIdentity(), SurName = surName, Name = name, Age = age, Height = height, Weight = weight };
        Registry.Storage.Save(expected);
        var sut = new QuestionnaireService();

        var actuals = sut.FormattedTexts();

        actuals.Should().HaveCount(3);

    }
}