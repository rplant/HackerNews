using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackNews.Interfaces
{
    public interface IHackNewsBusiness
    {
        public Task<List<INewsStory>> GetNewHackStories(string searchTerm);

    }
}
