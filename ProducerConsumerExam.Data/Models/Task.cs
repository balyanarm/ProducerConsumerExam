using ProducerConsumerExam.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProducerConsumerExam.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public string TaskText { get; set; }
        public TaskStatus Status { get; set; }
        public int? ConsumerId { get; set; }
    }
}
