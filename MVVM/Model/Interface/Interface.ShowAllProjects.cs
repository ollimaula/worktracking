using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        // This method asynchronously retrieves all projects from the database and returns a string representation of them.
        public static async Task<string[]> ShowAllProjects()
        {
            // Asynchronously retrieve all internal projects and external projects from the database.
            IAsyncCursor<InternalProject> iprojects = await Database.i_project_collection.FindAsync(_ => true);
            IAsyncCursor<ExternalProject> eprojects = await Database.e_project_collection.FindAsync(_ => true);
            // Convert the projects to a list of Project objects and combine them.
            List<Project> projects = iprojects.ToList().Cast<Project>().Concat(eprojects.ToList().Cast<Project>()).ToList();
            string[] output = new string[projects.Count];
            // Iterate over the projects and create a string representation for each one.
            for (int i = 0; i < projects.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(projects[i].ToString());
                sb.AppendLine("\nTeam members:");
                // Iterate over the team members and add them to the output string.
                foreach (User user in projects[i].Team)
                    sb.AppendLine($" - {user}");
                output[i] = sb.ToString();
            }
            return output;
        }
    }
}
