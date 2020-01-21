using System;
using System.IO;
using Xunit;
using DirectoryCrawler.Model;
using DirectoryCrawler.Services;
using FluentAssertions;
using System.Linq;

namespace FileSorter.Tests
{

    public class DirectoryExTests
    {
        [Fact]
        public void GetDirectories()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var directories = dir.GetDirectories();
            
            // assert
            directories.Should().Contain(o=>o.Name == "KSW Doradztwo")
                .And.Contain(o => o.Name == "PZ Solutions");
        }


        [Fact]
        public void GetFile_Exists()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var got = dir.TryGetFile(".dirrc.json", out var file);

            // assert
            got.Should().BeTrue();
            file.Should().NotBeNull();
            file.Name.Should().Be(".dirrc.json");
        }
        [Fact]
        public void GetFile_RelativePath_Exists()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var got = dir.TryGetFile("KSW Doradztwo\\.dirrc.json", out var file);

            // assert
            got.Should().BeTrue();
            file.Should().NotBeNull();
            file.Name.Should().Be(".dirrc.json");
        }
        [Fact]
        public void GetFile_NotExists()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var got = dir.TryGetFile(Guid.NewGuid().ToString("N"), out var file);

            // assert
            got.Should().BeFalse();
            file.Should().BeNull();
        }

        [Fact]
        public void GetNestedDirectory_RelativePath_Exists()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var got = dir.TryGetNestedDirectory("KSW Doradztwo\\2018", out var directory);

            // assert
            got.Should().BeTrue();
            directory.Should().NotBeNull();
        }
        [Fact]
        public void GetNestedDirectory_RelativePath_NotExists()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var got = dir.TryGetNestedDirectory("KSW Doradztwo\\2018\\XYZ123", out var directory);

            // assert
            got.Should().BeFalse();
            directory.Should().BeNull();
        }


        [Fact]
        public void WalkDown_RelativePath_Exists1()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var results = dir.WalkDown("KSW Doradztwo\\2018").ToArray();

            // assert
            results.Should().HaveCount(2);
            results[0].Name.Should().Be("KSW Doradztwo");
            results[1].Name.Should().Be("2018");
        }

        [Fact]
        public void WalkDown_RelativePath_Exists2()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var results = dir.WalkDown("KSW Doradztwo\\..\\PZ Solutions\\2018").ToArray();

            // assert
            results.Should().HaveCount(4);
            results[0].Name.Should().Be("KSW Doradztwo");
            results[1].Name.Should().Be($"{Path.DirectorySeparatorChar}");
            results[2].Name.Should().Be("PZ Solutions");
            results[3].Name.Should().Be("2018");
        }

        [Fact]
        public void WalkDown_RelativePath_Exists3()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var results = dir.WalkDown("KSW Doradztwo\\2018\\\\PZ Solutions\\2018").ToArray();

            // assert
            results.Should().HaveCount(5);
            results[0].Name.Should().Be("KSW Doradztwo");
            results[1].Name.Should().Be("2018");
            results[2].Name.Should().Be($"{Path.DirectorySeparatorChar}");
            results[3].Name.Should().Be("PZ Solutions");
            results[4].Name.Should().Be("2018");
        }
        [Fact]
        public void WalkDown_RelativePath_Exists4()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample");

            // act
            var results = dir.WalkDown("\\PZ Solutions\\2018").ToArray();

            // assert
            results.Should().HaveCount(3);
            results[0].Name.Should().Be($"{Path.DirectorySeparatorChar}");
            results[1].Name.Should().Be("PZ Solutions");
            results[2].Name.Should().Be("2018");
        }

        [Fact]
        public void WalkUp_RelativePath_Exists1()
        {
            // arrange
            var dir = new DirectoryEx(".\\Sample")
                .GetNestedDirectory("KSW Doradztwo\\2018\\\\PZ Solutions\\2018");

            // act
            var results = dir.WalkUp().ToArray();

            // assert
            results.Should().HaveCount(3);
            results[0].Name.Should().Be("2018");
            results[1].Name.Should().Be("PZ Solutions");
            results[2].Name.Should().Be($"{Path.DirectorySeparatorChar}");
        }
    }
}
