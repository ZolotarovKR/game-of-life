namespace CoreTests;

using Core;

public class CellTests
{
    [Test]
    public void TestUpdate_IsAliveAndHasMoreThatThreeNeighbours_Dies()
    {
        var c = new Cell(true);
        c.Update(4);
        Assert.That(c.IsAlive, Is.False);
    }
    [Test]
    public void TestUpdate_IsAliveAndHasLessThatTwoNeighbours_Dies()
    {
        var c = new Cell(true);
        c.Update(1);
        Assert.That(c.IsAlive, Is.False);
    }
    [Test]
    public void TestUpdate_IsAliveAndHasTwoNeighbours_Survives()
    {
        var c = new Cell(true);
        c.Update(2);
        Assert.That(c.IsAlive, Is.True);
    }
    [Test]
    public void TestUpdate_IsAliveAndHasThreeNeighbours_Survives()
    {
        var c = new Cell(true);
        c.Update(3);
        Assert.That(c.IsAlive, Is.True);
    }
    [Test]
    public void TestUpdate_IsDeadAndHasMoreThatThreeNeighbours_StaysDead()
    {
        var c = new Cell(false);
        c.Update(4);
        Assert.That(c.IsAlive, Is.False);
    }
    [Test]
    public void TestUpdate_IsDeadAndHasLessThatThreeNeighbours_StaysDead()
    {
        var c = new Cell(false);
        c.Update(2);
        Assert.That(c.IsAlive, Is.False);
    }
    [Test]
    public void TestUpdate_IsDeadAndHasThreeNeighbours_Resurrects()
    {
        var c = new Cell(false);
        c.Update(3);
        Assert.That(c.IsAlive, Is.True);
    }
    [Test]
    public void TestIsAlive_IsAliveAndDies_InvokesOnDeathEvent()
    {
        var c = new Cell(true);
        var invoked = false;
        c.OnDeath += () => invoked = true;
        c.IsAlive = false;
        Assert.That(invoked, Is.True);
    }
    [Test]
    public void TestIsAlive_IsDeadAndResurrects_InvokesOnBirthEvent()
    {
        var c = new Cell(false);
        var invoked = false;
        c.OnBirth += () => invoked = true;
        c.IsAlive = true;
        Assert.That(invoked, Is.True);
    }
}
