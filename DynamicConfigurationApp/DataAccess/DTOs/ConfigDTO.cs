using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lib_DynamicConfiguration.DataAccess.DTOs
{
    public class ConfigDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Type { get; set; }

        [BsonRequired]
        public string Value { get; set; }

        [BsonRequired]
        [BsonDefaultValue(0)]
        public bool IsActive { get; set; }

        [BsonRequired]
        public string ApplicationName { get; set; }
    }
}
