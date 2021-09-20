using System.Collections.Generic;
using UnityEngine;

public class Human : BoardUnit
{
    private float _xp;
    private float _speed;
    private float _currentSpeed;
    private Cell _cell;
    private bool isSlow;
    private List<TowerUnit> _towers;
    public GameObject deathPref;
    private GameObject suppressionWind;
    private GameObject defenceAffect;


    private void Awake()
    {
        suppressionWind = GameAssets.instance.GetAssetByString (Constants.SUPPRESSION_WIND);
        defenceAffect = GameAssets.instance.GetAssetByString (Constants.DEFFENCE_AFFECT);
        
    }

   
    public void Activate( int linePosition, UnitTemplate template )
    {
        _towers = new List<TowerUnit> ();
        _xp = template.xp;
        _speed = template.speed;
        _currentSpeed = _speed;
        SetLinePosition (linePosition);

        Init (template);
        

        //_boardUnitState = GetComponent<BoardUnitState> ();
       // _boardUnitState.Init ();
    }

    private void OnTriggerEnter2D( Collider2D collider )
    {
        if ( collider.gameObject.tag.Equals (Constants.CELL_TAG) )
        {
            _cell = collider.GetComponent<Cell> ();
            this.SetColumnPosition (_cell.GetColumnPosition ());
            this.SetLinePosition (_cell.GetLinePosition ());
            GameEvents.current.HumanPositionWasChanged (this, _cell);
        }
    }

    public List<TowerUnit> GetTowers()
    {
        return _towers;
    }

    public override void GetEnemies( BoardUnit human, Cell cell )
    {
        if ( human.GetType () == typeof (Human) && human != this )
            return;

        if ( cell.GetLinePosition () != _linePosition )
            return;

        List<TowerUnit> enemies = UnitsOnBoard.LineTowersList [_linePosition];

        if ( enemies.Count == 0 )
        {
            _boardUnitState.SetIdleState();
            return;
        }

        _towers.Clear ();
        for ( int i = 0; i < enemies.Count; i++ )
        {
            if ( Mathf.Abs (enemies [i].GetColumnPosition () - _columnPosition) <= _attackRange )
            {
                _towers.Add (enemies [i]);
                Debug.Log (name + " Found " + enemies [i].name + " Count " + _towers.Count + " at line " + _linePosition);
            }
        }

        if ( _towers.Count > 0 )
        {
            //_boardUnitState.Decide ();
        }
        else
        {
            //_boardUnitState.SetIdleState ();
        }

    }
   
    public override BoardUnit GetRandomTarget()
    {
        List<TowerUnit> towers = _towers;
        if ( towers.Count > 0 )
            return towers [Random.Range (0, towers.Count)];
        else
            return null;
    }

    public override void Idle()
    {
        transform.Translate (Vector2.left * _currentSpeed * Time.deltaTime);
    }

    public override void MakeDeath()
    {
        UnitsOnBoard.RemoveHumanFromLineHumansList (this);
        GameEvents.current.HumanDeathEvent (this);
        Instantiate (_death, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }

    public float GetCurrentSpeed()
    {
        return _currentSpeed;
    }

    public void SetCurrentSpeed( float speed)
    {
        _currentSpeed = speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }



}

