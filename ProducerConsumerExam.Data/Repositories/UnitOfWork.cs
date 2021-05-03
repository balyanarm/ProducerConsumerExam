using System;

namespace ProducerConsumerExam.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskContext _context;

        public ITaskRepository Tasks { get; private set; }

        public UnitOfWork(TaskContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't Save changes: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
