using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.APIGatewayEvents;

namespace Petzfinder.Model
{
    public class ApiGatewayResponse : APIGatewayProxyResponse
    {
        public ApiGatewayResponse()
        {
            Headers = new Dictionary<string,string>();
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Headers", "Content-Type,Authorization,X-Amz-Date,X-Api-Key,X-Amz-Security-Token"));
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Methods", "DELETE, POST, GET, OPTIONS"));
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"));
        }

        public ApiGatewayResponse(int status)
        {
            Headers = new Dictionary<string, string>();
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Headers", "Content-Type,Authorization,X-Amz-Date,X-Api-Key,X-Amz-Security-Token"));
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Methods", "DELETE, POST, GET, OPTIONS"));
            Headers.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"));
            StatusCode = status;
        }
    }
}
