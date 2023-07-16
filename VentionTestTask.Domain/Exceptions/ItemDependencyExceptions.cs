using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VentionTestTask.Domain.Exceptions
{
    public class ItemDependencyExceptions : Xeption
    {
        public ItemDependencyExceptions(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
