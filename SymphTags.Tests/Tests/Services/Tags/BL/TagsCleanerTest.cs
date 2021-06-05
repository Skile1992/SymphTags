using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SymphTagsApp.Application.Services.Tags.BL;

namespace SymphTags.Tests.Tests.Services.Tags.BL
{
    [TestClass]
    public class TagsCleanerTest
    {
        [TestMethod]
        public void SanitizeTextTest()
        {
            var tags = new List<string>()
            {
                "football", "without", "test"
            };

            var sanitizedTags = TagsCleaner.Sanitize(tags);

            Assert.IsNotNull(sanitizedTags);
            Assert.AreEqual(1, sanitizedTags.Count);
            Assert.AreEqual("football", sanitizedTags[0]);
        }
    }
}
