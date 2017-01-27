using NUnit.Framework;

namespace DM.Emkay.S3.Tests
{
    [TestFixture]
    public class DeleteBucketTests : S3TestsBase
    {
        private DeleteBucket _delete;
        private const string BucketName = "S3-66427BC0DE13";

        [SetUp]
        public void SetUp()
        {
            Client.EnsureBucketExists(BucketName);

            _delete = new DeleteBucket(ClientFactory, RequestTimoutMilliseconds, BufferSizeKilobytes, LoggerMock)
                        {
                            Bucket = BucketName
                        };
        }

        [TearDown]
        public void TearDown()
        {
            if (_delete != null)
                _delete.Dispose();
            _delete = null;
        }

        [Test]
        public void Execute_should_succeed()
        {
            Assert.IsTrue(_delete.Execute());
        }
    }
}