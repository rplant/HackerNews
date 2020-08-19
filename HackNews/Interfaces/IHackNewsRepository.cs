using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackNews.Interfaces
{
    public interface IHackNewsRepository
    {
        Task<HttpResponseMessage> GetTopStoryListAsync();

        Task<HttpResponseMessage> GetNewStoryListAsync();

        Task<HttpResponseMessage> GetStoryByIdAsync(int id);

    }
}
