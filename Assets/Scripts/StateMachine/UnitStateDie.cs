public class UnitStateDie : IUnitState
{
    private BoardUnit _unit;
    private UnitAnimation _unitAnimation;

    public UnitStateDie( BoardUnit unit, UnitAnimation unitAnimation )
    {
        _unit = unit;
        _unitAnimation = unitAnimation;
    }

    public void Enter()
    {
        _unitAnimation?.StopAllAnimations ();
        if(_unit.GetUnitType() == Unit.UnitType.Tower)
            _unit.GetCurrentCell().SetFreefromTower ();
    }

    public void Exit()
    {
        return;
    }

    public void Tick()
    {
        return;
    }
}