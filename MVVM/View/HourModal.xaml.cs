using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TTC8440.MVVM.ViewModel;

namespace TTC8440.MVVM.View
{
    public partial class HourModal : Window
    {
        public HourModal()
        {
            InitializeComponent();
            // Set the owner of the modal window to the current window
            Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
    }
}
