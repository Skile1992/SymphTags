using System.Collections.Generic;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkByTagsList
{
    public class LinkListByTagsModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<TagModel> Tags { get; set; }
    }

    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
