using Kanadeiar.Common;
using Task1.QuestionnaireSub.Application.QuestionnaireFunction;
using Task1EventSource;

ConsoleHelper.PrintHeader("Задача № 1. Написать программу «Анкета». Модель предметной области, основанная на событиях.", "DDDRetrainingOnCSharp1 C# Уровень 1 Лекция 1.");

var dispatcher = GeneralHelper.Dispatcher();
var storage = GeneralHelper.Storage(dispatcher);
var master = ReadModelHelper.CreateReadModel(dispatcher);

var service = new QuestionnaireApplicationService(storage, master);

service.CreateQuestionnaireFromConsole();

ConsoleHelper.Pause();

service.PrintToConsole();

ConsoleHelper.PrintFooter();




