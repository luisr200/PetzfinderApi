using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Petzfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petzfinder.Data
{
    public class TagsRepository
    {
        public async Task<Tags> GetTagById(string id)
        {
            var dbClient = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(dbClient);
            Tags tag = await context.LoadAsync<Tags>(id);
            return tag;
        }


        public async Task<List<Tags>> GetAllTags()
        {
            //List<Tags> tags = new List<Tags>();
            //var dbClient = new AmazonDynamoDBClient();
            //var request = new ScanRequest
            //{
            //    TableName = "Tag",
            //};
            //var response = await dbClient.ScanAsync(request);
            ////TODO hacer el mapeo generico
            //foreach (var item in response.Items)
            //{
            //    AttributeValue str = new AttributeValue();
            //    item.TryGetValue("tagId", out str);
            //    Tags newTag = new Tags
            //    {
            //        TagId = str.S
            //    };
            //    tags.Add(newTag);
            //}
            //return tags;
            var dbClient = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(dbClient);
            var vr = await context.QueryAsync<Tags>(new List<ScanCondition>()).GetRemainingAsync();
            return vr.ToList();
        }
        public async Task<List<Tags>> GetUnprintedTags()
        {
            List<Tags> tags = new List<Tags>();
            var dbClient = new AmazonDynamoDBClient();
            var request = new ScanRequest
            {
                TableName = "Tag",
                ProjectionExpression = "tagId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":catg", new AttributeValue { S = "false" } }
                },
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#printed", "printed" }
                },
                FilterExpression = "#printed = :catg"
            };
            var response = await dbClient.ScanAsync(request);
            //TODO hacer el mapeo generico
            foreach (var item in response.Items)
            {
                AttributeValue str = new AttributeValue();
                item.TryGetValue("tagId", out str);
                Tags newTag = new Tags
                {
                    TagId = str.S
                };
                tags.Add(newTag);

            }
            return tags;
            //var dbClient = new AmazonDynamoDBClient();
            //var context = new DynamoDBContext(dbClient);
            //List<ScanCondition> conditions = new List<ScanCondition>()
            //{
            //    new ScanCondition("Printed", ScanOperator.Equal, false)
            //};
            //DynamoDBOperationConfig config = new DynamoDBOperationConfig()
            //{
            //    IgnoreNullValues = true
            //};
            //tags = await context.ScanAsync<Tags>(
            //    conditions,config
            //    ).GetRemainingAsync();
            //return tags.ToList();
        }

        public async Task PutTag(Tags tag)
        {
            var dbClient = new AmazonDynamoDBClient();

            //OBJECT PERSISTENCE MODEL
            DynamoDBOperationConfig config = new DynamoDBOperationConfig()
            {
                IgnoreNullValues = true
            };
            DynamoDBContext context = new DynamoDBContext(dbClient,config);
            await context.SaveAsync(tag);

            //DOCUMENT MODEL EXAMPLE
            //Table tagTable = Table.LoadTable(dbClient, "Tag");
            //var tagDocument = new Document();
            //tagDocument["tagId"] = tag.TagId;
            //tagDocument["printed"] = new DynamoDBBool(false);

            //HIGH LEVEL EXAMPLE
            //var request = new PutItemRequest
            //{
            //    TableName = "Tag",
            //    Item = new Dictionary<string, AttributeValue>
            //      {
            //        { "tagId", new AttributeValue { S = tag.TagId }}
            //        //{ "avatar", new AttributeValue { NULL = true }},
            //        //{ "description", new AttributeValue { NULL = true} },
            //        //{ "name", new AttributeValue { NULL = true }},
            //        //{ "petId", new AttributeValue { NULL = true }},
            //        //{ "photo", new AttributeValue { NULL = true }},
            //        //{ "printed", new AttributeValue{ NULL = true }}
            //      }
            //};

            //await tagTable.PutItemAsync(tagDocument);
        }        
    }
}
