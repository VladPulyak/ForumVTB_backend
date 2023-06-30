using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class ResetPasswordException : Exception
    {
        public ResetPasswordException(string message) : base(message)
        {
            
        }
    }
}
