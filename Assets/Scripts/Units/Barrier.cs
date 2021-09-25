public class Barrier : TowerUnit
{
    public override void SeekEnemies( BoardUnit unit, Cell cell ) { return; }

    public override BoardUnit GetRandomTarget() { return null; }
}
