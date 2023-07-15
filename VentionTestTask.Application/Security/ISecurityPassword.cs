using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentionTestTask.Application.Security
{
    public interface ISecurityPassword
    {
        public string Encrypt(string password);
        public bool Verify(string password, string passwordHash);
    }
}
