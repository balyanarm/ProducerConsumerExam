using Microsoft.EntityFrameworkCore;
using ProducerConsumerExam.Data.Enums;
using ProducerConsumerExam.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProducerConsumerExam.Data.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(DbContext context) : base(context)
        {

        }

        public TaskContext TaskContext
        {
            get { return Context as TaskContext; }
        }

        public TimeSpan AvgTime()
        {
            var avgTicks = TaskContext.Tasks.Where(t => t.Status == TaskStatus.Done)
                 .Select(t => t.ModificationTime - t.CreationTime)
                 .ToList();
            if (avgTicks.Count != 0)
                return new TimeSpan(Convert.ToInt64(avgTicks.Average(t => t.Ticks)));
            else
                return TimeSpan.Zero;

        }

        public double ErrorPercent()
        {
            var totalCount = GetAll().Count();
            if (totalCount != 0)
                return TaskContext.Tasks.Where(t => t.Status == TaskStatus.Error).Count() * 100 / totalCount;
            else
                return 0;
        }

        public IEnumerable<Task> GetNewTasks(int count)
        {
            return TaskContext.Tasks.Where(t => t.Status == TaskStatus.Pending)
                .Take(count)
                .ToList();
        }

        public Dictionary<TaskStatus, int> TasksCountByStatus()
        {
            return GetAll().GroupBy(t => t.Status)
              .ToDictionary(g => g.Key, g => g.Count());
        }

        public IEnumerable<Task> GetLastestTasks(List<int?> ConsumerIDs)
        {
            var selectedConsumers = TaskContext.Tasks
                .Where(t => ConsumerIDs.Contains(t.ConsumerID))
                .ToList();

            return selectedConsumers.GroupBy(s => s.ConsumerID)
                .Select(s => s.OrderByDescending(x => x.CreationTime).FirstOrDefault());
        }

        public bool AnyPendingTask() 
        {
            return TaskContext.Tasks.Any(t => t.Status == TaskStatus.Pending);
        }
    }
}
