using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IUnitState _currentState;


    public IUnitState GetCurrentState()
    {
        return _currentState;
    }

   
    public void ChangeState( IUnitState newState )
    {
        if ( newState == _currentState )
            return;

        if ( _currentState != null )
            _currentState.Exit ();

        _currentState = newState;

        if ( _currentState != null )
            _currentState.Enter ();

    }

    protected void Update()
    {
        if ( _currentState != null)
        {
            _currentState.Tick ();
        }
    }
}
