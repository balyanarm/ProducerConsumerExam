using System;

namespace ProducerConsumerExam.Common
{
    public interface ILogger
    {
       void Warn(string message);

       void Info(string message);

       void Error(string message);
        
    }
}
