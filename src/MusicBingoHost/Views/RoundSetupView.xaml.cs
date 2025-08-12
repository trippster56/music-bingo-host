using System.Windows.Controls;
using MusicBingoHost.ViewModels;

namespace MusicBingoHost.Views;

public partial class RoundSetupView : UserControl
{
    public RoundSetupView()
    {
        InitializeComponent();
        DataContext = new RoundSetupViewModel();
    }
}
