using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Win32;
using System.IO;

namespace SimpleNotepad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            var newTab = new TabItem
            {
                Header = "Untitled",
                Content = new TextBox
                {
                    AcceptsReturn = true,
                    AcceptsTab = true,
                    FontSize = 14,
                    TextWrapping = Avalonia.Media.TextWrapping.Wrap
                }
            };
            TabView.Items.Add(newTab);
        }

        [System.Obsolete]
        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(this);
            if (result != null && result.Length > 0)
            {
                string path = result[0];
                string text = File.ReadAllText(path);
                var newTab = new TabItem
                {
                    Header = Path.GetFileName(path),
                    Content = new TextBox
                    {
                        Text = text,
                        AcceptsReturn = true,
                        AcceptsTab = true,
                        FontSize = 14,
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap
                    }
                };
                TabView.Items.Add(newTab);
            }
        }

        [System.Obsolete]
        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (TabView.SelectedItem is TabItem tab &&
                tab.Content is TextBox editor)
            {
                var dialog = new SaveFileDialog();
                var result = await dialog.ShowAsync(this);
                if (!string.IsNullOrEmpty(result))
                {
                    File.WriteAllText(result, editor.Text);
                    tab.Header = Path.GetFileName(result);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
