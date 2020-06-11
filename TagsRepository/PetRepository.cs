using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Petzfinder.Model;
using Petzfinder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petzfinder.Data
{
    public class PetRepository
    {
        public async Task<Pet> GetPetById(string key)
        {
            var dbClient = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(dbClient);
            Pet pet = await context.LoadAsync<Pet>(key);
            return pet;
        }

        public async Task<List<Pet>> GetAllUserPets(string email)
        {
            List<Pet> pets = new List<Pet>();
            var dbClient = new AmazonDynamoDBClient();
            var request = new ScanRequest
            {
                TableName = "Pet",
                //ProjectionExpression = "id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":catg", new AttributeValue { S = email } }
                },
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                    { "#user", "user" }
                },
                FilterExpression = "#user = :catg"
            };
            var response = await dbClient.ScanAsync(request);
            //TODO hacer el mapeo generico
            foreach (var item in response.Items)
            {
                AttributeValue str = new AttributeValue();
                //item.TryGetValue("id", out str);
                Pet newPet = new Pet
                {
                    Id = item.GetValueOrDefault("id").S,
                    Age = item.GetValueOrDefault("age").S,
                    Birthday = item.GetValueOrDefault("birthday").S,
                    Description = item.GetValueOrDefault("description").S,
                    MainPicture = item.GetValueOrDefault("mainPicture").S,
                    Name = item.GetValueOrDefault("name").S,
                    Pedigree = item.GetValueOrDefault("pedigree").S,
                    Pictures = item.GetValueOrDefault("pictures").S,
                    SpecialConditions = item.GetValueOrDefault("specialConditions").S,
                    SpecialConsiderations = item.GetValueOrDefault("specialConsiderations").S,
                    User = item.GetValueOrDefault("user").S,
                    Avatar= item.GetValueOrDefault("avatar").S
                };
                pets.Add(newPet);

            }
            return pets;
        }

        public async Task PutPet(Pet pet)
        {
            var dbClient = new AmazonDynamoDBClient();

            //OBJECT PERSISTENCE MODEL
            DynamoDBOperationConfig config = new DynamoDBOperationConfig()
            {
                IgnoreNullValues = true
            };
            DynamoDBContext context = new DynamoDBContext(dbClient, config);
            await context.SaveAsync(pet);
        }
    }
}
