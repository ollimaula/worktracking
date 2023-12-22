using MongoDB.Driver;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TTC8440.Core;

namespace TTC8440.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        private RelayCommand _add_hours;
        private RelayCommand _add_project;
        private string _hour_output;
        private string _project_output;
        public string HourOutput
        {
            get { return _hour_output; }
            set
            {
                _hour_output = value;
                OnPropertyChanged(nameof(HourOutput));
            }
        }
        public string ProjectOutput
        {
            get { return _project_output; }
            set
            {
                _project_output = value;
                OnPropertyChanged(nameof(ProjectOutput));
            }
        }
        public string Hours { get; set; }
        public string ProjectId { get; set; }
        public string Client { get; set; }
        public string ProjectName { get; set; }
        public bool IsExternalProject { get; set; }
        public bool IsInternalProject { get; set; }
        public bool IsTupuHour { get; set; }
        public bool IsHupuHour { get; set; }
        public bool IsLupuHour { get; set; }
        public bool IsTupuProject { get; set; }
        public bool IsHupuProject { get; set; }
        public bool IsLupuProject { get; set; }
        public HomeViewModel()
        {
            IsTupuHour = true;
            IsTupuProject = true;
            IsInternalProject = true;
        }
        public ICommand AddHours
        {
            get
            {
                if (_add_hours == null) _add_hours = new RelayCommand(async (object parameter) => 
                {
                    HourOutput = "Adding hours...";
                    await PerformAddHours(); 
                });
                return _add_hours;
            }
        }
        public ICommand AddProject
        {
            get
            {
                if (_add_project == null) _add_project = new RelayCommand(async (object parameter) =>
                {
                    ProjectOutput = "Creating project...";
                    await PerformAddProject();
                });
                return _add_project;
            }
        }
        private async Task PerformAddHours()
        {
            string name = "";
            if (IsTupuHour) name = "Tupu Ankka";
            else if (IsHupuHour) name = "Hupu Ankka";
            else if (IsLupuHour) name = "Lupu Ankka";
            try
            {
                int pid = Convert.ToInt32(ProjectId);
                int hours = Convert.ToInt32(Hours);
                User user = await Database.user_collection.Find(x => x.Name == name).SingleAsync();
                InternalProject iproject = null;
                ExternalProject eproject = null;
                Project project;
                // Try to find a Project in the database with the given project ID.
                // If no Project is found, the project variable will remain null.
                try { iproject = await Database.i_project_collection.Find(x => x.ProjectID == pid).SingleAsync(); } catch { }
                try { eproject = await Database.e_project_collection.Find(x => x.ProjectID == pid).SingleAsync(); } catch { }
                // Determine which type of project was found and set the project variable accordingly.
                if (eproject == null) project = iproject;
                else project = eproject;
                await Interface.CreateWork(project, user, DateTime.Now, hours);
                HourOutput = $"{hours} hours of work added for {user.Name} on project \"{project.Name}\".";
            }
            catch (System.FormatException) { HourOutput = "Incorrect input!"; }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.Message);
                HourOutput = "Something went wrong...";
            }
        }
        private async Task PerformAddProject()
        {
            if (ProjectName == null)
            {
                ProjectOutput = "Give your project a name!";
                return;
            }
            string name = "";
            if (IsTupuProject) name = "Tupu Ankka";
            else if (IsHupuProject) name = "Hupu Ankka";
            else if (IsLupuProject) name = "Lupu Ankka";
            try
            {
                if (IsInternalProject)
                {
                    await Interface.CreateInternalProject(ProjectName, name);
                    ProjectOutput = $"Project \"{ProjectName}\" created with {name} as lead.";
                }
                else if (IsExternalProject)
                {
                    if (Client ==  null)
                    {
                        ProjectOutput = "Give your project a client!";
                        return;
                    }
                    await Interface.CreateExternalProject(Client, ProjectName, name);
                    ProjectOutput = $"Project \"{ProjectName}\" created for client {Client} with {name} as lead.";
                }
            }
            catch { ProjectOutput = "Something went wrong..."; }
        }
    }
}
