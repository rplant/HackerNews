using HackNews.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackNews.Repository
{
    public class HackNewsRepository : IHackNewsRepository
    {
        public HttpClient client;

        public HackNewsRepository()
        {
            client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetNewStoryListAsync()
        {
            return await client.GetAsync("https://hacker-news.firebaseio.com/v0/newstories.json");
        }

        public async Task<HttpResponseMessage> GetStoryByIdAsync(int id)
        {
            return await client.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
        }

        public async Task<HttpResponseMessage> GetTopStoryListAsync()
        {
            return await client.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json");
        }
    }
}
