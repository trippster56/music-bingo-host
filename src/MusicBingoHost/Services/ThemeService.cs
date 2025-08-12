using System.IO;
using System.Text.Json;
using System.Windows;

namespace MusicBingoHost.Services;

public class ThemeService
{
    private const string SettingsFileName = "settings.json";
    private readonly string _settingsPath;

    public ThemeService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appDataPath, "MusicBingoHost");
        Directory.CreateDirectory(appFolder);
        _settingsPath = Path.Combine(appFolder, SettingsFileName);
    }

    public bool IsDarkTheme { get; private set; }

    public void LoadSavedTheme()
    {
        try
        {
            if (File.Exists(_settingsPath))
            {
                var json = File.ReadAllText(_settingsPath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);
                IsDarkTheme = settings?.IsDarkTheme ?? false;
            }
        }
        catch
        {
            IsDarkTheme = false;
        }

        ApplyTheme();
    }

    public void SetTheme(bool isDark)
    {
        IsDarkTheme = isDark;
        ApplyTheme();
        SaveSettings();
    }

    private void ApplyTheme()
    {
        var app = Application.Current;
        app.Resources.MergedDictionaries.Clear();
        
        var themeUri = IsDarkTheme 
            ? new Uri("Themes/DarkTheme.xaml", UriKind.Relative)
            : new Uri("Themes/LightTheme.xaml", UriKind.Relative);
            
        app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = themeUri });
    }

    private void SaveSettings()
    {
        try
        {
            var settings = new AppSettings { IsDarkTheme = IsDarkTheme };
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
        }
        catch
        {
            // Ignore save errors
        }
    }

    private class AppSettings
    {
        public bool IsDarkTheme { get; set; }
    }
}
