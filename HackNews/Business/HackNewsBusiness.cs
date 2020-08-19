using HackNews.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackNews.Business
{
    public class HackNewsBusiness : IHackNewsBusiness
    {
        private IMemoryCache memoryCache;
        private IHackNewsRepository hackNewsRepository;

        public HackNewsBusiness(IMemoryCache memoryCache, IHackNewsRepository hackNewsRepository)
        {
            this.memoryCache = memoryCache;
            this.hackNewsRepository = hackNewsRepository;
        }

        public async Task<List<INewsStory>> GetNewHackStories(string searchTerm)
        {
            List<INewsStory> stories = new List<INewsStory>();

            var response = await hackNewsRepository.GetNewStoryListAsync();
            if (response.IsSuccessStatusCode)
            {
                var storiesResponse = response.Content.ReadAsStringAsync().Result;
                var newIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);

                try
                {
                    var tasks = newIds.Select(GetStoryAsync);
                    stories = (await Task.WhenAll(tasks)).ToList();

                    if (!String.IsNullOrEmpty(searchTerm))
                    {
                        var search = searchTerm.ToLower();
                        stories = stories.Where(s => (s?.Title.ToLower().IndexOf(search) > -1) && !string.IsNullOrEmpty(s?.Url)).ToList();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message} add to fake log file");
                }

            }
            return stories;
        }

        private async Task<INewsStory> GetStoryAsync(int storyId)
        {
            return await memoryCache.GetOrCreateAsync<INewsStory>(storyId,
                async cacheEntry =>
                {
                    INewsStory story = new INewsStory();

                    try
                    {

                        var response = await hackNewsRepository.GetStoryByIdAsync(storyId);
                        if (response.IsSuccessStatusCode)
                        {
                            var storyResponse = response.Content.ReadAsStringAsync().Result;
                            story = JsonConvert.DeserializeObject<INewsStory>(storyResponse);
                        }

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message} add to fake log file");
                    }

                    return story;
                });
        }
    }
}
