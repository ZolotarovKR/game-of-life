using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Gui.ViewModels;

namespace Gui.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CellClickedHandler(object sender, PointerPressedEventArgs args)
    {
        if (sender is Rectangle r && r.DataContext is MainWindowViewModel.Cell c)
        {
            SendInverseCellCommand(c.J, c.I);
        }
    }
    private void SendInverseCellCommand(int j, int i)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.InverseCellCommand.Execute((j, i)).Subscribe();
        }
    }
}