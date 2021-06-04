using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TemplateApp.Persistance
{
    public class ContextInitializer
    {
        public static void Initialize(Context context)
        {
            SeedCountries(context);
            SeedStudents(context);
            SeedCourses(context);
            SeedUsers(context);
        }

        private static void SeedCountries(Context context)
        {
            context.Country.Add(new Domain.Entities.Country
            {
                Id = 1,
                Name = "BiH"
            });
            context.Country.Add(new Domain.Entities.Country
            {
                Id = 2,
                Name = "Srbija"
            });

            context.SaveChanges();
        }

        private static void SeedStudents(Context context)
        {
            context.Student.Add(new Domain.Entities.Student
            {
                Id = 1,
                Name = "Marko",
                Surname = "Markovic",
                CountryId = 1
            });
            context.Student.Add(new Domain.Entities.Student
            {
                Id = 2,
                Name = "Petar",
                Surname = "Petrovic",
                CountryId = 1
            });

            context.SaveChanges();
        }

        private static void SeedCourses(Context context)
        {
            context.Course.Add(new Domain.Entities.Course
            {
                Id = 1,
                Name = "Uvod u programiranje"
            });

            context.Course.Add(new Domain.Entities.Course
            {
                Id = 2,
                Name = "Programski jezici"
            });

            context.SaveChanges();
        }

        private static void SeedUsers(Context context)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("mySalt"));

            context.User.Add(new Domain.Entities.User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")),
                Salt = Encoding.UTF8.GetBytes("mySalt")
            });

            context.User.Add(new Domain.Entities.User
            {
                Id = 2,
                Username = "test",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")),
                Salt = Encoding.UTF8.GetBytes("mySalt")
            });

            context.SaveChanges();
        }
    }
}
