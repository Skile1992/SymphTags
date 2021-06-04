using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Persistance;

namespace TemplateApp.Tests.Infrastructure
{
    public class TestBase : IDisposable
    {
        public Context _context
        {
            get
            {
                var inMemoryContextOptions = new DbContextOptionsBuilder<Context>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new Context(inMemoryContextOptions);
            }
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
