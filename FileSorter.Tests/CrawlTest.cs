using System.Collections.Generic;
using DirectoryCrawler.Model;
using DirectoryCrawler.Services;
using FluentAssertions;
using Xunit;

namespace FileSorter.Tests
{
    public class MetaTests
    {
        [Fact]
        public void Merge_Properties()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");
            var sut = new Meta()
            {
                Properties = new Dictionary<string, object>
                {
                    {"a","A" },
                    {"b","B" },
                    {"c","C" },
                }
            };

            var meta2 = new Meta()
            {
                Properties = new Dictionary<string, object>
                {
                    {"a","X" },
                    {"d","D" },
                }
            };
            // act
            var result = sut.Merge(meta2, dir.GetDirectory("PZ Solutions"));
            // assert
            result.Should().NotBeNull();
            result.Properties["a"].Should().Be("X");
            result.Properties["b"].Should().Be("B");
            result.Properties["c"].Should().Be("C");
            result.Properties["d"].Should().Be("D");
        }
        [Fact]
        public void Merge_Directories()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            var sut = new Meta()
            {
                Directories =
                {
                    {"a", new Meta()},
                    {"b", new Meta()},
                    {"c", new Meta()},
                    {"c\\d", new Meta()},
                    {"c\\a", new Meta()},
                    {"*", new Meta()},
                    {"**", new Meta()},
                }
            };

            var meta2 = new Meta()
            {
                Directories =
                {
                    {"d", new Meta()},
                    {"c", new Meta()},
                    {"d\\e", new Meta()},
                }
            };
            // act
            var result = sut.Merge(meta2, new DirectoryEx(dir, "c"));
            // assert
            result.Should().NotBeNull();
            result.Directories.Should().ContainKey("d");
            result.Directories.Should().ContainKey("c");
            result.Directories.Should().ContainKey("a");
            result.Directories.Should().ContainKey("d\\e");
            result.Directories.Should().ContainKey("**");
            result.Directories.Should().NotContainKey("*");
        }

    }
    public class CrawlTest
    {

        [Fact]
        public void Build()
        {
            // arrange
            var sut = new Crawler(".\\Sample");

            // act
            var result = sut.Build("KSW Doradztwo\\2018\\01");
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