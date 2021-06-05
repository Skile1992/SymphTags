using System;

namespace SymphTagsApp.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, int id) 
            : base("Entity " + name + " not found for id " + id)
        {

        }
    }
}
