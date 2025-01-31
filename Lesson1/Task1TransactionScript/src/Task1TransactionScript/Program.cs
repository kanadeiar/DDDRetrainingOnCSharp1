using Kanadeiar.Common;
using Task1.QuestionnaireSub.PresentationLayer.Scripts;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета». Транзакционный сценарий.", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

var script = new QuestionnaireScript();

var questionnaire = script.InputFromConsole()
    .Throw(fail => new ApplicationException(fail.Error));

script.PrintToConsole(questionnaire)
    .Throw(fail => new ApplicationException(fail.Error));

ConsoleHelper.PrintFooter();
