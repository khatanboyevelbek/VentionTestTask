using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VentionTestTask.Domain.Exceptions
{
    public class InvalidDtoException : Xeption
    {
        public InvalidDtoException(string message)
            : base(message)
        { }
    }
}
