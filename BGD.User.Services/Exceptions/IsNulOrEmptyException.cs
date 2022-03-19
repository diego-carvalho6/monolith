using System;
using System.Buffers.Text;

namespace BGD.User.Services.Exceptions
{
    public class IsNulOrEmptyException : Exception
    {
        public IsNulOrEmptyException() : base("IS_NULL_OR_EMPTY")
        {
            
        }
        
    }
}