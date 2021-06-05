namespace SymphTags.Domain.Entities
{
    public class LinkTags
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int LinkId { get; set; }

        public Tag Tag { get; set; }
        public Link Link { get; set; }
    }
}
