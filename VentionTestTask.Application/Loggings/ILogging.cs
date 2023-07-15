using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentionTestTask.Application.Loggings
{
    public interface ILogging
    {
        void LogInformation(string message);
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
