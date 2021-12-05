using UnityEngine;

public class UCBoardUnitMoving : MonoBehaviour
{
    private BoardUnit _unit;
    private Direction _direction;
    private float _unitSpeed;
    private float _currentSpeed;


    public virtual void SetStartDirection() { }

    public void Init( BoardUnit unit )
    {
        _unit = unit;
        _unitSpeed = unit.GetUnitTemplate().speed;
        _currentSpeed = _unitSpeed;

        SetDefaultDirection (unit);

        GameEvents.current.OnIdleStateEvent += Idle;
        GameEvents.current.OnStopToFightEvent += StopToFight;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnIdleStateEvent -= Idle;
        GameEvents.current.OnStopToFightEvent -= StopToFight;
    }

    private void Update()
    {
        if ( _currentSpeed <= 0 && _direction == Direction.Right )
            return;
        transform.Translate (Vector2.left * _currentSpeed * Time.deltaTime);
    }

    public void StopToFight(BoardUnit unit, BoardUnit enemy)
    {
        if ( unit != _unit ) return;
        TurnToEnemy (unit, enemy);
        _currentSpeed = 0;
    }

    public void Idle(BoardUnit unit)
    {
        if ( unit != _unit ) return;
        _currentSpeed = _unitSpeed;
        SetDefaultDirection (unit);
    }

    public void ChangeUnitSpeed( float koef )
    {
        _unitSpeed *= koef;
        _currentSpeed = _unitSpeed;
    }

    private void TurnToEnemy(BoardUnit unit, BoardUnit enemy)
    {
        if ( unit != _unit ) return;

        if ( unit.transform.position.x < enemy.transform.position.x )
            TurnRight ();
        else
            TurnLeft ();

    }

    public void TurnLeft()
    {
        transform.rotation = new Quaternion (0, 0, 0, 0);
        _direction = Direction.Left;
    }

    public void TurnRight()
    {
        transform.rotation = new Quaternion (0, 180, 0, 0);
        _direction = Direction.Right;
    }

    public Direction GetDirection()
    {
        return _direction;
    }

    public void SetDefaultDirection(BoardUnit unit)
    {
        if ( unit.GetUnitTemplate ().unitType == Unit.UnitType.Human )
            TurnLeft ();
        else
            TurnRight ();
    }

}

public enum Direction
{
    Left,
    Right
}