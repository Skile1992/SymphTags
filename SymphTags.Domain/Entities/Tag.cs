using System.Collections.Generic;

namespace SymphTags.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LinkTags> LinkTags { get; set; }
    }
}
