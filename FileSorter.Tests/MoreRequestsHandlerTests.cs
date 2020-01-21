using System.Threading.Tasks;
using FileSorter.Application.Move;
using MediatR;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace FileSorter.Tests
{
    public class MoreRequestsHandlerTests : FilesTestsBase
    {
        private IRequestHandler<MoveRequest> sut;
        public MoreRequestsHandlerTests()
        {
            this.sut = (IRequestHandler<MoveRequest>)new MoveRequestHandler(this.settings, new PhysicalFileProvider(this.settings.FilesLocation));
        }

        [Fact]
        public async Task Simple()
        {
            this.CreateFile("A\\122018.jpg");
            this.CreateFile("B\\X.txt");
            this.CreateFile("B\\Y.txt");
            this.CreateFile("B\\Z.txt");

            await sut.Handle(new MoveRequest("C", "A\\122018.jpg", "B"), default);

            this.AssertPath("C\\122018.jpg").FileExists();
            this.AssertPath("C\\B").DirectoryExists();
        }


        [Fact]
        public async Task MoveToExistingFile()
        {
            this.CreateFile("A\\B.txt");
            this.CreateFile("B\\1.txt");
            this.CreateFile("B\\2.txt");
            this.CreateFile("B\\3.txt");
            this.CreateFile("B\\4.txt");
            this.CreateFile("B\\5.txt");

            await sut.Handle(new MoveRequest("A\\B.txt", "B\\1.txt", "B\\2.txt"), default);
            await sut.Handle(new MoveRequest("A\\B.txt", "B\\3.txt"), default);
            await sut.Handle(new MoveRequest("A\\B (2).txt", "B\\4.txt"), default);

            this.AssertPath("A\\B.txt").FileExists();
            this.AssertPath("A\\B (1).txt").FileExists();
            this.AssertPath("A\\B (2).txt").FileExists();
            this.AssertPath("A\\B (3).txt").FileExists();
            this.AssertPath("A\\B (4).txt").FileExists();
        }
        [Fact]
        public async Task MoveToDirectoryWithExistingFile()
        {
            this.CreateFile("A\\B.txt");
            this.CreateFile("B\\B.txt");

            await sut.Handle(new MoveRequest("A", "B\\B.txt"), default);

            this.AssertPath("A\\B.txt").FileExists();
            this.AssertPath("A\\B (1).txt").FileExists();
        }
        [Fact]
        public async Task MoveDirectoryToDirectoryWithExistingFile()
        {
            this.CreateFile("A\\B.txt");
            this.CreateFile("A\\B\\B.txt");
            this.CreateFile("A\\B\\D.txt\\B.txt");
            this.CreateFile("B\\B.txt");
            this.CreateFile("B\\C\\B.txt");
            this.CreateFile("B\\D.txt");

            await sut.Handle(new MoveRequest("A", "B"), default);

            this.AssertPath("A\\B.txt").FileExists();
            this.AssertPath("A\\B\\B.txt").FileExists();
            this.AssertPath("A\\B\\B (1).txt").FileExists();
            this.AssertPath("A\\B\\D (1).txt").FileExists();
            this.AssertPath("A\\B\\C\\B.txt").FileExists();
        }

    }
}