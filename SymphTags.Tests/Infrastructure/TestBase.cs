using System;
using Microsoft.EntityFrameworkCore;
using SymphTags.Persistance;
using SymphTagsApp.Application.Interfaces;

namespace SymphTags.Tests.Infrastructure
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

        public MockedUser _currentUser
        {
            get
            {
                return new MockedUser();
            }
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
