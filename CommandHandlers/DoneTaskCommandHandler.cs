namespace ToDoApp.CommandHandlers;
using ToDoApp;
using ToDoApp.CommandHandlers;
using System.Globalization;
using System.Runtime.CompilerServices;

public class DoneTasksCommandHandler(IStorage storage) : ICommandHandler
{
    private const string FilePath = "tasks.csv";
    public void HandleCommand()
    {
        System.Console.Write("Введите номер задачи: ");
        string? task_id = Console.ReadLine();

        List<TodoTask> tasks = storage.LoadTask();

        if(tasks.Count <= 0){
            System.Console.WriteLine("Список задач пуст");
            return;
        }
        
        if(tasks.Count < int.Parse(task_id))
        {
            System.Console.WriteLine("Такой задачи нет");
            return;
        }

        foreach(var task in tasks)
        {
            if(task.Id == int.Parse(task_id))
            {
                task.Done = "[Done!]";
            }
        }

        List<string> csv = new List<string>();

        foreach(var i in tasks)
        {
            string? line = $"{i.Id},{i.Title},{i.CreatedAt:yyyy-MM-dd HH:mm},{i.Done}";
            csv.Add(line);
        }

        File.WriteAllLines(FilePath, csv);
        System.Console.WriteLine("Задача Успешно Помечена.\n");
    }
}