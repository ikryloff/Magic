using UnityEngine;

[RequireComponent (typeof (TowerUnit))]
[RequireComponent (typeof (TargetFinder))]
[RequireComponent (typeof (UnitAnimation))]

public class LoopWeapon : TrapWeapon
{
    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            Human human = enemy.GetComponent<Human> ();
            int newLine = human.GetLinePosition ();
            int newColumn = human.GetColumnPosition () + 6;
            Cell newCell = Board.GetCellByPosition (new CellPos (newColumn, newLine));
            Vector3 newPos = new Vector3 (newCell.transform.position.x, Board.GetLineY (newLine), enemy.transform.position.z);
            enemy.transform.position = newPos;
            GameEvents.current.HumanPositionWasChanged (human, newCell);

            _unit.MakeDeath ();
        }
    }
}
