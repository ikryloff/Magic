using UnityEngine;

public class SpellActivator : MonoBehaviour
{
    private TowerBuilder _towerBuilder;
    private AttackSpeller _attackSpeller;

    private void Awake()
    {
        _towerBuilder = GetComponent<TowerBuilder> ();
        _attackSpeller = GetComponent<AttackSpeller> ();
    }

      public void ActivateSpell( UnitTemplate spellTemplate, Cell [] cells )
    {
        if ( Wizard.IsStopCasting )
            return;
        Wizard.IsStopCasting = true;

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

}
