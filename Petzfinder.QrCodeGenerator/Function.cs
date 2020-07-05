using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.IdentityModel.Tokens;
using Petzfinder.Model;
using Petzfinder.Models;
using Petzfinder.Service;
using Petzfinder.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace Petzfinder.QrCodeGenerator
{
    public class Function
    {
        private TagService _tagService = new TagService();
        private AwsService _AwsService = new AwsService();

        public async Task<ApiGatewayResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            List<InMemoryFile> fileList = new List<InMemoryFile>();

            var unprintedTags = await _tagService.GetUnprintedTags();
            Dictionary<Tags, InMemoryFile> filesDictionary = new Dictionary<Tags, InMemoryFile>();
            foreach (var val in unprintedTags)
            {
                var qrArray = QrCodeCreator.CreateQrArray($"https://petzfinder.net/tag/{val.TagId}");
                var file = new InMemoryFile()
                {
                    Content = qrArray,
                    FileName = $"{val.TagId}.png"
                };
                filesDictionary.Add(val, file);
            }
            var timeStamp = DateTime.Now.ToString("yyyy_MM_dd_mm");
            _AwsService.UploadZipFileToS3(ZipFile.GetZipArchive(filesDictionary.Values.ToList()), timeStamp);
            foreach (var tag in unprintedTags)
            {
                tag.Printed = "true";
                tag.FileDestination = timeStamp.Replace("_mm_ss", "");
                await _tagService.UpdateTag(tag);
            }
            
            //_AwsService.UploadQRFileListToS3(filesDictionary);
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200
                //Body = Base64UrlEncoder.Encode(ZipFile.GetZipArchive(filesDictionary.Values.ToList())),
                //IsBase64Encoded = true
            };
            //var zip = ZipFile.GetZipArchive(filesDictionary.Values.ToList());
            return response;
        }
    }
}
