using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        // ShowUserWork method returns total work hours of the specified user across all the projects
        public static async Task<string> ShowUserWork(string _name)
        {
            // Get the user from the database by name
            User user = await Database.user_collection.Find(x => x.Name == _name).SingleAsync();
            // Find all the internal and external projects that the user is a part of
            List<InternalProject> iprojects = await Database.i_project_collection.Find(x => x.Team.Contains(user)).ToListAsync();
            List<ExternalProject> eprojects = await Database.e_project_collection.Find(x => x.Team.Contains(user)).ToListAsync();
            // Combine internal and external projects into a single list of projects
            List<Project> projects = iprojects.Cast<Project>().Concat(eprojects.Cast<Project>()).ToList();
            // Calculate the total work hours for the user across all the projects
            int total_work = 0;
            foreach (Project project in projects)
                foreach (Work work in project.Works)
                    if (work.User.Name == user.Name) total_work += work.Hours;
            // Return the total work hours for the user as a formatted string
            return $"{total_work} hours worked by {user.Name}";
        }
    }
}
