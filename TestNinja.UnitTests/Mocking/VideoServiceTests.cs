using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mock
{
    [TestFixture]
    class VideoServiceTests
    {
        private VideoService VideoService;
        private Mock<IFileReader> FileReader;
        private Mock<IVideoRepository> Repository;

        [SetUp]
        public void SetUp()
        {
            FileReader = new Mock<IFileReader>();
            Repository = new Mock<IVideoRepository>();
            VideoService = new VideoService(FileReader.Object, Repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            FileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = VideoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            Repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = VideoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringOfUnprocessedVideos()
        {
            Repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> {
                new Video() { Id = 1 },
                new Video() { Id = 2 },
                new Video() { Id = 3 },
            });

            var result = VideoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
