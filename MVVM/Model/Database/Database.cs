using MongoDB.Driver;

namespace TTC8440
{
    public static class Database
    {
        public static readonly string connection_string = "mongodb+srv://olli:REDACTED@nodejs-practice.fnmnaw4.mongodb.net/test";
        public static readonly string database_name = "WorkTimeManagement";
        public static readonly MongoClient client = new MongoClient(connection_string);
        public static readonly IMongoDatabase db = client.GetDatabase(database_name);
        public static readonly IMongoCollection<User> user_collection = db.GetCollection<User>("Users");
        public static readonly IMongoCollection<InternalProject> i_project_collection = db.GetCollection<InternalProject>("InternalProjects");
        public static readonly IMongoCollection<ExternalProject> e_project_collection = db.GetCollection<ExternalProject>("ExternalProjects");
    }
}
