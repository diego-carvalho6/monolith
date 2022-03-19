using System;

namespace BGD.User.Services.Exceptions
{
    public class AlreadyInUseException : Exception
    {
        public AlreadyInUseException() : base("ALREADY_IN_USE")
        {
            
        }
    }
}