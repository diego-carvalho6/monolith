using System;

namespace BGD.User.Services.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base("INCORRECT_PASSWORD")
        {
            
        }
    }
}