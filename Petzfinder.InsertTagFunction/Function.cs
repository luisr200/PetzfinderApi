using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petzfinder.Util;

using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using Petzfinder.Service;
using Petzfinder.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace Petzfinder.InsertTagFunction
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(object input, ILambdaContext context)
        {
            TagService _tagService = new TagService();
            List<Tags> tags = new List<Tags>();
            dynamic num = (int)JObject.Parse(input.ToString())["number"];
            for (int i = 0; i < num; i++)
            {
                Tags tag = new Tags();
                tag.TagId = AlphanumericFactory.RandomString(5);
                tags.Add(tag);
            }
            await _tagService.InsertTags(tags);
            return "End";
        }
    }
}
