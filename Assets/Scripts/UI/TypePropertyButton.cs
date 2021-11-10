using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypePropertyButton : ItemPropertyButton
{
    [SerializeField] Sprite tower;
    [SerializeField] Sprite trap;
    [SerializeField] Sprite spell;
    [SerializeField] Sprite barrier;

    public override void UpdateView( UnitTemplate unitTemplate )
    {
        if ( unitTemplate.unitType == Unit.UnitType.Spell )
            _button.image.sprite = spell;
        else if ( unitTemplate.unitType == Unit.UnitType.Tower )
        {
            switch ( unitTemplate.towerType )
            {
                case TowerUnit.TowerType.Trap:
                    _button.image.sprite = trap;
                    break;
                case TowerUnit.TowerType.Tower:
                    _button.image.sprite = tower;
                    break;
                case TowerUnit.TowerType.Barrier:
                    _button.image.sprite = barrier;
                    break;
            }
        }

    }
}
