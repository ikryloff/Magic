public class Unit : StateMachine
{
    protected UnitType _unitType;

    public enum UnitType
    {
        Spell,
        Tower,
        Human,
    }

    public enum UnitClassProperty
    {
        Elemental,
        Nature,
        Demonology,
        Necromancy,
        Deffence,
        Hunter,
        Bravery,
        Nobility,
        Holiness,
        Immunity
    }

    public enum UnitAttackPower
    {
        None,
        AllInLine,
        NearestInLine,
        RandomInLine,
        All,

    }

    public UnitType GetUnitType()
    {
        return _unitType;
    }
}
