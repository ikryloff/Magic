using UnityEngine;

public class WayPointer : Trap
{
    private int _stepDirection;

    public override void Activate( UnitTemplate template, Cell cell )
    {
        base.Activate (template, cell);
        _stepDirection = template.stepDirection;
    }


    public override void Fire( BoardUnit enemy )
    {
        if ( enemy )
        {
            Human human = enemy.GetComponent<Human> ();
            int currentLine = human.GetLinePosition ();
            int newLine = human.GetLinePosition () + _stepDirection;
            human.SetLinePosition (newLine);
            Utilities.DisplaceZPosition (human); // to prevent flicking
            UnitsOnBoard.RemoveHumanFromLineHumansList (human);
            UnitsOnBoard.AddHumanToLineHumansList (human, newLine);
            Cell newCell = Board.GetCellByPosition (new CellPos (enemy.GetColumnPosition (), newLine));
            Vector3 newPos = new Vector3 (newCell.transform.position.x, Board.GetLineY(newLine), enemy.transform.position.z);
            enemy.transform.position = newPos;
            GameEvents.current.HumanPositionWasChanged (human, newCell);
            MakeDeath ();
        }
    }
}
