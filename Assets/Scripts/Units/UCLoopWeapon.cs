using UnityEngine;

[RequireComponent (typeof (TowerUnit))]
[RequireComponent (typeof (UCTargetFinder))]

public class UCLoopWeapon : UCTrapWeapon
{
    public override void Fire( BoardUnit unit, BoardUnit enemy )
    {
        if ( unit != _unit ) return;
        if ( enemy == null ) return;
        Human human = enemy.GetComponent<Human> ();
        int newLine = human.GetLinePosition ();
        int newColumn = human.GetColumnPosition () + 6;
        Cell newCell = Board.GetCellByPosition (new CellPos (newColumn, newLine));
        if ( newCell == null )
            newCell = Board.GetCellByPosition (new CellPos (newColumn - 1, newLine));
        Vector3 newPos = new Vector3 (newCell.transform.position.x, Board.GetLineY (newLine), enemy.transform.position.z);
        enemy.transform.position = newPos;
        GameEvents.current.HumanPositionWasChanged (human);
        GameEvents.current.DieAction (_unit);

    }
}
