using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Petzfinder.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Petzfinder.Data
{
    public class UserRepository
    {

        public async Task<User> GetUserByKey(string key)
        {
            var dbClient = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(dbClient);
            User user = await context.LoadAsync<User>(key);
            return user;
        }

        public async Task PutUser(User user)
        {
            var dbClient = new AmazonDynamoDBClient();

            //OBJECT PERSISTENCE MODEL
            DynamoDBOperationConfig config = new DynamoDBOperationConfig()
            {
                IgnoreNullValues = true
            };
            DynamoDBContext context = new DynamoDBContext(dbClient, config);
            await context.SaveAsync(user);
        }
    }
}
