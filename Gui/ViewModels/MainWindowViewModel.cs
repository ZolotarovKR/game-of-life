using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media;
using Core;
using ReactiveUI;

namespace Gui.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public class Cell(int j, int i, bool isAlive)
    {
        static readonly int CELL_SIZE_AND_MARGIN = 35;

        public int J { get; set; } = j;
        public int I { get; set; } = i;
        public int Y { get; set; } = j * CELL_SIZE_AND_MARGIN;
        public int X { get; set; } = i * CELL_SIZE_AND_MARGIN;
        public bool IsAlive { get; set; } = isAlive;
        public IBrush Background { get; set; } = new SolidColorBrush(isAlive ? Colors.Green : Colors.Red);
    }

    private UniverseManager UniverseManager { get; }

    public List<Cell> Cells
    {
        get
        {
            var cells = new List<Cell>();
            for (int j = 0; j < UniverseManager.Height; j++)
            {
                for (int i = 0; i < UniverseManager.Width; i++)
                {
                    cells.Add(new Cell(j, i, UniverseManager[j, i]));
                }
            }
            return cells;
        }
    }
    public int Height { get => UniverseManager.Height; }
    public int Width { get => UniverseManager.Width; }
    public int Born { get => UniverseManager.Born; }
    public int Died { get => UniverseManager.Died; }
    public int Generation { get => UniverseManager.Generation; }

    public ReactiveCommand<(int j, int i), Unit> InverseCellCommand { get; }
    public ReactiveCommand<Unit, Unit> MoveForwardCommand { get; }
    public ReactiveCommand<Unit, Unit> MoveBackwardCommand { get; }

    public MainWindowViewModel()
    {
        UniverseManager = new UniverseManager(13, 18);
        InverseCellCommand = ReactiveCommand.Create<(int y, int x)>(coords => InverseCell(coords.y, coords.x));
        MoveForwardCommand = ReactiveCommand.Create(MoveForward);
        var canMoveBackward = this.WhenAnyValue(vm => vm.Generation, vm => vm.Cells, (g, cells) => UniverseManager.CanMoveBackward());
        MoveBackwardCommand = ReactiveCommand.Create(MoveBackward, canMoveBackward);
    }

    private void InverseCell(int y, int x)
    {
        UniverseManager[y, x] = !UniverseManager[y, x];
        UniverseChanged();
    }

    private void MoveForward()
    {
        UniverseManager.MoveForward();
        UniverseChanged();
    }

    private void MoveBackward()
    {
        UniverseManager.MoveBackward();
        UniverseChanged();
    }

    private void UniverseChanged()
    {
        this.RaisePropertyChanged(nameof(Born));
        this.RaisePropertyChanged(nameof(Died));
        this.RaisePropertyChanged(nameof(Generation));
        this.RaisePropertyChanged(nameof(Cells));
    }
}
