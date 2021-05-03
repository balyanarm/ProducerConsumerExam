using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProducerConsumerExam.Common;
using ProducerConsumerExam.Data.Models;
using ProducerConsumerExam.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ProducerConsumerExam.Producer
{
    class Program
    {
        private static bool _createTask = false;
        private static System.Threading.Tasks.Task _currentTask;
        static void Main(string[] args)
        {
            DbHelper.MigrateDB();

            MainAsync().GetAwaiter().GetResult();
        }

        static async System.Threading.Tasks.Task MainAsync()
        {
            var action = GetAction();

            while (action != "q")
            {
                switch (action)
                {
                    case "x":
                        if (_createTask)
                        {
                            _createTask = false;
                            await _currentTask;
                        }
                        break;
                    case "c":
                        if (!_createTask)
                        {
                            _createTask = true;
                            _currentTask = StartProcessingAsync();
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown action, Please try again");
                        break;
                }
                action = GetAction();
            }
            if (_createTask)
            {
                _createTask = false;
                await _currentTask;
            }

        }

        static System.Threading.Tasks.Task StartProcessingAsync()
        {
            return System.Threading.Tasks.Task.Run(StartProccesing);
        }

        static void StartProccesing()
        {
            using (var uw = new UnitOfWork(new Data.TaskContext()))
            {
                while (_createTask)
                {
                    uw.Tasks.Add(new Task()
                    {
                        ConsumerID = null,
                        CreationTime = DateTime.UtcNow,
                        Status = Data.Enums.TaskStatus.Pending,
                        TaskText = "Task Text for Consumer"
                    });
                    uw.Complete();
                    Thread.Sleep(100);
                    Console.WriteLine("New Task Created");
                }

            }

        }

        static void CallMethods()
        {
            using (var uw = new UnitOfWork(new Data.TaskContext()))
            {
                var a = uw.Tasks.GetLastestTasks(new List<int?>() { 1, 4, 5 });
                var b = uw.Tasks.TasksCountByStatus();
                var c = uw.Tasks.AvgTime();
                var d = uw.Tasks.ErrorPercent();
            }
        }

        static string GetAction()
        { 
            string message =
                @"x - to stop processing
q - to quit
c - to continue processing";
            Console.WriteLine("\n" + message);

            return Console.ReadKey().KeyChar.ToString();
        }
    }
}
