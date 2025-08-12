namespace MusicBingoHost.Models;

public class Track
{
    public required string FilePath { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Artist { get; init; } = string.Empty;
    public TimeSpan Duration { get; init; }
    public string FileName => Path.GetFileName(FilePath);
    public bool IsSupported { get; init; } = true;
    public string? UnsupportedReason { get; init; }
}
