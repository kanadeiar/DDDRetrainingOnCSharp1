using Kanadeiar.Common;
using Task1.Services;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета».", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

var service = new QuestionnaireService();

var questionnaire = service.InputFromConsole()
    .Throw(none => throw new ApplicationException(none.Message));

service.PrintToConsole(questionnaire)
    .Throw(none => throw new ApplicationException(none.Message));

ConsoleHelper.PrintFooter();
