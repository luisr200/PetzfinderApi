using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace Petzfinder.Model
{
    [DynamoDBTable("Pet")]
    public class Pet
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("user")]
        public string User { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("sex")]
        public string Sex { get; set; }

        [DynamoDBProperty("age")]
        public string Age { get; set; }

        [DynamoDBProperty("birthday")]
        public string Birthday { get; set; }

        [DynamoDBProperty("description")]
        public string Description { get; set; }

        [DynamoDBProperty("mainPicture")]
        public string MainPicture { get; set; }

        [DynamoDBProperty("pictures")]
        public string Pictures { get; set; }

        [DynamoDBProperty("pedigree")]
        public string Pedigree { get; set; }

        [DynamoDBProperty("specialConditions")]
        public string SpecialConditions { get; set; }

        [DynamoDBProperty("specialConsiderations")]
        public string SpecialConsiderations { get; set; }

        [DynamoDBProperty("avatar")]
        public string Avatar { get; set; }
    }
}
