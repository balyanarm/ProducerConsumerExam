using Microsoft.EntityFrameworkCore;
using ProducerConsumerExam.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerExam.Data
{
    public class TaskContext : DbContext
    {
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=../../../../ProducerConsumerTasks.db"); 
        }

    }
}
