using System;

namespace BGD.User.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("NOT_FOUND")
        {
            
        }
    }
}