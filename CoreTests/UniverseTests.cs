namespace CoreTests;

using Core;

public class UniverseTests
{
    [Test]
    public void TestUniverse__AllCellsAreDead()
    {
        var u = new Universe(10, 10);
        for (int j = 0; j < u.Height; j++)
        {
            for (int i = 0; i < u.Width; i++)
            {
                Assert.That(u[j, i], Is.False);
            }
        }
    }
    [Test]
    public void TestUniverse_HeightIsZero_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Universe(0, 10));
    }
    [Test]
    public void TestUniverse_WidthIsZero_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Universe(10, 0));
    }
    [Test]
    public void TestHeight__EqualsToPassed()
    {
        var u = new Universe(10, 20);
        Assert.That(u.Height, Is.EqualTo(10));
    }
    [Test]
    public void TestWidth__EqualsToPassed()
    {
        var u = new Universe(10, 20);
        Assert.That(u.Width, Is.EqualTo(20));
    }
    [Test]
    public void TestUpdate_AllCellsAreAlive_AllCellsAreDead()
    {
        var u = new Universe(10, 10);
        for (int j = 0; j < u.Height; j++)
        {
            for (int i = 0; i < u.Width; i++)
            {
                u[j, i] = true;
            }
        }
        u.Update();
        for (int j = 0; j < u.Height; j++)
        {
            for (int i = 0; i < u.Width; i++)
            {
                Assert.That(u[j, i], Is.False);
            }
        }
    }
    [Test]
    public void TestUpdate__GenerationIsIncremented()
    {
        var u = new Universe(10, 10);
        u.Update();
        Assert.That(u.Generation, Is.EqualTo(1));
    }
}
