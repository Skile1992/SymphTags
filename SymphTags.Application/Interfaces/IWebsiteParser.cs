using System.Collections.Generic;
using System.Threading.Tasks;

namespace SymphTagsApp.Application.Interfaces
{
    public interface IWebsiteParser
    {
        Task<List<string>> Parse(string url);
    }
}
