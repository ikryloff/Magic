using UnityEngine;


[RequireComponent (typeof (TargetFinder))]
[RequireComponent (typeof (TowerUnit))]

public class PointerWeapon : TrapWeapon
{
    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            Human human = enemy.GetComponent<Human> ();
            int currentLine = human.GetLinePosition ();
            int newLine = human.GetLinePosition () + _unitTemplate.stepDirection;
            human.SetLinePosition (newLine);
            UnitsOnBoard.RemoveHumanFromLineHumansList (human);
            UnitsOnBoard.AddHumanToLineHumansList (human, newLine);
            Cell newCell = Board.GetCellByPosition (new CellPos (enemy.GetColumnPosition (), newLine));
            Vector3 newPos = new Vector3 (newCell.transform.position.x, Board.GetLineY (newLine), enemy.transform.position.z);
            enemy.transform.position = newPos;
            GameEvents.current.HumanPositionWasChanged (human, newCell);

            _unit.MakeDeath ();
        }
    }
}
