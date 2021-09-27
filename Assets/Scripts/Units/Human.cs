using System.Collections.Generic;
using UnityEngine;

public class Human : BoardUnit
{
    private float _xp;
    private float _speed;
    private float _currentSpeed;
    private bool isSlow;
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
        _xp = template.xp;
        _speed = template.speed;
        _currentSpeed = _speed;
        SetLinePosition (linePosition);
        SetColumnPosition (int.MaxValue);

        Init (template);
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

    public override BoardUnit GetRandomTarget( )
    {
        List<TowerUnit> towers = UnitsOnBoard.LineTowersList [_linePosition];
        if ( towers.Count == 0 )
            return null;

        List<TowerUnit> towersInRange = new List<TowerUnit> ();

        for ( int i = 0; i < towers.Count; i++ )
        {
            if ( Mathf.Abs (towers [i].GetColumnPosition () - _columnPosition) <= _attackRange )
                towersInRange.Add (towers [i]);
        }

        if ( towersInRange.Count == 0 )
            return null;

        return towersInRange [Random.Range (0, towersInRange.Count)];
    }

   
    public override void IdleBehavior()
    {
        transform.Translate (Vector2.left * _currentSpeed * Time.deltaTime);
    }

    public override void MakeDeath()
    {
        UnitsOnBoard.RemoveHumanFromLineHumansList (this);
        Instantiate (_death, transform.position, Quaternion.identity);
        SetDieState ();
        Destroy (gameObject);

    }

    public float GetCurrentSpeed()
    {
        return _currentSpeed;
    }

    public void SetCurrentSpeed( float speed )
    {
        _currentSpeed = speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }



}

