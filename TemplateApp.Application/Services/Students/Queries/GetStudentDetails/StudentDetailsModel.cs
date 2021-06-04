using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateApp.Application.Services.Students.Queries.GetStudentDetails
{
    public class StudentDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryName { get; set; }
    }
}
