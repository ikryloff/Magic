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

        foreach ( Cell item in cells )
        {            
            if ( item.GetLinePosition() < tempTopPos )
                tempTopPos = item.GetLinePosition();
            if ( item.GetLinePosition () > tempBottomPos )
                tempBottomPos = item.GetLinePosition ();
            if ( item.GetColumnPosition() < tempLeftPos )
                tempLeftPos = item.GetColumnPosition ();
            if ( item.GetColumnPosition () > tempRightPos )
                tempRightPos = item.GetColumnPosition ();
        }
        Top = tempTopPos;
        Bottom = tempBottomPos;
        Left = tempLeftPos;
        Right = tempRightPos;


        int spellLenght = (Bottom - Top + 1) * (Right - Left + 1) + (Bottom - Top + 1); 

        int [] spell = new int [spellLenght];
        _activeCells = new Cell [cells.Count];

        int count = 0;
        int countCell = 0;
        for ( int i = tempTopPos; i <= tempBottomPos; i++ )
        {
            for ( int j = tempLeftPos; j <= tempRightPos; j++ )
            {
                Cell cell = _board.GetCell (i, j);
                if ( cell.IsLoaded )
                {
                    spell [count] = 1;
                    _activeCells [countCell] = cell;
                    countCell += 1;
                }
                else
                    spell [count] = 0;

                count += 1;

                if ( j == tempRightPos )
                {
                    spell [count] = 2;
                    count++;
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

