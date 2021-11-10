using UnityEngine;

public class AttackTypePropertyButton : ItemPropertyButton
{
    [SerializeField] private Sprite attackAllInLine;
    [SerializeField] private Sprite attackNearestInLine;
    [SerializeField] private Sprite attackAll;
    [SerializeField] private Sprite attackRandomInLine;
    [SerializeField] private Sprite affectAllInLine;
    [SerializeField] private Sprite affectNearestInLine;
    [SerializeField] private Sprite affectAll;
    [SerializeField] private Sprite affectRandomInLine;
    [SerializeField] private Sprite affectTower;



    public override void UpdateView( UnitTemplate unitTemplate )
    {

        if ( unitTemplate.unitType == Unit.UnitType.Tower )
        {
            _button.image.sprite = attackRandomInLine;
        }

        if ( unitTemplate.unitType == Unit.UnitType.Spell )
        {

            if ( unitTemplate.spellType == SpellUnit.SpellType.AttackSpell )
            {
                switch ( unitTemplate.attackPower )
                {
                    case Unit.UnitAttackPower.AllInLine:
                        _button.image.sprite = attackAllInLine;
                        break;
                    case Unit.UnitAttackPower.NearestInLine:
                        _button.image.sprite = attackNearestInLine;
                        break;
                    case Unit.UnitAttackPower.RandomInLine:
                        _button.image.sprite = attackRandomInLine;
                        break;
                    case Unit.UnitAttackPower.All:
                        _button.image.sprite = attackAll;
                        break;
                }
            }

            if ( unitTemplate.spellType == SpellUnit.SpellType.PressureSpell )
            {
                switch ( unitTemplate.attackPower )
                {
                    case Unit.UnitAttackPower.AllInLine:
                        _button.image.sprite = affectAllInLine;
                        break;
                    case Unit.UnitAttackPower.NearestInLine:
                        _button.image.sprite = affectNearestInLine;
                        break;
                    case Unit.UnitAttackPower.RandomInLine:
                        _button.image.sprite = affectRandomInLine;
                        break;
                    case Unit.UnitAttackPower.All:
                        _button.image.sprite = affectAll;
                        break;
                }
            }

            if ( unitTemplate.spellType == SpellUnit.SpellType.HealingSpell || unitTemplate.spellType == SpellUnit.SpellType.ReturnManaSpell )
                _button.image.sprite = affectTower;
        }


    }
}
