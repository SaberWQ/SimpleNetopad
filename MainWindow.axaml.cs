using Avalonia.Controls;
using Avalonia.Interactivity;
using System.IO;
using System.Threading.Tasks;

namespace SimpleNotepad;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent(); // Це працює, якщо x:Class у XAML вказано
    }

    private async void OpenFile_Click(object? sender, RoutedEventArgs e)
    {
        var file = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
        {
            Title = "Open text file",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new Avalonia.Platform.Storage.FilePickerFileType("Text Files") { Patterns = new[] { "*.txt" } }
            }
        });

        if (file.Count > 0)
        {
            var content = await File.ReadAllTextAsync(file[0].Path.LocalPath);
            Editor.Text = content;
        }
    }

    private async void SaveFile_Click(object? sender, RoutedEventArgs e)
    {
        var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
        {
            Title = "Save text file",
            SuggestedFileName = "NewFile.txt",
            FileTypeChoices = new[]
            {
                new Avalonia.Platform.Storage.FilePickerFileType("Text Files") { Patterns = new[] { "*.txt" } }
            }
        });

        if (file != null)
        {
            await File.WriteAllTextAsync(file.Path.LocalPath, Editor.Text);
        }
    }
}
