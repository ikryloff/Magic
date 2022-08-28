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
    public event Action<GameManager.GameState> OnGameStateChangedEvent;
    public event Action<bool> OnSwitchTouch;
    public event Action OnStopCastingEvent;
    public event Action OnBoardIsBuiltEvent;
    public event Action OnTimeToColorCellsEvent;
    public event Action OnCastResetEvent;
    public event Action OnEnemyAppear;
    public event Action<TowerUnit> OnTowerWasBuiltEvent;
    public event Action<Human> OnHumanPositionWasChanged;
    public event Action<string> OnNewGameMessage;
    public event Action<BoardUnit> OnIdleStateEvent;
    public event Action<BoardUnit> OnDieEvent;
    public event Action<BoardUnit, UnitTemplate> OnNewHit;
    public event Action<BoardUnit, BoardUnit> OnStopToFightEvent;
    public event Action<BoardUnit, BoardUnit> OnAttackAnimationFinishedEvent;
    public event Action<BoardUnit, BoardUnit> OnAttackStartedEvent;
    public event Action<Unit.UnitClassProperty> OnTabChangeEvent;
    public event Action<UnitTemplate> OnItemButtonClickedEvent;
    public event Action<UnitTemplate> OnSpellItemViewOpenedEvent;
    public event Action<float> OnPrepareTimeValueChangedEvent;
    public event Action<float> OnManaValueChangedEvent;
    public event Action<float> OnManaWasteEvent;

    //UI Events
    public event Action<int> OnWizardLevelChangeEvent;

    public void WizardLevelChangeAction(int skill)
    {
        OnWizardLevelChangeEvent?.Invoke (skill);
    }

   
    public void DieAction( BoardUnit unit )
    {
        OnDieEvent?.Invoke (unit);
    }


    public void IdleStateAction( BoardUnit unit )
    {
        OnIdleStateEvent?.Invoke (unit);
    }

    public void SpellItemViewOpenedAction( UnitTemplate template )
    {
        OnSpellItemViewOpenedEvent?.Invoke (template);
    }

    public void ItemButtonClickedAction( UnitTemplate template )
    {
        OnItemButtonClickedEvent?.Invoke (template);
    }

    public void ManaWasteAction( float value )
    {
        Debug.Log ("WasteEvent");
        OnManaWasteEvent?.Invoke (value);
    }

    public void ManaValueChangedAction( float value )
    {
        OnManaValueChangedEvent?.Invoke (value);
    }

    public void PrepareTimeValueChangedAction( float value )
    {
        OnPrepareTimeValueChangedEvent?.Invoke (value);
    }

    public void TabChangeAction( Unit.UnitClassProperty classIndex )
    {
        OnTabChangeEvent?.Invoke (classIndex);
    }

    public void GameStateChangedAction( GameManager.GameState state )
    {
        Debug.Log ("GameState " + state);
        OnGameStateChangedEvent?.Invoke (state);
    }

    public void SwitchTouch( bool isOn )
    {
        OnSwitchTouch?.Invoke (isOn);
        Debug.Log ("Touch " + isOn);
    }

    public void NewHit( BoardUnit unit, UnitTemplate sender )
    {
        OnNewHit?.Invoke (unit, sender);
    }

    public void StopToFightAction( BoardUnit unit, BoardUnit enemy )
    {
        OnStopToFightEvent?.Invoke (unit, enemy);
    }

    public void AttackAnimationFinishedAction( BoardUnit unit, BoardUnit enemy )
    {
        OnAttackAnimationFinishedEvent?.Invoke (unit, enemy);
    }

    public void AttackStartedAction( BoardUnit unit, BoardUnit enemy )
    {
        OnAttackStartedEvent?.Invoke (unit, enemy);
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

    public void BoardIsBuiltAction()
    {
        OnBoardIsBuiltEvent?.Invoke ();
    }

    public void TimeToColorCellsEvent()
    {
        OnTimeToColorCellsEvent?.Invoke ();
    }



    public void CastResetAction()
    {
        OnCastResetEvent?.Invoke ();
    }

    public void EnemyAppear()
    {
        if ( OnEnemyAppear != null )
        {
            OnEnemyAppear ();
        }
    }

    public void TowerWasBuiltAction( TowerUnit tower )
    {
        OnTowerWasBuiltEvent?.Invoke (tower);
    }

    public void HumanPositionWasChanged( Human human )
    {
        OnHumanPositionWasChanged?.Invoke (human);
    }


}
