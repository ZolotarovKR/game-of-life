namespace Core;

internal interface IUniverse
{
    public int Height { get; }
    public int Width { get; }
    public int Born { get; }
    public int Died { get; }
    public int Generation { get; }
    public bool this[int y, int x] { get; }
}
