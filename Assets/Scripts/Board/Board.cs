using UnityEngine;

public class Board : MonoBehaviour
{
    private Line [] _lines;
   

    private void Awake()
    {
        _lines = GetComponentsInChildren<Line> ();
        DefineLines (_lines);
        print ("Board");        
    }

    private void DefineLines( Line [] lines )
    {
        for ( int i = 0; i < lines.Length; i++ )
        {
            lines [i].DefineCells ();
        }
    }

    private  Line GetLine( int num )
    {
        for ( int i = 0; i < _lines.Length; i++ )
        {
            if ( num == _lines [i].GetLineNumber() )
                return _lines [i];
        }
        return null;
    }

    public Cell GetCell( int lineNumber, int cellNumber )
    {
        return GetLine (lineNumber).GetCell (cellNumber);
    }
}
