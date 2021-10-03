using System.Collections.Generic;
using UnityEngine;


public class TowerUnit : BoardUnit
{
    public enum TowerType
    {
        None,
        Trap,
        Tower,
        Barrier,
    }

    protected TowerType _towerType;
    private int _towerCost;

    public virtual void Activate( UnitTemplate template, Cell cell )
    {
        _cell = cell;
        _towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition ());
        SetColumnPosition (cell.GetColumnPosition ());
        Init (template);
        _towerCost = template.cost / template.targetIndexes.Length;

    }

    public override void RemoveUnit()
    {
        UnitsOnBoard.RemoveTowerFromLineTowersList(this);
    }

    public TowerType GetTowerType()
    {
        return _towerType;
    }

}
