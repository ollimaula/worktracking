using MongoDB.Driver;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        // This method returns a string containing the total number of hours worked on a project.
        public static async Task<string> ShowProjectWork(int project_id)
        {
            Debug.WriteLine(project_id);
            InternalProject iproject = null;
            ExternalProject eproject = null;
            // Try to find a Project in the database with the given project ID.
            // If no Project is found, the project variable will remain null.
            try { iproject = await Database.i_project_collection.Find(x => x.ProjectID == project_id).SingleAsync(); } catch { }
            try { eproject = await Database.e_project_collection.Find(x => x.ProjectID == project_id).SingleAsync(); } catch { }
            Project project;
            // Determine which type of project was found and set the project variable accordingly.
            if (eproject == null) project = iproject;
            else project = eproject;
            int total_work = 0;
            // Calculate the total number of hours worked on the project.
            foreach (Work work in project.Works) total_work += work.Hours;
            return $"{total_work} hours worked on Project: \"{project.Name}\"";
        }
    }
}
