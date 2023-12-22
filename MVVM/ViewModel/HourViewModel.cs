using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using TTC8440.Core;
using TTC8440.MVVM.Model;
using TTC8440.MVVM.View;

namespace TTC8440.MVVM.ViewModel
{
    internal class HourViewModel : ObservableObject
    {
        public ObservableCollection<WeekButton> Buttons { get; set; }
        private ICommand _button_command;
        public ICommand ButtonCommand
        {
            get
            {
                if (_button_command == null)
                {
                    _button_command = new RelayCommand(param => ButtonClicked(param));
                }
                return _button_command;
            }
        }
        public static bool IsTupu { get; set; }
        public static bool IsHupu { get; set; }
        public static bool IsLupu { get; set; }
        public static int ButtonNumber { get; set; }
        public static int ProjectId { get; set; }
        private void ButtonClicked(object param)
        {
            ButtonNumber = Convert.ToInt32(param);
            HourModal modal = new HourModal();
            modal.ShowDialog();
        }
        public HourViewModel()
        {
            IsTupu = true; 
            Buttons = new ObservableCollection<WeekButton>();
            for (int i = 1; i < 53; i++)
            {
                Buttons.Add(new WeekButton { Label = $"Week {i}", Name = $"{i}" });
            }
        }
    }
}
