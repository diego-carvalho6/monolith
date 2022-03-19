using System;

namespace BGD.User.Services.Exceptions
{
    public class IncorrectUserNameException : Exception
    {
        public IncorrectUserNameException() : base("INCORRECT_USERNAME")
        {
            
        }
    }
}