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

    public int towerID;
    protected TowerType _towerType;
    public int towerLevel;
    public int towerCost;

    public SpellUnit.SpellType spellType;

    public virtual void Activate( UnitTemplate template, Cell cell )
    {
        _cell = cell;
        _towerType = template.towerType;
        SetLinePosition (cell.GetLinePosition ());
        SetColumnPosition (cell.GetColumnPosition ());
        Init (template);
        towerCost = template.cost / template.targetIndexes.Length;

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
