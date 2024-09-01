using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Domain.Interfaces
{
    public interface ISecurityService
    {
        public string GenerateHash(string password);
        public bool VerifyHash(string passwordHash, string password);
    }
}
