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


    private void ChangePositionForHumanAttack( BoardUnit unit, BoardUnit enemy, string direction )
    {
        int newColPos = unit.GetColumnPosition ();
        int enemyColPos = enemy.GetColumnPosition ();

        if ( newColPos == enemyColPos )
        {
           // if ( direction.Equals (Constants.UNIT_RIGHT_DIR) )
            {
                newColPos = enemyColPos - 1;

            }
          //  if ( direction.Equals (Constants.UNIT_LEFT_DIR) )
            {
                newColPos = enemyColPos + 1;
            }
            unit.SetColumnPosition (newColPos);
        }
        CellPos newPos = new CellPos (newColPos, unit.GetLinePosition ());
        Cell newCell = Board.GetCellByPosition (newPos);
        unit.transform.position = new Vector3 (newCell.transform.position.x + Utilities.GetPositionDisplace (), unit.transform.position.y, unit.transform.position.z);
    }
}

public enum Direction
{
    Left,
    Right
}