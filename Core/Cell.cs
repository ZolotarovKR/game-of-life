namespace Core;

public class Cell
{
    private bool _isAlive;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if (_isAlive && !value)
            {
                OnDeath?.Invoke();
            }
            else if (!_isAlive && value)
            {
                OnBirth?.Invoke();
            }
            _isAlive = value;
        }
    }
    public event Action? OnDeath;
    public event Action? OnBirth;

    public Cell(bool isAlive)
    {
        _isAlive = isAlive;
    }

    public void Update(int numberOfNeighbours)
    {
        if (IsAlive && numberOfNeighbours != 2 && numberOfNeighbours != 3)
        {
            IsAlive = false;
        }
        else if (!IsAlive && numberOfNeighbours == 3)
        {
            IsAlive = true;
        }
    }
}
