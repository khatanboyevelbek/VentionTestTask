﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace VentionTestTask.Application.Security
{
    public class SecurityPassword : ISecurityPassword
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 13, HashType.SHA512);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA512);
        }
    }
}
