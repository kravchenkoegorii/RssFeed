namespace TestTask.Models
{
    public class RssFeedModel
    {
        public RssFeedModel(string link)
        {
            Link = link;
        }

        public int Id { get; set; }
        public string Link { get; set; }
    }
}
