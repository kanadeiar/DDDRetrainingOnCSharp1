using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Kanadeiar.Common;
using Task1.QuestionnaireSub.DataAccessLayer;
using Task1.QuestionnaireSub.DataAccessLayer.Contracts;
using Task1.QuestionnaireSub.MainLogicLayer.QuestionnaireModule.Models;
using Task1.QuestionnaireSub.ServiceLayer.Services;

namespace Test1.QuestionnaireSub.Tests.Integration.ServiceLayer.Services;

public class QuestionnaireServiceTests
{
    public QuestionnaireServiceTests()
    {
        Registry.InitFake(new FakeRegistry());
    }

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

    [Theory(DisplayName = "Тестирование нарушения инвариантов анкеты")]
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

        var actuals = sut.FormatTexts(expected.Id)
            .Throw(() => new ApplicationException());

        actuals.Texts.Should().HaveCount(3);
        actuals.Texts.First().VariantMessage.Should().Be("Склеивание");
        actuals.Texts.Skip(1).First().VariantMessage.Should().Be("Форматирование");
        actuals.Texts.Last().VariantMessage.Should().Be("Интерполяция");
        actuals.Texts.First().Message.Should().Be($"{expected.SurName} {expected.Name} {expected.Age} лет {expected.Height} см {expected.Weight} кг");
        actuals.Texts.Skip(1).First().Message.Should().Be($"{expected.SurName} {expected.Name} {expected.Age} лет {expected.Height} см {expected.Weight} кг");
        actuals.Texts.Last().Message.Should().Be($"{expected.SurName} {expected.Name} {expected.Age} лет {expected.Height} см {expected.Weight} кг");
    }

    [Fact(DisplayName = "Тестирование обработки ошибки при создания текстовых даных из несуществующей анкеты")]
    public void TestFormattedTexts_WhenNotFoundError()
    {
        var sut = new QuestionnaireService();

        var result = sut.FormatTexts(1);

        result.Should().BeOfType<Fail<FormattedTexts>>();
    }
}