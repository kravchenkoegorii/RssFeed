namespace TestTask.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public DateTime PublDate { get; set; }
        public bool IsRead { get; set; }
    }
}
