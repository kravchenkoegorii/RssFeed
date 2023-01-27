using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public interface IFeedService
    {
        Task<RssFeedModel> AddFeed(string link);
        Task<List<RssFeedModel>> GetFeeds();
        Task<List<NewsModel>> ParseFeeds();
        Task<List<NewsModel>> GetAllNews(DateTime date);
    }
}