using System.Collections.Concurrent;
using MusicBingoHost.Models;
using TagLib;

namespace MusicBingoHost.Services;

public class AudioMetadataService
{
    private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".mp3", ".mp4", ".m4a", ".wma", ".wav", ".flac", ".ogg", ".aac"
    };

    public async Task<List<Track>> LoadTracksFromFolderAsync(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

        var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
            .Where(f => SupportedExtensions.Contains(Path.GetExtension(f)))
            .ToArray();

        var tracks = new ConcurrentBag<Track>();

        await Task.Run(() =>
        {
            Parallel.ForEach(files, file =>
            {
                try
                {
                    var track = ExtractMetadata(file);
                    tracks.Add(track);
                }
                catch (Exception ex)
                {
                    tracks.Add(new Track
                    {
                        FilePath = file,
                        IsSupported = false,
                        UnsupportedReason = ex.Message
                    });
                }
            });
        });

        return tracks.OrderBy(t => t.Artist).ThenBy(t => t.Title).ToList();
    }

    private static Track ExtractMetadata(string filePath)
    {
        try
        {
            using var file = TagLib.File.Create(filePath);
            
            return new Track
            {
                FilePath = filePath,
                Title = file.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
                Artist = file.Tag.FirstPerformer ?? "Unknown Artist",
                Duration = file.Properties.Duration,
                IsSupported = true
            };
        }
        catch (UnsupportedFormatException)
        {
            return new Track
            {
                FilePath = filePath,
                Title = Path.GetFileNameWithoutExtension(filePath),
                Artist = "Unknown Artist",
                IsSupported = false,
                UnsupportedReason = "Unsupported audio format"
            };
        }
        catch (CorruptFileException)
        {
            return new Track
            {
                FilePath = filePath,
                Title = Path.GetFileNameWithoutExtension(filePath),
                Artist = "Unknown Artist",
                IsSupported = false,
                UnsupportedReason = "Corrupted file"
            };
        }
    }

    public static bool IsAudioFile(string filePath)
    {
        return SupportedExtensions.Contains(Path.GetExtension(filePath));
    }
}
