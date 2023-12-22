using MongoDB.Driver.Core.Operations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace TTC8440.MVVM.View
{
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
            Task.Run(() => CallProjects());
        }
        private async Task CallProjects()
        {
            Dispatcher.Invoke(() =>
            {
                TextBlock text_block = new TextBlock 
                {
                    Foreground = Brushes.AntiqueWhite,
                    Text = "Accessing database..." 
                };
                ProjectPanel.Children.Add(text_block);
            });
            string[] projects = await Interface.ShowAllProjects();
            CreateBorders(projects);
        }
        public void CreateBorders(string[] projects)
        {
            Dispatcher.Invoke(() => {
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
                foreach (string project in projects)
                {
                    Border border = new Border
                    {
                        Background = brush,
                        Margin = new Thickness(0, 10, 10, 10),
                        CornerRadius = new CornerRadius(10),
                        Padding = new Thickness(10),
                        MaxWidth = 600
                    };
                    TextBlock text_block = new TextBlock
                    {
                        Foreground = new SolidColorBrush(Color.FromRgb(18,18,18)),
                        Text = project
                    };
                    border.Child = text_block;
                    ProjectPanel.Children.Add(border);
                }
                ProjectPanel.Children.RemoveAt(0);
            });
        }
    }
}
