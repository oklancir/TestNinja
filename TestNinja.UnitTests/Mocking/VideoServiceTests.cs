using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private VideoService VideoService;
        private Mock<IFileReader> FileReader;


        [SetUp]
        public void SetUp()
        {
            FileReader = new Mock<IFileReader>();
            VideoService = new VideoService(FileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            FileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = VideoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
