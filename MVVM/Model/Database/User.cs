using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TTC8440
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int HourlyRate { get; set; }
        public User(string name, string email, string phone, int hourly_rate)
        {
            Name = name;
            Email = email;
            Phone = phone;
            HourlyRate = hourly_rate;
        }
        public override string ToString() => $"{Name}, {Email}, {Phone}";
    }
}
