using System.Windows;
using MusicBingoHost.ViewModels;

namespace MusicBingoHost;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
