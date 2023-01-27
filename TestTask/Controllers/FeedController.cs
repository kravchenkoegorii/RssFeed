using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpPost("feed_url")]
        public async Task<ActionResult<RssFeedModel>> AddFeed([FromHeader] string link)
        {
            return await _feedService.AddFeed(link);
        }

        [HttpGet("feeds")]
        public async Task<List<RssFeedModel>> GetActiveFeeds()
        {
            return await _feedService.GetFeeds();
        }

        [HttpGet("news/{date}")]
        public async Task<List<NewsModel>> GetNewsByDate(DateTime date)
        {
            return await _feedService.GetAllNews(date);
        }

        [HttpPost("parse")]
        public async Task<List<NewsModel>> ParseFeeds()
        {
            return await _feedService.ParseFeeds();
        }
    }
}
