using UnityEngine;


[RequireComponent (typeof (UCTargetFinder))]
[RequireComponent (typeof (TowerUnit))]

public class UCPointerWeapon : UCTrapWeapon
{
    public override void Fire( BoardUnit unit, BoardUnit enemy )
    {
        if ( unit != _unit ) return;
        if ( enemy == null ) return;
        Human human = enemy.GetComponent<Human> ();
        int currentLine = human.GetLinePosition ();
        int newLine = human.GetLinePosition () + _unitTemplate.stepDirection;
        human.SetLinePosition (newLine);
        UnitsOnBoard.RemoveHumanFromLineHumansList (human);
        UnitsOnBoard.AddHumanToLineHumansList (human, newLine);
        Cell newCell = Board.GetCellByPosition (new CellPos (enemy.GetColumnPosition (), newLine));
        Vector3 newPos = new Vector3 (newCell.transform.position.x, Board.GetLineY (newLine), enemy.transform.position.z);
        enemy.transform.position = newPos;
        GameEvents.current.HumanPositionWasChanged (human);

        //_unit.SetDieState();
    }
}
