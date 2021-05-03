using ProducerConsumerExam.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerExam.Consumer
{
    public static class TaskProfile
    {
        public static TaskDto ToDto(this Task entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new TaskDto
            {
                Id = entity.Id,
                CreationTime = entity.CreationTime,
                ModificationTime = entity.ModificationTime,
                TaskText = entity.TaskText,
                Status = entity.Status,
                ConsumerID = entity.ConsumerID,
            };
        }
    }
}
