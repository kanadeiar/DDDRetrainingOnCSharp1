using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application;
using Task1.QuestionnaireSub.Application.Scripts;
using Task1.QuestionnaireSub.Infra.Adapters;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета». Модель предметной области.", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

DeveloperScript.RunExample();
var storage = new QuestionnairesStorage();
var script = new QuestionnaireScript(storage);

var id = script.CreateQuestionnaireFromConsole()
    .Throw(fail => throw new ApplicationException(fail.Error));

script.PrintToConsole(id)
    .Throw(fail => throw new ApplicationException(fail.Error));


ConsoleHelper.PrintFooter();
