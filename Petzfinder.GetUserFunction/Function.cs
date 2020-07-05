using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Petzfinder.Model;
using Petzfinder.Service;
using Petzfinder.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Petzfinder.GetUserFunction
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
            string token = request.Headers["Authorization"] ;
            var accountEmail = DecodeJWT.GetAccountEmail(token);
            //LambdaLogger.Log("Context Identity: " + JsonConvert.SerializeObject(context));
            //LambdaLogger.Log("Request Context" + JsonConvert.SerializeObject(request.RequestContext.Authorizer.));
            UserService _service = new UserService();
            var user = await _service.GetUserByKey(accountEmail);
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(user)
            };
            return response;
        }
    }
}
