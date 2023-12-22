using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TTC8440.MVVM.ViewModel;

namespace TTC8440.MVVM.View
{
    public partial class UserView : UserControl
    {
        public UserView() { }
        public UserView(string button_name)
        {
            InitializeComponent();
            RadioButton home_rb = (RadioButton)Application.Current.MainWindow.FindName("HomeRadioButton");
            home_rb.IsChecked = false;
            Task.Run(() => CallUsers(button_name));
        }
        private async Task CallUsers(string button_name)
        {
            string result;
            Dispatcher.Invoke(() =>
            {
                if (button_name == "All") Title.Text = "=== All Users ===";
                else Title.Text = "=== " + button_name + " ===";
                TextBlock text_block = new TextBlock
                {
                    Foreground = Brushes.AntiqueWhite,
                    Text = "Accessing database..."
                };
                UserPanel.Children.Add(text_block);
            });
            await Dispatcher.Invoke(async () =>
            {
                LinearGradientBrush brush = new LinearGradientBrush
                (
                    new GradientStopCollection()
                    {
                        new GradientStop((Color)ColorConverter.ConvertFromString("#5bc3ff"), 0.0),
                        new GradientStop((Color)ColorConverter.ConvertFromString("#3aa0ff"), 1.0)
                    },
                    new Point(0, 0),
                    new Point(1, 2)
                );
                Border border = new Border
                {
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Background = brush,
                    Margin = new Thickness(10, 10, 40, 10)
                };
                TextBlock text_block = new TextBlock();
                border.Child = text_block;
                UserPanel.Children.Add(border);
                if (button_name == "All")
                {
                    result = await Interface.ShowAllUsers();
                    result += await Interface.ShowUserWork("Tupu Ankka");
                    result += "\n";
                    result += await Interface.ShowUserWork("Hupu Ankka");
                    result += "\n";
                    result += await Interface.ShowUserWork("Lupu Ankka");
                    text_block.Text = result;
                }
                else if (button_name == "Tupu")
                {
                    result = await Interface.ShowSpecificUser("Tupu Ankka");
                    result += await Interface.ShowUserWork("Tupu Ankka");
                    text_block.Text = result;
                }
                else if (button_name == "Hupu")
                {
                    result = await Interface.ShowSpecificUser("Hupu Ankka");
                    result += await Interface.ShowUserWork("Hupu Ankka");
                    text_block.Text = result;
                }
                else if (button_name == "Lupu")
                {
                    result = await Interface.ShowSpecificUser("Lupu Ankka");
                    result += await Interface.ShowUserWork("Lupu Ankka");
                    text_block.Text = result;
                }
                UserPanel.Children.RemoveAt(0);
            });
        }
    }
}
