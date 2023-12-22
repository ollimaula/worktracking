using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows;
using task43;
using TTC8440.Core;
using TTC8440.MVVM.View;
using System.Linq;

namespace TTC8440.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProjectViewCommand { get; set; }
        public RelayCommand HourViewCommand { get; set; }
        public RelayCommand UserViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public HourView HourVM { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            HourVM = new HourView();
            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommand(obj =>
            {
                CurrentView = HomeVM;
            });
            ProjectViewCommand = new RelayCommand(obj =>
            {
                CurrentView = new ProjectView();
            });
            HourViewCommand = new RelayCommand(obj =>
            {
                CurrentView = HourVM;
            });
            UserViewCommand = new RelayCommand(obj =>
            {
                ShowUserView(obj as string);
            });
        }
        private void ShowUserView(string button_name)
        {
            // Set the CurrentView property of the MainViewModel to the UserViewModel
            MainViewModel current = App.Current.MainWindow.DataContext as MainViewModel;
            current.CurrentView = new UserView(button_name);
        }
    }
}
