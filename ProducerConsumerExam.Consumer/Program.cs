using ProducerConsumerExam.Common;
using ProducerConsumerExam.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerExam.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumersCount = InputValue("Please insert valid integer for consumers count - ");
            var taskBulkSizes = InputValue("Please insert valid integer for task bulk sizes - ");

            while (!DbHelper.DbExists())
            {
                Console.WriteLine("Waiting for creating DataBase");
                Thread.Sleep(1000);
            }

            StartProcessingAsync(consumersCount, taskBulkSizes)
                .GetAwaiter()
                .GetResult();

            Console.ReadLine();
        }

        static int InputValue(string message)
        {
            string line;
            int value;
            do
            {
                Console.WriteLine(message);
                line = Console.ReadLine();
            }
            while (int.TryParse(line, out value) != true);
            return value;
        }

        static async Task StartProcessingAsync(int consumersCount, int tasksCount)
        {
            ILogger logger = new ConsoleLogger();
            var anyPendingTask = true;
            while (anyPendingTask)
            {
                var tasks = new Task[consumersCount];

                for (var i = 1; i <= consumersCount; i++)
                {
                    var consumerId = i;
                    tasks[consumerId-1] = Task.Run(() =>
                    {
                        using (var uw = new UnitOfWork(new Data.TaskContext()))
                        {
                            ConsumerService consumerService = new ConsumerService(uw, logger);
                            var taskDtos = consumerService.GetPendingTasksForConsumer(consumerId, tasksCount);
                            consumerService.StartConsumerWork(consumerId, taskDtos);
                            Thread.Sleep(100);
                        }
                    });
                }
                await Task.WhenAll(tasks);
                using (var uw = new UnitOfWork(new Data.TaskContext()))
                {
                    anyPendingTask = uw.Tasks.AnyPendingTask();
                    while(!anyPendingTask)
                    {
                        Thread.Sleep(2000);
                        anyPendingTask = uw.Tasks.AnyPendingTask();
                    }
                }
            }
        }
    }

}

