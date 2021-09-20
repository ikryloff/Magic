using System;
using System.Collections.Generic;
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
    public event Action <GameManager.GameState> OnGameStateChangedAction;
    public event Action <bool> OnSwitchTouch;
    public event Action OnStopCastingAction;
    public event Action OnBoardIsBuiltAction;
    public event Action OnCastResetAction;
    public event Action OnEnemyAppear;
    public event Action<TowerUnit, Cell> OnTowerWasBuiltAction;
    public event Action<Human, Cell> OnHumanPositionWasChanged;
    public event Action<string> OnNewGameMessage;
    public event Action<BoardUnit, UnitTemplate> OnNewHit;
    public event Action<BoardUnit, float> OnHealthChangedAction;
    public event Action<Human> OnHumanDeathAction;
    public event Action<TowerUnit, Cell> OnTowerUnitDeathAction;


    public void HealthChangedEvent( BoardUnit sender, float ratio )
    {
        OnHealthChangedAction?.Invoke (sender, ratio);
    }


    public void GameStateChangedEvent( GameManager.GameState state )
    {
        OnGameStateChangedAction?.Invoke (state);
        Debug.Log ("GameState " + state);
    }

    public void SwitchTouch( bool isOn )
    {
        OnSwitchTouch?.Invoke (isOn);
        Debug.Log ("Touch " + isOn);
    }


    public void HumanDeathEvent( Human unit )
    {
        OnHumanDeathAction?.Invoke (unit);
    }

    public void TowerUnitDeathEvent( TowerUnit unit, Cell cell )
    {
        OnTowerUnitDeathAction?.Invoke (unit, cell);
    }

    public void NewHit( BoardUnit unit, UnitTemplate sender)
    {
        OnNewHit?.Invoke (unit, sender);
    }

    public void NewGameMessage( string message )
    {
        OnNewGameMessage?.Invoke (message);
    }


    public void CastOver()
    {
        OnCastOver?.Invoke ();
    }

    public void StopCastingEvent()
    {
        OnStopCastingAction?.Invoke ();
    }

    public void BoardIsBuiltEvent()
    {
        OnBoardIsBuiltAction?.Invoke ();
    }



    public void CastResetEvent()
    {
        OnCastResetAction?.Invoke ();
    }

    public void EnemyAppear()
    {
        if ( OnEnemyAppear != null )
        {
            OnEnemyAppear ();
        }
    }

    public void TowerWasBuiltEvent( TowerUnit tower, Cell cell )
    {
        OnTowerWasBuiltAction?.Invoke (tower, cell);
    }

    public void HumanPositionWasChanged( Human human, Cell cell )
    {
        OnHumanPositionWasChanged?.Invoke (human, cell);
    }

  
}
