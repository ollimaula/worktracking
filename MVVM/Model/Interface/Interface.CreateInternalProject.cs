using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        public static async Task CreateInternalProject(string name, string lead)
        {
            // Pull the user object from database based on name.
            User user = await Database.user_collection.Find(x => x.Name == lead).SingleAsync();
            int project_id = await GetNextId();
            // Insert the new project into the "i_project_collection" collection in the database.
            await Database.i_project_collection.InsertOneAsync(new InternalProject(name, user, project_id));
        }
    }
}
