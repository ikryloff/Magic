using System.Collections.Generic;
using UnityEngine;

public class Human : BoardUnit
{
    private float _xp;
    private float _speed;
    private float _currentSpeed;

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
       
    public override void IdleBehavior()
    {
        transform.Translate (Vector2.left * _currentSpeed * Time.deltaTime);
    }

    public override void RemoveUnit()
    {
        UnitsOnBoard.RemoveHumanFromLineHumansList (this);
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

