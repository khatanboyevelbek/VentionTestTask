using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VentionTestTask.Domain.Exceptions
{
    public class DtoValidationExceptions : Xeption
    {
        public DtoValidationExceptions(string messsage, Xeption innerException) 
            : base(messsage, innerException)
        { }
    }
}
