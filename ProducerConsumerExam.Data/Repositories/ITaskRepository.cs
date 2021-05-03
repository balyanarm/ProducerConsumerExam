using ProducerConsumerExam.Data.Enums;
using ProducerConsumerExam.Data.Models;
using System;
using System.Collections.Generic;

namespace ProducerConsumerExam.Data.Repositories
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetLastestTasks(List<int?> CustomerIDs);

        Dictionary<TaskStatus, int> TasksCountByStatus();

        TimeSpan AvgTime();

        double ErrorPercent();

        IEnumerable<Task> GetNewTasks(int count);

        bool AnyPendingTask();
    }
}
