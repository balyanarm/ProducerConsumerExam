using Microsoft.EntityFrameworkCore;
using ProducerConsumerExam.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProducerConsumerExam.Common
{
    public static class DbHelper
    {
        private readonly static string DbFile = Path.Combine("../../../../", "ProducerConsumerTasks.db");

        public static bool DbExists()
        {
            return File.Exists(DbFile);
        }

        public static void MigrateDB()
        {
            if (DbExists())
            {
                File.Delete(DbFile);
            }

            using (var dbContext = new TaskContext())
            {
                dbContext.Database.EnsureCreated();
               // dbContext.Database.Migrate();
            }
        }
    }
}
