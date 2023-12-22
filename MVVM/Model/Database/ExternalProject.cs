namespace TTC8440
{
    public class ExternalProject : Project
    {
        public string Client;
        public ExternalProject(string client, string name, User lead, int project_id)
            : base(name, lead, project_id)
        {
            Client = client;
            Billable = true;
        }
        public override string ToString()
        {
            return $"External Project:\n\nProject name: {Name}\nClient: {Client}\nProject lead: {Lead}\nProject ID: {ProjectID}";
        }
    }
}
