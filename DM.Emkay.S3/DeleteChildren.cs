﻿using System;
using Microsoft.Build.Framework;

namespace DM.Emkay.S3
{
    public class DeleteChildren : S3Base
    {
        public DeleteChildren()
            : this(new S3ClientFactory(), DefaultRequestTimeout, DefaultBufferSizeKilobytes, null)
        { }

        [Obsolete("Only for test purpose!")]
        internal DeleteChildren(IS3ClientFactory s3ClientFactory, int timeoutMilliseconds, int bufferSizeKilobytes, ITaskLogger logger)
            : base(s3ClientFactory, timeoutMilliseconds, bufferSizeKilobytes, logger)
        { }

        [Required]
        public string[] Children { private get; set; }

        public override bool Execute()
        {
            try
            {
                Delete();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogMessage(MessageImportance.High,
                                  string.Format("Delete children has failed because of {0}", ex.Message));
                return false;
            }
        }

        private void Delete()
        {
            if (Children == null)
                return;

            foreach (var c in Children)
            {
                Delete(c);
            }
        }

        private void Delete(string key)
        {
            Client.DeleteObject(Bucket, key);
        }
    }
}