using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VentionTestTask.Domain.Exceptions
{
    public class FailedArgumentExceptions : Xeption
    {
        public FailedArgumentExceptions(string message, Exception innerException) 
            : base(message, innerException)
        { }
    }
}
