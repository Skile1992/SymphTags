using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Domain.Entities;

namespace TemplateApp.Persistance
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<User> User { get; set; }
    }
}
