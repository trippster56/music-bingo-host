using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Win32;
using MusicBingoHost.Models;
using MusicBingoHost.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MusicBingoHost.ViewModels;

public partial class RoundSetupViewModel : ObservableObject
{
    [ObservableProperty]
    private string selectedFolderPath = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string loadingStatus = string.Empty;

    [ObservableProperty]
    private int totalTracks;

    [ObservableProperty]
    private int supportedTracks;

    [ObservableProperty]
    private int unsupportedTracks;

    [ObservableProperty]
    private TimeSpan loadTime;

    public ObservableCollection<Track> Tracks { get; } = new();
    public ObservableCollection<Track> UnsupportedFiles { get; } = new();

    private readonly AudioMetadataService _audioMetadataService = new();

    public RoundSetupViewModel()
    {
        SelectFolderCommand = new AsyncRelayCommand(SelectFolderAsync);
    }

    public ICommand SelectFolderCommand { get; }

    private async Task SelectFolderAsync()
    {
        var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = "Select Music Folder",
            UseDescriptionForTitle = true
        };

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            SelectedFolderPath = dialog.SelectedPath;
            await LoadTracksAsync();
        }
    }

    private async Task LoadTracksAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedFolderPath))
            return;

        IsLoading = true;
        LoadingStatus = "Loading tracks...";
        
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var tracks = await _audioMetadataService.LoadTracksFromFolderAsync(SelectedFolderPath);
            
            stopwatch.Stop();
            LoadTime = stopwatch.Elapsed;

            Tracks.Clear();
            UnsupportedFiles.Clear();

            var supportedList = tracks.Where(t => t.IsSupported).ToList();
            var unsupportedList = tracks.Where(t => !t.IsSupported).ToList();

            foreach (var track in supportedList)
                Tracks.Add(track);

            foreach (var track in unsupportedList)
                UnsupportedFiles.Add(track);

            TotalTracks = tracks.Count;
            SupportedTracks = supportedList.Count;
            UnsupportedTracks = unsupportedList.Count;

            LoadingStatus = $"Loaded {SupportedTracks} tracks in {LoadTime.TotalMilliseconds:F0}ms";
        }
        catch (Exception ex)
        {
            LoadingStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
