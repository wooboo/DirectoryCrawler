using System;
using DirectoryCrawler.Services;
using Newtonsoft.Json;

namespace DirectoryCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new Crawler("./Sample");
            //var dir = crawler.Crawl(null, "./");
            //Console.WriteLine(JsonConvert.SerializeObject(dir, Formatting.Indented));
        }
    }
}
