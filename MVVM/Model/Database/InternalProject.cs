using System.Diagnostics;

namespace TTC8440
{
    public class InternalProject : Project
    {
        public InternalProject(string name, User lead, int project_id)
            : base(name, lead, project_id)
        {
            Billable = false;
        }
        public override string ToString()
        {
            return $"Internal Project:\n\nProject name: {Name}\nProject lead: {Lead}\nProject ID: {ProjectID}";
        }
    }
}
