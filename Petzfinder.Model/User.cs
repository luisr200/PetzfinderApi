using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petzfinder.Model
{
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey("email")]
        public string Email { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }
        

        [DynamoDBProperty("phone")]
        public string Phones { get; set; }

        [DynamoDBProperty("location")]
        public string Location { get; set; }


        [DynamoDBProperty("createdDate")]
        public string CreatedDate { get; set; }

        [DynamoDBProperty("updatedDate")]
        public string UpdatedDate { get; set; }

        [DynamoDBProperty("picture")]
        public string Picture { get; set; }

        [DynamoDBProperty("birthdate")]
        public string Birthdate { get; set; }

        [DynamoDBProperty("address")]
        public string Address { get; set; }
    }
}
