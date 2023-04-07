using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace S1.View;

public partial class RecordTypeWindow : Window
{
    public RecordTypeWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ToSoloRecord(object? sender, RoutedEventArgs e)
    {
        SoloRecordWindow soloRecordWindow = new SoloRecordWindow();
        soloRecordWindow.Show();
        this.Close();
    }
}