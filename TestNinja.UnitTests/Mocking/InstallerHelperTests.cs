using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mock
{
    [TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> FileDownloader;
        private InstallerHelper InstallerHelper;

        [SetUp]
        public void SetUp()
        {
            FileDownloader = new Mock<IFileDownloader>();
            InstallerHelper = new InstallerHelper(FileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            FileDownloader.Setup(fd =>
            fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<WebException>();

            var result = InstallerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            var result = InstallerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }
    }
}
