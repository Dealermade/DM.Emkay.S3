using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;

namespace DM.Emkay.S3
{
    [Obsolete("Use PublishFiles")]
    public class PublishFilesWithHeaders : PublishFiles 
    {        
        public PublishFilesWithHeaders() :
            this(new S3ClientFactory())
        { }

        [Obsolete("Only for test purpose!")]
        internal PublishFilesWithHeaders(IS3ClientFactory s3ClientFactory,
            int timeoutMilliseconds = DefaultRequestTimeout,
            int bufferSizeKilobytes = DefaultBufferSizeKilobytes,
            bool publicRead = true,
            ITaskLogger logger = null)
            : base(s3ClientFactory, timeoutMilliseconds, bufferSizeKilobytes, publicRead, logger)
        { }
    }
}