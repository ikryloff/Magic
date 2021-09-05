using System;

public class SpellUnit : Unit
{
    public enum SpellType
    {
        None,  // for none        
        AttackSpell,    // for humans
        PressureSpell,    // for humans
        ReturnManaSpell,  // for towers         
        HealingSpell,  // for towers         
    }
}
