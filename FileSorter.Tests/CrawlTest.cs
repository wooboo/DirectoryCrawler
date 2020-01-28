using DirectoryCrawler.Services;
using FluentAssertions;
using Xunit;

namespace FileSorter.Tests
{
    public class CrawlTest
    {

        [Fact]
        public void Build()
        {
            // arrange
            var sut = new Crawler("..\\FileSorter\\Sample");

            // act
            var result = sut.Build("KSW Doradztwo");
            // assert
            result.Should().NotBeNull();
        }
        //[Fact]
        //public void Do()
        //{
        //    // /
        //    // /A
        //    // /A/B
        //    // /A/B/C
        //    // /A/B/D
        //    // /A/B/E9
        //    var metaUtil1 = MetaUtil.Create(null, "./Sample/.dirrc.json");
        //    var metaUtil2 = metaUtil1.Add("./Sample/PZ Solutions/.dirrc.json");
        //    var metaUtil3 = metaUtil2.Add("./Sample/PZ Solutions/2018/.dirrc.json");
        //    var metaUtil4 = metaUtil3.Add("./Sample/PZ Solutions/2018/01/.dirrc.json");

        //    var props1 = metaUtil4.GetProperties("./Sample");
        //    var props2 = metaUtil4.GetProperties("./Sample/PZ Solutions");
        //    var props3 = metaUtil4.GetProperties("./Sample/PZ Solutions/2018");
        //    var props4 = metaUtil4.GetProperties("./Sample/PZ Solutions/2018/01");
        //}
    }
}