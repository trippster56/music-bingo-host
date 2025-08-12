using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace MusicBingoHost.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int selectedTabIndex;

    [ObservableProperty]
    private bool isDarkTheme;

    private readonly Services.ThemeService _themeService;

    public MainViewModel()
    {
        _themeService = new Services.ThemeService();
        isDarkTheme = _themeService.IsDarkTheme;
        ToggleThemeCommand = new RelayCommand(ToggleTheme);
    }

    public ICommand ToggleThemeCommand { get; }

    private void ToggleTheme()
    {
        IsDarkTheme = !IsDarkTheme;
        _themeService.SetTheme(IsDarkTheme);
    }
}
