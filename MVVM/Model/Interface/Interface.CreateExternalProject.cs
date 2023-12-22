using MongoDB.Driver;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        public static async Task CreateExternalProject(string client, string name, string lead)
        {
            // Pull the user object from database based on name.
            User user = await Database.user_collection.Find(x => x.Name == lead).SingleAsync();
            int project_id = await GetNextId();
            // Insert the new project into the "e_project_collection" collection in the database.
            await Database.e_project_collection.InsertOneAsync(new ExternalProject(client, name, user, project_id));
        }
    }
}