using Amazon;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using Amazon.S3.Transfer;
using Petzfinder.Model;
using Petzfinder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Petzfinder.Service
{
    public class AwsService
    {
        private const string bucketNameQr = "petzfinderqr";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;
        private TagService _tagService = new TagService();
        public async Task UploadFileToS3Bucket(MemoryStream stream, string filename, string bucketName)
        {
            try
            {
                s3Client = new AmazonS3Client(bucketRegion);
                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(stream,
                                           bucketName,filename);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public async Task UploadZipFileToS3(byte[] zipArray, string name)
        {
            try
            {
                
                await UploadFileToS3Bucket(new MemoryStream(zipArray), $"{name}.zip", $"{bucketNameQr}/{name}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public async Task UploadQRFileListToS3(Dictionary<Tags,InMemoryFile> files)
        {
            try
            {
                var timeStamp = DateTime.Now.ToString("yyyy_MM_dd_mm_ss");
                foreach (var file in files)
                {
                    await UploadFileToS3Bucket(new MemoryStream(file.Value.Content),file.Value.FileName,$"{bucketNameQr}/{timeStamp}");
                    file.Key.Printed = "true";
                    await _tagService.UpdateTag(file.Key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
    }
}
