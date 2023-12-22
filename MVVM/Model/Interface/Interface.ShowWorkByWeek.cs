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
        public static async Task<int> ShowWorkByWeek(int week)
        {
            // Asynchronously retrieve all internal projects and external projects from the database.
            IAsyncCursor<InternalProject> iprojects = await Database.i_project_collection.FindAsync(_ => true);
            IAsyncCursor<ExternalProject> eprojects = await Database.e_project_collection.FindAsync(_ => true);
            // Convert the projects to a list of Project objects and combine them.
            List<Project> projects = iprojects.ToList().Cast<Project>().Concat(eprojects.ToList().Cast<Project>()).ToList();
            List<Work> works_during_week = new List<Work>();
            int total = 0;
            foreach (Project project in projects)
            {
                works_during_week = (project.Works.FindAll(x => x.Time > Periods.Weeks[week - 1].Item1 && x.Time < Periods.Weeks[week - 1].Item2));
                foreach (Work work in works_during_week) total += work.Hours;
            }
            return total;
        }
    }
}
