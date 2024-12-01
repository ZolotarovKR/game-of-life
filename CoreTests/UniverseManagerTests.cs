namespace CoreTests;

using Core;
public class UniverseManagerTests
{
    [Test]
    public void TestMoveForward__UpdatesUniverse()
    {
        var u = new Universe(10, 10);
        u[1, 1] = true;
        u[1, 2] = true;
        u[1, 3] = true;
        var m = new UniverseManager(u);
        m.MoveForward();
        Assert.That(u[0, 2], Is.True);
        Assert.That(u[1, 2], Is.True);
        Assert.That(u[2, 2], Is.True);
        for (int j = 0; j < m.Height; j++)
        {
            for (int i = 0; i < m.Width; i++)
            {
                if ((j == 0 || j == 1 || j == 2) && i == 2)
                {
                    continue;
                }
                Assert.That(u[j, i], Is.False);
            }
        }
        Assert.That(m.Born, Is.EqualTo(5));
        Assert.That(m.Died, Is.EqualTo(2));
        Assert.That(m.Generation, Is.EqualTo(1));
    }
    [Test]
    public void TestMoveBackward_SnapshotDoesntExist_ThrowsInvalidOperationException()
    {
        var m = new UniverseManager(new Universe(10, 10));
        Assert.Throws<InvalidOperationException>(m.MoveBackward);
    }
    [Test]
    public void TestMoveBackward__RestoresUniverse()
    {
        var u = new Universe(10, 10);
        u[1, 1] = true;
        u[1, 2] = true;
        u[1, 3] = true;
        var m = new UniverseManager(u);
        m.MoveForward();
        m.MoveBackward();
        Assert.That(u[1, 1], Is.True);
        Assert.That(u[1, 2], Is.True);
        Assert.That(u[1, 3], Is.True);
        for (int j = 0; j < m.Height; j++)
        {
            for (int i = 0; i < m.Width; i++)
            {
                if (j == 1 && (i == 1 || i == 2 || i == 3))
                {
                    continue;
                }
                Assert.That(u[j, i], Is.False);
            }
        }
        Assert.That(m.Born, Is.EqualTo(3));
        Assert.That(m.Died, Is.EqualTo(0));
        Assert.That(m.Generation, Is.EqualTo(0));
    }
}
