using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateApp.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
