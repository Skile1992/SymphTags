using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymphTagsApp.Application.Services.Tags.BL
{
    public static class TagsCleaner
    {
        //TODO: add some additional logic for better recognition of key tags
        public static List<string> Sanitize(List<string> tags)
        {
            //under documentation not defined what are 'short' words
            //so i have taken that short words are all which length is under 5
            tags.RemoveAll(x => x.Length < 5);
            
            tags.RemoveAll(x => NonAllowedWords().Any(y => y.Equals(x)));

            return tags;
        }

        //TODO: add all words that are not relevant
        //maybe move this to some resource file later
        //or try find better way for this
        private static List<string> NonAllowedWords()
        {
            return new List<string> { "despite", "except", "regarding", "without", "&amp;" };
        }

    }
}
