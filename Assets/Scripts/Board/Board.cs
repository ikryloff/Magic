using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellTile;
    [SerializeField]
    private int _width, _height;
    private float _tileWidth;
    private Camera _mainCamera;
    private SpawnPoints _spawnPoints;
    private FirePoints _firePoints;
    private List<Cell> _defCells;
    private TowerBuilder _builder;
    public static Dictionary<Cell, CellPos> _cellsPositionMap;
    private static Dictionary<CellPos, Cell> _positionCellMap;

    private void Awake()
    {
        Init ();
    }


    private void Init()
    {
        _defCells = new List<Cell> ();
        _mainCamera = Camera.main;
        _tileWidth = _cellTile.GetComponent<SpriteRenderer> ().bounds.size.x;
        _cellsPositionMap = new Dictionary<Cell, CellPos> ();
        _positionCellMap = new Dictionary<CellPos, Cell> ();
        _builder = FindObjectOfType<TowerBuilder> ();
        _spawnPoints = GetComponentInChildren<SpawnPoints> ();
        _firePoints = GetComponentInChildren<FirePoints> ();
        Debug.Log ("InitBoardObject");
        BuildBoard ();
    }


    private void BuildBoard()
    {
        int countLineOffset = _height / 2;

        for ( int y = 0; y < _height; y++ )
        {
            for ( int x = 0; x < _width; x++ )
            {
                float lineOffset = x * _tileWidth;

                if ( y % 2 == 0 )
                    lineOffset = lineOffset - _tileWidth * 0.5f;
                int col = countLineOffset + x;
                int line = _height - y - 1;
                GameObject cellGO = Instantiate (_cellTile, new Vector3 (lineOffset, y), Quaternion.identity);
                cellGO.name = $"Cell_{col}_{line}";
                cellGO.transform.parent = this.transform;
                Cell cell = cellGO.GetComponent<Cell> ();
                cell.Init (new CellPos (col, line));
                _cellsPositionMap.Add (cell, new CellPos (col, line));
                _positionCellMap.Add (new CellPos (col, line), cell);

                // set bounds of board
                if ( y == 0 || y == _height - 1 || x == 0 || x == _width - 1 || x == 1 )
                {
                    cell.SetUnusable ();
                }
                // setup spawnpoints if it is last cell in line, without first and last lines
                if ( x == _width - 1 && line != 0 && line < 8)
                {
                    _spawnPoints.SetPointPosition (line - 1, cell.transform.position + new Vector3 (_tileWidth, _tileWidth * 0.5f, 0));
                }
                // setup firepoints
                if ( y == _height - 1 && x < _firePoints.GetPointsCount () )
                {
                    _firePoints.SetPointPosition (x, cell.transform.position + new Vector3 (0, _tileWidth * 2f, 0));
                }
                // setup deffense towers cells
                if ( y != 0 && y != _height - 1 && x == 1 )
                {
                    _defCells.Add (cell);
                }
            }
            // offset for cells numeration
            if ( y % 2 == 1 )
                countLineOffset -= 1;

        }
        _mainCamera.transform.position = new Vector3 (_width * _tileWidth * 0.45f, _height / 2, -10);
        Debug.Log ("Cells was built");
        _builder.BuildDefTower (_defCells);
        GameEvents.current.CastResetEvent ();
    }

    public static CellPos GetPositionByCell( Cell cell )
    {
        return _cellsPositionMap [cell];
    }

    public static Cell GetCellByPosition( CellPos pos )
    {
        if ( _positionCellMap.ContainsKey (pos) )
            return _positionCellMap [pos];
        else return null;
    }
}

public struct CellPos
{
    private int _col;
    private int _line;

    public int Col => _col;
    public int Line => _line;

    public CellPos( int col, int line )
    {
        _col = col;
        _line = line;
    }
}
