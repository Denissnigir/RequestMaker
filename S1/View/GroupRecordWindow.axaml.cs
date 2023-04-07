using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace S1.View;

public partial class GroupRecordWindow : Window
{
    public GroupRecordWindow()
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
}