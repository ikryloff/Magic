using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPropertiesView : MonoBehaviour
{
    [SerializeField] private ItemPropertyButton [] notSpellProperties;
    [SerializeField] private ItemPropertyButton [] spellProperties;
    [SerializeField] private ItemPropertyButton [] towerProperties;
    [SerializeField] private ItemPropertyButton [] notTrapProperties;
    [SerializeField] private ItemPropertyButton [] notBarrierProperties;


    public void UpdateView(UnitTemplate unitTemplate)
    {
        if(unitTemplate.unitType == Unit.UnitType.Spell )
        {
            HideButtons (notSpellProperties);
            ShowButtons (spellProperties);
        }
        if( unitTemplate.unitType == Unit.UnitType.Tower )
        {
            ShowButtons (towerProperties);
            if(unitTemplate.towerType == TowerUnit.TowerType.Trap )
            {
                HideButtons (notTrapProperties);
            }
            if( unitTemplate.towerType == TowerUnit.TowerType.Barrier )
            {
                HideButtons (notBarrierProperties);
            }
        }

        GameEvents.current.SpellItemViewOpenedAction (unitTemplate);
    }

    private void ShowButtons(ItemPropertyButton[] items )
    {
        for ( int i = 0; i < items.Length; i++ )
        {
            items [i].gameObject.SetActive (true);
        }
    }

    private void HideButtons( ItemPropertyButton [] items )
    {
        for ( int i = 0; i < items.Length; i++ )
        {
            items [i].gameObject.SetActive (false);
        }
    }
}
