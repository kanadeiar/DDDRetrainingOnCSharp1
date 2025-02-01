using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Kanadeiar.Common;
using Kanadeiar.Tests;
using Moq;
using RearmCSharp1L1T1.Questionnaire.PresentationLayer.Abstractions;
using Task1.QuestionnaireSub.DataAccessLayer.Data;
using Task1.QuestionnaireSub.MainLogicLayer;
using Task1.QuestionnaireSub.PresentationLayer.Scripts;

namespace Task1TransactionScript.QuestionnaireSub.Tests.EndToEnd.PresentationLayer.Scripts;

public class QuestionnaireScriptTests
{
    /// <summary>
    /// Оптимистичный сценарий:
    /// Успешное заполнение анкеты через ввод данных с консоли.
    /// Успешный вывод данных анкеты в консоль тремя разными способами.
    /// </summary>
    [Theory]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestInputAndPrint(string surName, string name, int age, int height, int weight, Mock<IConsole> mock)
    {
        mock.SetupSequence(x => x.ReadLine())
            .Returns(surName).Returns(name).Returns(age.ToString).Returns(height.ToString).Returns(weight.ToString);
        ConsoleHelper.console = mock.Object;
        var storage = new QuestionnaireStorage();
        var sut = new QuestionnaireScript(storage);

        //Успешный ввод данных из консоли.
        var actualId = sut.InputFromConsole()
            .TryGetValue(_ => throw new ApplicationException());

        var expected = storage.Load(actualId);
        expected.SurName.Should().Be(surName);
        expected.Name.Should().Be(name);
        expected.Age.Should().Be(age);
        expected.Height.Should().Be(height);
        expected.Weight.Should().Be(weight);

        var result = sut.PrintToConsole(actualId);

        result.Should().BeOfType<Result>();
        mock.Verify(x => x.WriteLine("Склеивание:"));
        mock.Verify(x => x.WriteLine("Форматирование:"));
        mock.Verify(x => x.WriteLine("Интерполяция:"));
        mock.Verify(x => x.WriteLine($"{expected.SurName} {expected.Name} {expected.Age} лет {expected.Height} см {expected.Weight} кг"), Times.Exactly(3));
    }

    /// <summary>
    /// Пессимистичный сценарий:
    /// Неудачный ввод данных из консоли.
    /// </summary>
    [Theory]
    [AutoMoqData]
    public void TestInputFromConsole_WhenError(Mock<IConsole> mock)
    {
        mock.Setup(x => x.ReadLine())
            .Throws(new IOException());
        ConsoleHelper.console = mock.Object;
        var storage = new QuestionnaireStorage();
        var sut = new QuestionnaireScript(storage);

        var actual = sut.InputFromConsole();

        actual.Should().BeOfType<Fail<int>>();
    }

    /// <summary>
    /// Пессимистичный сценарий:
    /// Неудачный вывод данных в консоль.
    /// </summary>
    [Theory]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestPrintToConsole_WhenError(string surName, string name, int age, int height, int weight, Mock<IConsole> mock)
    {
        var expectedId = 1;
        var questionnaire = QuestionnaireFactory.Create(expectedId, surName, name, age, height, weight);
        mock.Setup(x => x.WriteLine(It.IsAny<string>()))
            .Throws(new IOException());
        ConsoleHelper.console = mock.Object;
        var storage = new QuestionnaireStorage();
        storage.Save(questionnaire.Deconstruct());
        var sut = new QuestionnaireScript(storage);

        var actual = sut.PrintToConsole(expectedId);

        actual.Should().BeOfType<Fail>();
    }
}