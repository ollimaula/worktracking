using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        // A method to show a specific user and their associated projects.
        public static async Task<string> ShowSpecificUser(string name)
        {
            // Create a StringBuilder to store the result string.
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== User Information ===\n");
            // Find the specified user in database.
            User user = await Database.user_collection.Find(x => x.Name == name).SingleAsync();
            // Find all internal and external projects that the user is part of.
            List<InternalProject> iprojects = await Database.i_project_collection.Find(x => x.Team.Contains(user)).ToListAsync();
            List<ExternalProject> eprojects = await Database.e_project_collection.Find(x => x.Team.Contains(user)).ToListAsync();
            // Concatenate the lists of projects and cast them to the base Project class.
            List<Project> projects = iprojects.Cast<Project>().Concat(eprojects.Cast<Project>()).ToList();
            // Append the user's information to the result string.
            sb.AppendLine(user.ToString());
            // Append the user's associated projects to the result string.
            sb.AppendLine("Projects:");
            foreach (Project project in projects)
                sb.AppendLine($" - {project.Name}");
            sb.AppendLine();
            // Return the result string.
            return sb.ToString();
        }
    }
}
