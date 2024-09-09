using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace server.Data
{
    public class RecipeItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "Recipe Name is required")]
        public string Name { get; set; }

        [BsonElement("created_at")]
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("userid")]
        [Required(ErrorMessage = "userid is required")]
        public string UserId { get; set; }

        [BsonElement("url")]
        [DefaultValue("none")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [BsonElement("type")]
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [BsonElement("ingredients")]
        [Required(ErrorMessage = "Ingredients are required")]
        public Ingredient[] Ingredients { get; set; }

        [BsonElement("rating")]
        [DefaultValue(0)]
        public int Rating { get; set; }

        [BsonElement("tags")]
        [DefaultValue(new string[] { })]
        public string[] Tags { get; set; }

        [BsonElement("instructions")]
        [Required(ErrorMessage = "Instructions are required")]
        public string[] Instructions { get; set; }
    }

    public enum SourceType
    {
        website = 1,
        video = 2,
        book = 3,
        custom = 4
    }

    public class Ingredient
    {
        [BsonElement("name")]
        [Required(ErrorMessage = "Ingredient Name is required")]
        public string Name { get; set; }

        [BsonElement("quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [BsonElement("unit")]
        [Required(ErrorMessage = "Unit is required")]
        public string Unit { get; set; }
    }
}



// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
//
// namespace server.Data;
//
// public class RecipeItem
// {
//     [Required (ErrorMessage = "Recipe Name is required")]
//     public string name { get; set; }
//     
//     [Required (ErrorMessage = "Description is required")]
//     [DataType(DataType.Date)]
//     public DateTime created_at { get; set; }
//     
//     [Required (ErrorMessage = "userid is required")]
//     public Guid userid { get; set; }
//     
//     [DefaultValue("none")]
//     [DataType(DataType.Url)]
//     public string url { get; set; }
//     
//     [Required (ErrorMessage = "Type is required")]
//     public Type type { get; set; }
//     
//     [Required (ErrorMessage = "Ingredients are required")]
//     public Ingredient[] ingredients { get; set; }
//     
//     [DefaultValue(0)]
//     public int rating { get; set; }
//     
//     [DefaultValue(new String[] {})]
//     public string[] tags { get; set; }
//     
//     [Required (ErrorMessage = "Instructions are required")]
//     public string[] instructions { get; set; }
// }
//
// public enum Type
// {
//     website = 1,
//     video = 2,
//     book = 3,
//     custom = 4
// }
//
// public class Ingredient
// {
//     [Required (ErrorMessage = "Ingredient Name is required")]
//     public string name { get; set; }
//     
//     [Required (ErrorMessage = "Quantity is required")]
//     public int quantity { get; set; }
//     
//     [Required (ErrorMessage = "Unit is required")]
//     public string unit { get; set; }
// }
