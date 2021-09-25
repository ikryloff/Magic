using System.Collections.Generic;
using UnityEngine;

public class SpellDecoder : MonoBehaviour
{
    private SpellsMaps spellsMaps;
    private Board _board;
    private SpellActivator spellActivator;
    Cell [] _activeCells;
    private Board field;
    public int Top;
    public int Bottom;
    public int Left;
    public int Right;

    private void Awake()
    {
        _board = FindObjectOfType<Board>();
    }

    private void Start()
    {
        spellsMaps = GetComponent<SpellsMaps>();
        spellActivator = GetComponent<SpellActivator> ();
    }

    public void MakeSpell( List<Cell> cells )
    {
        if ( cells.Count <= 1 )
        {
            GameEvents.current.StopCastingAction ();
            return;
        }
        string spellCode = GetSpellcode (cells);
        Debug.Log ("SpellCode: " + spellCode);

        UnitTemplate entityTemplate = spellsMaps.GetSpellByString (spellCode);
        if( entityTemplate == null )
        {
            GameEvents.current.StopCastingAction ();
            return;
        }

        spellActivator.ActivateSpell (entityTemplate, GetTargetCells(entityTemplate, _activeCells));

    }   

    public string GetSpellcode( List<Cell> cells )
    {
        int tempLeftPos = int.MaxValue;
        int tempRightPos = 0;
        int tempTopPos = int.MaxValue;
        int tempBottomPos = 0;

        _activeCells = null;
        //calc bounds of used cells
        foreach ( Cell cell in cells )
        {            
            if ( cell.GetLinePosition() < tempTopPos )
                tempTopPos = cell.GetLinePosition();
            if ( cell.GetLinePosition () > tempBottomPos )
                tempBottomPos = cell.GetLinePosition ();
            if ( cell.GetColumnPosition() < tempLeftPos )
                tempLeftPos = cell.GetColumnPosition ();
            if ( cell.GetColumnPosition () > tempRightPos )
                tempRightPos = cell.GetColumnPosition ();
        }
        Top = tempTopPos;
        Bottom = tempBottomPos;
        Left = tempLeftPos;
        Right = tempRightPos;


        List<int> spell = new List<int>();
        _activeCells = new Cell [cells.Count];

        int countCell = 0;
        for ( int i = tempTopPos; i <= tempBottomPos; i++ )
        {
            for ( int j = tempLeftPos; j <= tempRightPos; j++ )
            {
                //Debug.Log ("col " + j + " line " + i);
                Cell cell = Board.GetCellByPosition ( new CellPos(j, i));
                //this spell code must be wrong
                if ( cell == null )
                {
                    spell.Add (5);
                    continue;
                }
                
                if ( cell.IsLoaded )
                {
                    spell.Add(1);
                    _activeCells [countCell] = cell;
                    countCell += 1;
                }
                else
                    spell.Add (0);


                if ( j == tempRightPos )
                {
                    spell.Add (2);
                }
            }
        }

        return string.Join ("", spell);

    }
    //Get cells where will be a tower. They are numerated in SO
    public Cell[] GetTargetCells( UnitTemplate template, Cell[] activeCells)
    {
        Cell [] targetCells = new Cell [template.targetIndexes.Length];
        int j = 0;
        for ( int i = 0; i < activeCells.Length; i++ )
        {
            
            if ( i == template.targetIndexes[j] )
            {
                targetCells [j] = activeCells [i];
                j++;
                if( template.targetIndexes.Length == j)
                    return targetCells;
            }
        }
        return null;
        
    } 
}

