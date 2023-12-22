using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        // Creates a new work record for a given project and user
        public static async Task CreateWork(Project _project, User _user, DateTime time, int hours)
        {
            InternalProject iproject = null;
            ExternalProject eproject = null;
            // Try to find the project from internal and external collections.
            // If no Project is found, the project variable will remain null.
            // I agree, this try catch thingy is pretty horrible.
            try { iproject = await Database.i_project_collection.Find(x => x.ProjectID == _project.ProjectID).SingleAsync(); } catch { }
            try { eproject = await Database.e_project_collection.Find(x => x.ProjectID == _project.ProjectID).SingleAsync(); } catch { }
            // Find the user for whom the work is being created.
            User user = await Database.user_collection.Find(x => x.Name == _user.Name).SingleAsync();
            Work work = new Work(user, time, hours);
            // Add the work object to the appropriate project's work list, and add the user to the team if not already a member.
            if (eproject == null)
            {
                iproject.Works.Add(work);
                FilterDefinition<InternalProject> i_filter = Builders<InternalProject>.Filter.Eq(x => x.ProjectID, iproject.ProjectID);
                if (iproject.Team.FirstOrDefault(x => x.Name == user.Name) == null) iproject.Team.Add(user);
                // Replace the old project object with the updated one in the internal collection.
                await Database.i_project_collection.ReplaceOneAsync(i_filter, iproject);
            }
            else
            {
                eproject.Works.Add(work);
                FilterDefinition<ExternalProject> e_filter = Builders<ExternalProject>.Filter.Eq(x => x.ProjectID, eproject.ProjectID);
                if (eproject.Team.FirstOrDefault(x => x.Name == user.Name) == null) eproject.Team.Add(user);
                // Replace the old project object with the updated one in the external collection.
                await Database.e_project_collection.ReplaceOneAsync(e_filter, eproject);
            }
        }
    }
}
