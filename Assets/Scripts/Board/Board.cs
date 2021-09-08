using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellTile;
    [SerializeField]
    private int _width, _height;
    private float _tileWidth;
    private Camera _mainCamera;
    

    private Dictionary<Cell, CellPos> _cellsMap;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _tileWidth = _cellTile.GetComponent<SpriteRenderer> ().bounds.size.x;
        _cellsMap = new Dictionary<Cell, CellPos> ();
        BuildBoard ();
    }

    private void Start()
    {
        Debug.Log (GetCell (new CellPos(3,1)).name);
    }

    private void BuildBoard()
    {
        for ( int y = 0; y < _height; y++ )
        {
            for ( int x = 0; x < _width; x++ )
            {
                GameObject cellGO;
                float lineOffset = x * _tileWidth;

                if ( y % 2 == 0 )
                    lineOffset = lineOffset - _tileWidth * 0.5f;

                cellGO = Instantiate (_cellTile, new Vector3 (lineOffset, y), Quaternion.identity);
                cellGO.name = $"Cell_{x}_{_height - y}";
                cellGO.transform.parent = this.transform;
                Cell cell = cellGO.GetComponent<Cell> ();
                _cellsMap.Add (cell, new CellPos (x, _height - y));
            }
        }
        _mainCamera.transform.position = new Vector3 (_width * _tileWidth * 0.45f, _height / 2, -10);
    }

    public CellPos GetCellPosition( Cell cell )
    {
        return _cellsMap [cell];
    }

    public Cell GetCell( CellPos pos )
    {
        return _cellsMap.Where(c => c.Value.Equals(pos)).FirstOrDefault().Key;
    }
}

public struct CellPos
{
    int _col;
    int _line;

    public CellPos(int col, int line)
    {
        _col = col;
        _line = line;
    }
}
