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
        Commons,
        Regulars,
        Nobles,
        Holies,
        Invincibles
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
