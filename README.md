# Music Bingo Host 1.0

A WPF application for hosting music bingo games with MVVM architecture.

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code with C# extension

## Build and Run

```bash
dotnet restore
dotnet build
dotnet run --project src/MusicBingoHost
```

## Features

- **Navigation Tabs**: Round Setup, Cards, Run, Settings
- **Theme Support**: Light/Dark theme with persistence
- **MVVM Architecture**: Clean separation of concerns
- **Settings Persistence**: App preferences saved locally

## Project Structure

```
src/
├── MusicBingoHost/
│   ├── ViewModels/          # MVVM ViewModels
│   ├── Views/               # Additional views
│   ├── Services/            # Business logic services
│   ├── Themes/              # Light/Dark theme resources
│   ├── MainWindow.xaml      # Main application window
│   └── App.xaml             # Application entry point
```

## Development

This project implements Issue #1 requirements:
- ✅ WPF app with MVVM structure
- ✅ Navigation tabs (Round Setup | Cards | Run | Settings)
- ✅ Light/dark theme with persistence
- ✅ App builds and runs with functional tab switching
