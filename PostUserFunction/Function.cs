using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Petzfinder.Model;
using Petzfinder.Service;
using Petzfinder.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostUserFunction
{
    public class Function
    {

        public async Task<ApiGatewayResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string token = request.Headers["Authorization"];
            LambdaLogger.Log("CONTEXT: " + JsonConvert.SerializeObject(request));
            var accountEmail = DecodeJWT.GetAccountEmail(token);
            var user = JsonConvert.DeserializeObject<User>(request.Body);
            user.Email = accountEmail;
            UserService _service = new UserService();
            await _service.PutUser(user);
            ApiGatewayResponse response = new ApiGatewayResponse()
            {
                StatusCode = 200
            };
            return response;
        }
    }
}
