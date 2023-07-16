using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VentionTestTask.Domain.Exceptions
{
    public class FailedServiceExceptions : Xeption
    {
        public FailedServiceExceptions(string message, Exception innerException) 
            : base(message, innerException)
        { }
    }
}
