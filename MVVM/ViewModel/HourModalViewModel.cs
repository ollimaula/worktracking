using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Schema;
using TTC8440.Core;
using TTC8440.MVVM.View;

namespace TTC8440.MVVM.ViewModel
{
    internal class HourModalViewModel : ObservableObject
    {
        private string _output;
        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged(nameof(Output));
            }
        }
        public HourModalViewModel() 
        {
            if (HourViewModel.ButtonNumber == -1)
            {
                ShowTotalWorkByProject();
            }
            else if (HourViewModel.ButtonNumber == -2)
            {
                ShowTotalWorkByUser();
            }
            else ShowTotalWorkByWeek(HourViewModel.ButtonNumber);
        }
        private async void ShowTotalWorkByWeek(int week)
        {
            Output = "Loading...";
            int total = await Interface.ShowWorkByWeek(week);
            Output = $"Total of {total} hours of work done in week {week}";
        }
        private async void ShowTotalWorkByProject()
        {
            try
            {
                Output = "Loading...";
                Output = await Interface.ShowProjectWork(HourViewModel.ProjectId);
            }
            catch
            {
                Output = "Something went wrong!";
            }
        }
        private async void ShowTotalWorkByUser()
        {
            string query;
            if (HourViewModel.IsTupu) query = "Tupu Ankka";
            else if (HourViewModel.IsHupu) query = "Hupu Ankka";
            else query = "Lupu Ankka";
            Output = "Loading...";
            Output = await Interface.ShowUserWork(query);
        }
        private ICommand _button_command;
        public ICommand ButtonCommand
        {
            get
            {
                if (_button_command == null)
                {
                    _button_command = new RelayCommand(_ => CloseModal());
                }
                return _button_command;
            }
        }
        public void CloseModal()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }
    }
}
