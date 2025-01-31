using FluentAssertions;
using FrameworkConsoleApp1Tests.Infrastructure;
using Kanadeiar.Common;
using Kanadeiar.Tests;
using Moq;
using RearmCSharp1L1T1.Questionnaire.PresentationLayer.Abstractions;
using Task1.QuestionnaireModule;
using Task1.Services;

namespace Task1.Tests.EndToEnd.Services;

public class QuestionnaireServiceTests
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
        var sut = new QuestionnaireService();

        //Успешный ввод данных из консоли.
        var actual = sut.InputFromConsole()
            .TryGetValue(x => throw new ApplicationException());

        var values = actual.deconstruct();
        values.Item1.Should().Be(surName);
        values.Item2.Should().Be(name);
        values.Item3.Should().Be(age);
        values.Item4.Should().Be(height);
        values.Item5.Should().Be(weight);

        //Успешный вывод результатов в консоль.
        var expecteds = actual.deconstruct();

        var result = sut.PrintToConsole(actual);
        
        result.Should().BeOfType<Option>();
        mock.Verify(x => x.WriteLine("Склеивание:"));
        mock.Verify(x => x.WriteLine("Форматирование:"));
        mock.Verify(x => x.WriteLine("Интерполяция:"));
        mock.Verify(x => x.WriteLine($"{expecteds.Item1} {expecteds.Item2} {expecteds.Item3} лет {expecteds.Item4} см {expecteds.Item5} кг"), Times.Exactly(3));
    }

    /// <summary>
    /// Негативный сценарий:
    /// Неудачный ввод анкеты через ввод данных с консоли.
    /// </summary>
    [Theory]
    [AutoMoqData]
    public void TestInputFromConsole_WhenError_ThenNone(Mock<IConsole> mock)
    {
        mock.Setup(x => x.ReadLine())
            .Returns(() => throw new IOException());
        ConsoleHelper.console = mock.Object;
        var sut = new QuestionnaireService();

        var actual = sut.InputFromConsole();

        actual.Should().BeOfType<None<Questionnaire>>();
    }

    /// <summary>
    /// Негативный сценарий:
    /// Неудачный вывод данных в консоль.
    /// </summary>
    [Theory]
    [InlineAutoMoqData("Тестов", "Тест", 60, 150, 80)]
    public void TestPrintToConsole_WhenError_ThenNone(string surName, string name, int age, int height, int weight, Mock<IConsole> mock)
    {
        var questionnaire = new Questionnaire(surName, name, age, height, weight);
        mock.Setup(x => x.WriteLine(It.IsAny<string>()))
            .Throws(new IOException());
        ConsoleHelper.console = mock.Object;
        var sut = new QuestionnaireService();

        var actual = sut.PrintToConsole(questionnaire);

        actual.Should().BeOfType<None>();
    }
}