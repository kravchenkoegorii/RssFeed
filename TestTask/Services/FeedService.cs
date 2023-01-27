using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Exceptions;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class FeedService : IFeedService
    {
        private readonly TaskDbContext _dbContext;

        public FeedService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RssFeedModel> AddFeed(string link)
        {
            var feed = new RssFeedModel(link);
            await _dbContext.Feeds.AddAsync(feed);
            await _dbContext.SaveChangesAsync();
            return feed;
        }

        public async Task<List<RssFeedModel>> GetFeeds()
        {
            return await _dbContext.Feeds.ToListAsync();
        }

        public async Task<List<NewsModel>> GetAllNews(DateTime date)
        {
            var news = await _dbContext.News.Where(item => item.PublDate == date).ToListAsync();
            if (news == null)
            {
                throw new NotFoundException("News not found");
            }
            foreach (var item in news)
            {
                item.IsRead = true;
            }
            await _dbContext.SaveChangesAsync();
            return news;
        }

        public async Task<List<NewsModel>> ParseFeeds()
        {
            var feeds = await _dbContext.Feeds.ToListAsync();
            foreach (RssFeedModel feedUrls in feeds)
            {
                var data = await XmlParser.ParseXml(feedUrls.Link);
                await _dbContext.News.AddRangeAsync(data.ToList());
                await _dbContext.SaveChangesAsync();
            }

            return await _dbContext.News.ToListAsync();
        }
    }
}
