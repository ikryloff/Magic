using UnityEngine;


[RequireComponent (typeof (UCTargetFinder))]
[RequireComponent (typeof (UCWeapon))]
[RequireComponent (typeof (Rigidbody2D))]


public class Human : BoardUnit
{
    private float _xp;
    private bool _isWeak;

    public void Activate( int linePosition, UnitTemplate template )
    {
        _xp = template.xp;
        SetLinePosition (linePosition);
        SetColumnPosition (int.MaxValue);
        Init (template);
    }

    private void OnTriggerEnter2D( Collider2D collider )
    {
        if ( collider.gameObject.tag.Equals (Constants.CELL_TAG) )
        {
            _cell = collider.GetComponent<Cell> ();
            if ( _cell.IsGameEnder() )
            {
                GameEvents.current.GameStateChangedAction (GameManager.GameState.GameOver);
                return;
            }
            this.SetColumnPosition (_cell.GetColumnPosition ());
            this.SetLinePosition (_cell.GetLinePosition ());
            GameEvents.current.HumanPositionWasChanged (this);
        }
    }

   
    
    public bool IsWeak()
    {
        return _isWeak;
    }

    public void SetWeak(UnitTemplate senderTemplate)
    {
        _isWeak = true;
    }




}

