using System;

namespace ProducerConsumerExam.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }

        int Complete();
    }
}
