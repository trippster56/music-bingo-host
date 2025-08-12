using System.Windows;

namespace MusicBingoHost;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var themeService = new Services.ThemeService();
        themeService.LoadSavedTheme();
    }
}
