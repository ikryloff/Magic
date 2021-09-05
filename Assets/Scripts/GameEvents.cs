using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    private static GameEvents _current;

    public static GameEvents current
    {
        get { return _current; }
    }

    private void Awake()
    {
        if ( _current != null && _current != this )
        {
            Destroy (this.gameObject);
            return;
        }

        _current = this;
        DontDestroyOnLoad (this.gameObject);
        Debug.Log ("GameEventManager");
    }



    public event Action OnCastOver;
    public event Action OnStopCastingEvent;
    public event Action OnFieldIsBuiltEvent;
    public event Action OnCastReset;
    public event Action OnEnemyAppear;
    public event Action<TowerUnit, Cell> OnTowerWasBuilt;
    public event Action<Human, Cell> OnHumanPositionWasChanged;
    public event Action<string> OnNewGameMessage;
    public event Action<BoardUnit, float, Unit.UnitClassProperty> OnNewHit;
    public event Action<Human> OnHumanDeath;

    public void HumanDeath( Human unit )
    {
        OnHumanDeath?.Invoke (unit);
    }

    public void NewHit( BoardUnit unit, float damage, Unit.UnitClassProperty classProperty )
    {
        OnNewHit?.Invoke (unit, damage, classProperty );
    }

    public void NewGameMessage( string message )
    {
        OnNewGameMessage?.Invoke (message);
    }


    public void CastOver()
    {
        OnCastOver?.Invoke ();
    }

    public void StopCastingAction()
    {
        OnStopCastingEvent?.Invoke ();
    }

    public void FieldIsBuiltAction()
    {
        OnFieldIsBuiltEvent?.Invoke ();
    }



    public void CastReset()
    {
        if ( OnCastReset != null )
        {
            OnCastReset ();
        }
    }

    public void EnemyAppear()
    {
        if ( OnEnemyAppear != null )
        {
            OnEnemyAppear ();
        }
    }

    public void TowerWasBuilt( TowerUnit tower, Cell cell )
    {
        OnTowerWasBuilt?.Invoke (tower, cell);
    }

    public void HumanPositionWasChanged( Human human, Cell cell )
    {
        OnHumanPositionWasChanged?.Invoke (human, cell);
    }
}
