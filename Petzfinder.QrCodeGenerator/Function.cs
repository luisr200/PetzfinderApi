using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Transfer;
using QRCoder;

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

        public async Task FunctionHandler(string input, ILambdaContext context)
        {
            s3Client = new AmazonS3Client(bucketRegion);
            var fileTransferUtility = new TransferUtility(s3Client);

            var dbClient = new AmazonDynamoDBClient();
            var request = new ScanRequest
            {
                TableName = "Tag",
                ProjectionExpression = "tagId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":catg", new AttributeValue { BOOL = false } }
                },
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#printed", "printed" }
                },
                FilterExpression = "#printed = :catg"
            };

            var response = await dbClient.ScanAsync(request);

            foreach (var item in response.Items)
            {
                var value = new AttributeValue();
                item.TryGetValue("tagId", out value);
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"https://petzfinder.net/tag/{ value.S }", QRCodeGenerator.ECCLevel.Q); ;
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                var bitmap = qrCode.GetGraphic(2);
                var stream = new MemoryStream(bitmap);
                try
                {
                    await fileTransferUtility.UploadAsync(stream,
                                               bucketName, $"{value.S}.png");
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
