using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private int _lineNumber;
    public Cell [] cells;
   

    public void InitLine()
    {
        DefineCells ();
    }

    public void DefineCells()
    {
        cells = GetComponentsInChildren<Cell> ();
        for ( int i = 0; i < cells.Length; i++ )
        {
            cells [i].CellInit();
            cells [i].SetLinePosition(_lineNumber);
        }
    }

    public Cell GetCell( int num )
    {
        for ( int i = 0; i < cells.Length; i++ )
        {
            if ( num == cells [i].GetColumnPosition() )
                return cells [i];
        }
        return null;
    }

    public int GetLineNumber()
    {
        return _lineNumber;
    }

}
