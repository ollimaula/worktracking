using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTC8440
{
    public static partial class Interface
    {
        private static async Task<int> GetNextId()
        {
            // This method retrieves the highest project ID from both the internal and external project collections
            // This ensures that the next project created will have a unique ID that is higher than any existing project ID.
            IAsyncCursor<InternalProject> iprojects = await Database.i_project_collection.FindAsync(_ => true);
            IAsyncCursor<ExternalProject> eprojects = await Database.e_project_collection.FindAsync(_ => true);
            List<Project> projects = iprojects.ToList().Cast<Project>().Concat(eprojects.ToList().Cast<Project>()).ToList();
            return ++projects.OrderByDescending(x => x.ProjectID).FirstOrDefault().ProjectID;
        }
    }
}
