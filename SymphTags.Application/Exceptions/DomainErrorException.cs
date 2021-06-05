using System;

namespace SymphTagsApp.Application.Exceptions
{
    public class DomainErrorException : Exception
    {
        public DomainErrorException(string message) : base(message)
        {

        }
    }
}
