using System;
using System.Collections.Generic;
using System.Text;
using SymphTags.Domain.Entities;

namespace SymphTagsApp.Application.Services.Links.Queries.GetLinkList
{
    public class LinkListModel
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
