using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SymphTagsApp.Application.Interfaces;

namespace SymphTags.Infrastructure
{
    public class WebsiteParser : IWebsiteParser
    {
        public async Task<List<string>> Parse(string url)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(GetAbsoluteUrl(url));

            var htmlText = await response.Content.ReadAsStringAsync();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);

            var stringBuilder = new StringBuilder();
            
            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                stringBuilder.Append(node.InnerText);
            }
            var tags = stringBuilder.ToString().Split((string[])null, StringSplitOptions.RemoveEmptyEntries).ToList();

            return tags;
        }

        private string GetAbsoluteUrl(string url)
        {
            //if already absolute return
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }
            
            //build and return absolute url
            return "http://" + url.TrimStart('/');
        }
    }
}
