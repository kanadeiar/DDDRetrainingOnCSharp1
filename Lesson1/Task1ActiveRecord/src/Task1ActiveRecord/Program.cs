using Kanadeiar.Common;
using Task1.QuestionnaireSub.PresentationLayer.Scripts;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета». Активная запись.", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

var script = new QuestionnaireScript();

var id = script.CreateQuestionnaireFromConsole()
    .Throw(fail => throw new ApplicationException(fail.Error));

script.PrintToConsole(id)
    .Throw(fail => throw new ApplicationException(fail.Error));

ConsoleHelper.PrintFooter();
