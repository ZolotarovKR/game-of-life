namespace CoreTests;

using Core;
public class UniverseManagerTests
{
    [Test]
    public void TestMoveForward__UpdatesUniverse()
    {
        var m = new UniverseManager(10, 10);
        m[1, 2] = true;
        m[1, 1] = true;
        m[1, 3] = true;
        m.MoveForward();
        Assert.That(m[0, 2], Is.True);
        Assert.That(m[1, 2], Is.True);
        Assert.That(m[2, 2], Is.True);
        for (int j = 0; j < m.Height; j++)
        {
            for (int i = 0; i < m.Width; i++)
            {
                if ((j == 0 || j == 1 || j == 2) && i == 2)
                {
                    continue;
                }
                Assert.That(m[j, i], Is.False);
            }
        }
        Assert.That(m.Born, Is.EqualTo(5));
        Assert.That(m.Died, Is.EqualTo(2));
        Assert.That(m.Generation, Is.EqualTo(1));
    }
    [Test]
    public void TestMoveBackward_SnapshotDoesntExist_ThrowsInvalidOperationException()
    {
        var m = new UniverseManager(10, 10);
        Assert.Throws<InvalidOperationException>(m.MoveBackward);
    }
    [Test]
    public void TestMoveBackward__RestoresUniverse()
    {
        var m = new UniverseManager(10, 10);
        m[1, 1] = true;
        m[1, 2] = true;
        m[1, 3] = true;
        m.MoveForward();
        m.MoveBackward();
        Assert.That(m[1, 1], Is.True);
        Assert.That(m[1, 2], Is.True);
        Assert.That(m[1, 3], Is.True);
        for (int j = 0; j < m.Height; j++)
        {
            for (int i = 0; i < m.Width; i++)
            {
                if (j == 1 && (i == 1 || i == 2 || i == 3))
                {
                    continue;
                }
                Assert.That(m[j, i], Is.False);
            }
        }
        Assert.That(m.Born, Is.EqualTo(3));
        Assert.That(m.Died, Is.EqualTo(0));
        Assert.That(m.Generation, Is.EqualTo(0));
    }
    [Test]
    public void TestSetCellStatus_SnapshotsExist_RemovesSnapshots()
    {
        var m = new UniverseManager(10, 10);
        m[1, 1] = true;
        m[1, 2] = true;
        m[1, 3] = true;
        m.MoveForward();
        m.MoveForward();
        m[1, 0] = true;
        Assert.Throws<InvalidOperationException>(m.MoveBackward);
    }
}
