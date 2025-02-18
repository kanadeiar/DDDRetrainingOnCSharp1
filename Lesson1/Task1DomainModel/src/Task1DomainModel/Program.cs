using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application;
using Task1.QuestionnaireSub.Application.QuestionnaireFeature;
using Task1.QuestionnaireSub.Infra.Adapters;
using Task1.QuestionnaireSub.Infra.Tools;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета». Модель предметной области.", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

var storage = new QuestionnairesStorage();
var dispatcher = new DomainEventDispatcher();
var script = new QuestionnaireScript(storage, dispatcher);
DeveloperScript.RunExample(dispatcher);
dispatcher.Run();

var id = script.CreateQuestionnaireFromConsole()
    .Throw(fail => throw new ApplicationException(fail.Error));

script.PrintToConsole(id)
    .Throw(fail => throw new ApplicationException(fail.Error));


ConsoleHelper.PrintFooter();
