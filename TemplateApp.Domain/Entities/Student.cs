using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateApp.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
