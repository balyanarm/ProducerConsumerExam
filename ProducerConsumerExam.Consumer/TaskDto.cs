using ProducerConsumerExam.Data.Enums;
using System;

namespace ProducerConsumerExam.Consumer
{
    public class TaskDto
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public string TaskText { get; set; }
        public TaskStatus Status { get; set; }
        public int? ConsumerId { get; set; }
    }
}
