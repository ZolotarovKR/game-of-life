namespace Core;

public class Universe : IUniverse
{
    private Cell[,] Cells { get; }
    public int Height { get => Cells.GetLength(0); }
    public int Width { get => Cells.GetLength(1); }
    public int Born { get; private set; }
    public int Died { get; private set; }
    public int Generation { get; private set; }
    public bool this[int y, int x]
    {
        get => Cells[y, x].IsAlive;
        set => Cells[y, x].IsAlive = value;
    }

    public Universe(int height, int width)
    {
        if (height < 1 || width < 1)
        {
            throw new ArgumentOutOfRangeException();
        }
        Cells = new Cell[height, width];
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                Cells[j, i] = new Cell(false);
                Cells[j, i].OnBirth += () => Born++;
                Cells[j, i].OnDeath += () => Died++;
            }
        }
        Born = 0;
        Died = 0;
        Generation = 0;
    }

    public void Update()
    {
        int[,] ns = CountNeighbours();
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                Cells[j, i].Update(ns[j, i]);
            }
        }
        Generation++;
    }

    private int CountNeighboursOfCell(int y, int x)
    {
        int n = 0;
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dy == 0 && dx == 0)
                    continue;
                int j = y + dy == -1 ? Height - 1 : (y + dy == Height ? 0 : y + dy);
                int i = x + dx == -1 ? Width - 1 : (x + dx == Width ? 0 : x + dx);
                if (this[j, i])
                {
                    n += 1;
                }
            }
        }
        return n;
    }
    private int[,] CountNeighbours()
    {
        int[,] ns = new int[Height, Width];
        for (int j = 0; j < Height; j++)
        {
            for (int i = 0; i < Width; i++)
            {
                ns[j, i] = CountNeighboursOfCell(j, i);
            }
        }
        return ns;
    }

    private class Snapshot : IUniverseSnapshot
    {
        public Universe Originator { get; }
        public bool[,] AreAlive { get; }
        public int Height { get => AreAlive.GetLength(0); }
        public int Width { get => AreAlive.GetLength(1); }
        public int Born { get; }
        public int Died { get; }
        public int Generation { get; }

        public Snapshot(Universe originator)
        {
            Originator = originator;
            AreAlive = new bool[originator.Height, originator.Width];
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    AreAlive[j, i] = originator.Cells[j, i].IsAlive;
                }
            }
            Born = originator.Born;
            Died = originator.Died;
            Generation = originator.Generation;

        }

        public void Restore()
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Originator.Cells[j, i].IsAlive = AreAlive[j, i];
                }
            }
            Originator.Born = Born;
            Originator.Died = Died;
            Originator.Generation = Generation;

        }
    }

    internal IUniverseSnapshot Save() => new Snapshot(this);
}
