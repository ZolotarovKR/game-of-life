namespace Core;

public class UniverseManager : IUniverse
{
    private Universe Universe { get; }
    private List<IUniverseSnapshot> Snapshots { get; set; }
    private int Idx { get; set; }

    public int Height { get => Universe.Height; }
    public int Width { get => Universe.Width; }
    public int Born { get => Universe.Born; }
    public int Died { get => Universe.Died; }
    public int Generation { get => Universe.Generation; }
    public bool this[int y, int x] { get => Universe[y, x]; }

    public UniverseManager(Universe universe)
    {
        Universe = universe;
        Snapshots = [universe.Save()];
        Idx = 0;
    }

    public void MoveForward()
    {
        if (Idx == Snapshots.Count - 1)
        {
            Universe.Update();
            Snapshots.Add(Universe.Save());
            Idx += 1;
            return;
        }
        Idx += 1;
        Snapshots[Idx].Restore();

    }
    public void MoveBackward()
    {
        if (Idx == 0)
        {
            throw new InvalidOperationException();
        }
        Idx -= 1;
        Snapshots[Idx].Restore();

    }
}
