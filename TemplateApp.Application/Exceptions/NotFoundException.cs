using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateApp.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, int id) 
            : base("Entity " + name + " not found for id " + id)
        {

        }
    }
}
