using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TTC8440.MVVM.View;

namespace TTC8440
{
    public partial class MainWindow : Window
    {
        public MainWindow() { }
        private void CustomTitleBar_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Get the window associated with the border
                Window window = Window.GetWindow((DependencyObject)sender);
                window.DragMove();
            }
        }
        private void CustomTitleBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set the focus to the title bar so that we can drag the window even if the mouse is not over the content
            ((UIElement)sender).Focus();
        }
        private void MinimizeButton_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeButton_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            // tää höskä viewmodeliin? ->
            MaximizeModal modal = new MaximizeModal();
            modal.ShowDialog();
        }
        private void ExitButton_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
