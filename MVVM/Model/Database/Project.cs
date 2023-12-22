using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TTC8440
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int ProjectID;
        public bool Billable;
        public User Lead { get; set; }
        public List<User> Team;
        public List<Work> Works;
        public Project(string name, User lead, int project_id)
        {
            Name = name;
            ProjectID = project_id;
            Lead = lead;
            Team = new List<User>();
            Works = new List<Work>();
        }
    }
}
