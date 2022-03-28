using ProducerConsumerExam.Common;
using ProducerConsumerExam.Data.Enums;
using ProducerConsumerExam.Data.Models;
using ProducerConsumerExam.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProducerConsumerExam.Consumer
{
    public class ConsumerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ConsumerService(IUnitOfWork unitOfWork,
           ILogger logger)
        {
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<TaskDto> GetPendingTasksForConsumer(int customerId, int count)
        {
            var tasks = _unitOfWork.Tasks.Query
                .Where(t => t.Status == Data.Enums.TaskStatus.Pending)
                .Take(count)
                .ToList();

            foreach (var t in tasks) 
            {
                t.ModificationTime = DateTime.UtcNow;
                t.Status = Data.Enums.TaskStatus.InProgress;
                t.ConsumerId = customerId;
            }
            _unitOfWork.Tasks.UpdateAll(tasks);
            _unitOfWork.Complete();

            return tasks.Select(t => t.ToDto());
        }

        private void CompleteConsumerTask(int taskId) 
        {
            var task = _unitOfWork.Tasks.GetById(taskId);
            task.Status = TaskStatus.Done;

            _unitOfWork.Complete();
        }

        private void ConsumerTaskFailed(int taskId)
        {
            var task = _unitOfWork.Tasks.GetById(taskId);
            task.Status = TaskStatus.Error;

            _unitOfWork.Complete();
        }

        public void StartConsumerWork(int consumerId, IEnumerable<TaskDto> tasks)
        {
            _logger.Info($"Consumer {consumerId} gots {tasks.Count()} tasks.");

            foreach(var t in tasks)
            {
                _logger.Info($"Consumer {consumerId} working on  {t.Id}: {t.TaskText}.");

                var status = GetRandomStatus();
                switch (status) {
                    case TaskStatus.Done:
                        CompleteConsumerTask(t.Id);
                        _logger.Info($"Consumer {consumerId} compleates the task {t.Id}.");
                        break;
                    case TaskStatus.Error:
                        ConsumerTaskFailed(t.Id);
                        _logger.Warn($"Consumer {consumerId} failed the task {t.Id}.");
                        break;
                }
            }
        }

        private static Random rd = new Random();

        static TaskStatus GetRandomStatus()
        {
            var number = rd.Next(1, 100);
            return number < 80 ? TaskStatus.Done : TaskStatus.Error;
        }
    }
}
