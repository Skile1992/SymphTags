using System.Collections.Generic;

namespace SymphTags.Domain.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserIdCreated { get; set; }

        public User User { get; set; }
        public ICollection<LinkTags> LinkTags { get; set; }
    }
}
