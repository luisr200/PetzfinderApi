using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Transfer;
using Petzfinder.Service;
using Petzfinder.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace Petzfinder.QrCodeGenerator
{
    public class Function
    {

        private const string bucketName = "petzfinderqr";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;
        private TagService _service = new TagService();

        public async Task FunctionHandler(string input, ILambdaContext context)
        {
            s3Client = new AmazonS3Client(bucketRegion);
            var fileTransferUtility = new TransferUtility(s3Client);
            var response = await _service.GetUnprintedTags();

            foreach (var val in response)
            {
                var stream = QrCodeCreator.CreateQr($"https://petzfinder.net/tag/{val.TagId}");
                try
                {
                    await fileTransferUtility.UploadAsync(stream,
                                               bucketName, $"{val.TagId}.png");
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
        }
    }
}
