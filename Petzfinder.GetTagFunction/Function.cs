using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Petzfinder.Model;
using Petzfinder.Models;
using Petzfinder.Service;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace Petzfinder.GetTagFunction
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request.HttpMethod == "OPTIONS")
            {
                return new ApiGatewayResponse(200);
            }
            string tagId;
            request.PathParameters.TryGetValue("tagId", out tagId);
            TagService _service = new TagService();
            var tag = await _service.GetTagById(tagId);
            //var newTag = new Tags()
            //{
            //    TagId = "AYSBN",
            //    PetId = "555",
            //    Printed = "false",
            //    Name = "Negro"
            //};
            //await _service.UpdateTag(newTag);
            //var tags = await _service.GetAllTags();
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(tag)
            };
            return response;
        }
    }
}
