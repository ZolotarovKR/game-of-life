namespace Core;

public class UniverseManager : IUniverse
{
    private Universe Universe { get; }
    private List<IUniverseSnapshot> Snapshots { get; set; }
    public int Height { get => Universe.Height; }
    public int Width { get => Universe.Width; }
    public int Born { get => Universe.Born; }
    public int Died { get => Universe.Died; }
    public int Generation { get => Universe.Generation; }
    public bool this[int y, int x]
    {
        get => Universe[y, x];
        set
        {
            Snapshots.Clear();
            Universe[y, x] = value;
        }
    }

    public UniverseManager(int height, int width)
    {
        Universe = new Universe(height, width);
        Snapshots = [];
    }

    public void MoveForward()
    {
        Snapshots.Add(Universe.Save());
        Universe.Update();
    }
    public void MoveBackward()
    {
        if (Snapshots.Count == 0)
        {
            throw new InvalidOperationException();
        }
        Snapshots[Snapshots.Count - 1].Restore();
        Snapshots.RemoveAt(Snapshots.Count - 1);
    }

    public bool CanMoveBackward() => Snapshots.Count != 0;
}
