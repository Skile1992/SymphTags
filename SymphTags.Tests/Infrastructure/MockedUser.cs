using System;
using System.Collections.Generic;
using System.Text;
using SymphTagsApp.Application.Interfaces;

namespace SymphTags.Tests.Infrastructure
{
    public class MockedUser : ICurrentUser
    {
        public int Id => 1;
        public string DisplayName => "Test";
        public string Email => "test@gmail.com";
    }
}
