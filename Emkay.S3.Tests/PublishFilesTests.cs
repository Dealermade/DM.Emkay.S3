﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Emkay.S3.Tests
{
    [TestFixture]
    public class PublishFilesTests
    {
        private PublishFiles _publish;
        private const string Key = ""; // TODO edit your AWS S3 key here
        private const string Secret = ""; // TODO edit your AWS S3 secret here
        private const string SourceFolder = "."; // TODO edit your local folder here
        private const string Bucket = ""; // TODO edit your bucket name here
        private const string DestinationFolder = ""; // TODO edit your destination folder here

        [SetUp]
        public void SetUp()
        {
            _publish = new PublishFiles(300000, true, new Mock<ITaskLogger>().Object)
                                        {
                                            Key = Key,
                                            Secret = Secret,
                                            Client = new Mock<IS3Client>().Object, // TODO comment this here for lazy instanciation
                                            SourceFiles = EnumerateFiles(SourceFolder),
                                            Bucket = Bucket,
                                            DestinationFolder = DestinationFolder
                                        };
        }

        [TearDown]
        public void TearDown()
        {
            _publish.Dispose();
        }

        [Test]
        public void Execute_should_succeed()
        {
            Assert.IsTrue(_publish.Execute());
        }

        private static string[] EnumerateFiles(string folder)
        {
            return new DirectoryInfo(folder).GetFiles().Select(i => i.FullName).ToArray();
        }
    }
}
