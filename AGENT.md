# Music Bingo Host - Agent Instructions

## Build Commands
- **Build**: `dotnet build`
- **Run**: `dotnet run --project src/MusicBingoHost`
- **Restore**: `dotnet restore`

## Project Structure
- **Solution**: `MusicBingoHost.sln`
- **Main Project**: `src/MusicBingoHost/`
- **ViewModels**: MVVM pattern with Observable properties
- **Services**: Business logic (ThemeService, AudioMetadataService)
- **Models**: Data models (Track)
- **Views**: UserControls for complex UI
- **Themes**: Light/Dark theme resources

## Technology Stack
- **.NET 8.0** with WPF
- **MVVM** with Microsoft.Toolkit.Mvvm
- **TagLibSharp** for audio metadata extraction
- **System.Windows.Forms** for folder dialog

## Key Features
- Theme persistence in `%AppData%/MusicBingoHost/settings.json`
- Parallel track loading for performance
- Support for: MP3, MP4, M4A, WMA, WAV, FLAC, OGG, AAC
- Error handling for unsupported/corrupted files

## Performance Requirements
- Load ≥200 tracks in <2s (Issue #2)
- Use parallel processing for metadata extraction
