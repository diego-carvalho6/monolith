using System;

namespace BGD.User.Services.Exceptions
{
    public class DigitPasswordException : Exception
    {
        public DigitPasswordException() : base("DIGIT_PASSWORD_EXCEPTION")
        {
            
        }
    }
}