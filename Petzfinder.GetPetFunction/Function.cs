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
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Petzfinder.GetPetFunction
{
    public class Function
    {

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request.HttpMethod == "OPTIONS")
            {
                return new ApiGatewayResponse();
            }
            string petId;
            request.PathParameters.TryGetValue("id", out petId);
            PetService _service = new PetService();
            var tag = await _service.GetPetById(petId);
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(tag)
            };
            return response;
        }
    }
}
