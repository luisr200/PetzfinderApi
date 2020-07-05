using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Petzfinder.Model;
using Petzfinder.Service;
using Petzfinder.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PutPetFunction
{
    public class Function
    {
        public async Task<ApiGatewayResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            LambdaLogger.Log("CONTEXT: " + JsonConvert.SerializeObject(request));
            //byte[] data = Convert.FromBase64String(request.Body);
            //string decodedString = Encoding.UTF8.GetString(data);
            var pet = JsonConvert.DeserializeObject<Pet>(request.Body);
            if (string.IsNullOrEmpty(pet.Id))
            {
                pet.Id = AlphanumericFactory.RandomString(5);
            }
            PetService _service = new PetService();
            await _service.PutPet(pet);
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(pet)
            };
            return response;
        }
    }
}
