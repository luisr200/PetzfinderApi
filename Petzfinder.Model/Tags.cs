using Amazon.DynamoDBv2.DataModel;

namespace Petzfinder.Models
{
    [DynamoDBTable("Tag")]
    public class Tags
    {
        [DynamoDBHashKey("tagId")]
        public string TagId { get; set; }

        [DynamoDBProperty("petId")]
        public string PetId { get; set; }

        [DynamoDBProperty("avatar")]
        public string Avatar { get; set; }

        [DynamoDBProperty("description")]
        public string Description { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("photo")]
        public string Photo { get; set; }

        [DynamoDBProperty("printed")]
        public string Printed { get; set; }

    }
}
