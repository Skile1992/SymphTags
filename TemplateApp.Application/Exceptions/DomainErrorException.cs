using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateApp.Application.Exceptions
{
    public class DomainErrorException : Exception
    {
        public DomainErrorException(string message) : base(message)
        {

        }
    }
}
