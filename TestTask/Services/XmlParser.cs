using System.Xml;
using System.Xml.Linq;
using TestTask.Models;

namespace TestTask.Services
{
    public static class XmlParser
    {
        public static async Task<IEnumerable<NewsModel>> ParseXml(string url)
        {
            try
            {
                using var client = new HttpClient();
                using var stream = await client.GetStreamAsync(url);
                using var xmlReader = XmlReader.Create(stream);
                XDocument xDoc = XDocument.Load(url);

                var data = from item in xDoc.Descendants("channel").Descendants("item")
                           select new NewsModel
                           {
                               Title = item.Element("title").Value,
                               Description = item.Element("description").Value,
                               FullText = item.Element("fulltext").Value,
                               PublDate = DateTime.Parse(item.Element("pubDate").Value),
                               IsRead = false
                           };
                return data;
            }
            catch
            {
                throw new Exception("Parse Error!");
            }
        }
    }
}
