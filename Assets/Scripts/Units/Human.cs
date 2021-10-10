using UnityEngine;


[RequireComponent (typeof (UCTargetFinder))]
[RequireComponent (typeof (UCUnitAnimation))]
[RequireComponent (typeof (UCWeapon))]
[RequireComponent (typeof (Rigidbody2D))]


public class Human : BoardUnit
{
    private float _xp;
    private float _speed;
    private float _currentSpeed;
    private bool _isWeak;

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
    
    public bool IsWeak()
    {
        return _isWeak;
    }

    public void SetWeak(UnitTemplate senderTemplate)
    {
        _isWeak = true;
        _currentSpeed = _currentSpeed * 0.5f;
    }




}

