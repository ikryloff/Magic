using UnityEngine;

public class UnitStateDie : IUnitState
{
    private BoardUnit _unit;
    private UCUnitAnimation _unitAnimation;
    private UnitHealth _unitHealth;
    private UnitTemplate _unitTemplate;

    public UnitStateDie( BoardUnit unit, UCUnitAnimation unitAnimation, UnitHealth unitHealth, UnitTemplate unitTemplate )
    {
        _unit = unit;
        _unitAnimation = unitAnimation;
        _unitHealth = unitHealth;
        _unitTemplate = unitTemplate;
    }

    public void Enter()
    {
        _unitAnimation?.StopAllAnimations ();
        if( _unitTemplate.unitType == Unit.UnitType.Tower)
            _unit.GetCurrentCell().SetFreefromTower ();
        RemoveUnit ();
        _unitHealth.MakeDeath ();
    }

    public void Exit()
    {
        return;
    }

    public void Tick()
    {
        return;
    }

    private void RemoveUnit()
    {
        if ( _unitTemplate.unitType == Unit.UnitType.Human )
        {
            UnitsOnBoard.RemoveHumanFromLineHumansList (_unit.GetComponent<Human> ());
        }
        else
        {
            UnitsOnBoard.RemoveTowerFromLineTowersList (_unit.GetComponent<TowerUnit> ());
        }
    }
}