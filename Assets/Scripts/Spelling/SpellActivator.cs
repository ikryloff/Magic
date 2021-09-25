using UnityEngine;

public class SpellActivator : MonoBehaviour
{
    private TowerBuilder _towerBuilder;
    private AttackSpeller _attackSpeller;

    private bool _isActive;

    private void Awake()
    {
        _towerBuilder = GetComponent<TowerBuilder> ();
        _attackSpeller = GetComponent<AttackSpeller> ();
        GameEvents.current.OnSwitchTouch += SwitchActivity;
    }

    private void OnDisable()
    {
        GameEvents.current.OnSwitchTouch -= SwitchActivity;
    }

    public void ActivateSpell( UnitTemplate spellTemplate, Cell [] cells )
    {
        if ( !_isActive )
            return;
        GameEvents.current.GameStateChangedAction (GameManager.GameState.BoardSleep);

        if ( spellTemplate.unitType == Unit.UnitType.Spell )
        {
            // todo
            _attackSpeller.MakeSpelling (spellTemplate, cells);
        }
        else
        {
            _towerBuilder.BuildTower (spellTemplate, cells);
        }

    }

    private void SwitchActivity( bool isOn )
    {
        _isActive = isOn;
    }

}
