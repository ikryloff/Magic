using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IUnitState _currentState;
    bool _isSettingNow = false;


    public IUnitState GetCurrentState()
    {
        return _currentState;
    }

    public void ChangeState(IUnitState newState)
    {
        if ( newState == _currentState || _isSettingNow )
            return;

        _isSettingNow = true;

        if ( _currentState != null )
            _currentState.Exit ();
        _currentState = newState;

        if ( _currentState != null )
            _currentState.Enter ();

        _isSettingNow = false;
    }

    protected void Update()
    {
        if ( _currentState != null && !_isSettingNow )
            _currentState.Update ();
    }
}
