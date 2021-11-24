using UnityEngine;

[CreateAssetMenu (fileName = "New Unit", menuName = "Unit")]
public class UnitTemplate : ScriptableObject
{
    [Header ("Base info")]
    public int unitID;
    public Unit.UnitType unitType;
    public Unit.UnitClassProperty classProperty;
    public Unit.UnitAttackPower attackPower;
    public int level;
    public int damage;
    public int health;

    [Header ("For spells and towers")]
    public string spellTag;
    public string code;
    public int cost;
    public float prepareTime;
    public int [] targetIndexes; // what cell/cells will be target cells or lines of magic action
    public SpellUnit.SpellType spellType;
    public TowerUnit.TowerType towerType;

    [Header ("For towers and humans and spell effects")]
    public int attackRange;
    public float attackRate;
    public float speed;
    public GameObject unitPrefab;
    public GameObject bulletPrefab;
    public GameObject impactPrefab;
    public GameObject deathPrefab;
    public GameObject bornPrefab;
    public Sprite activeIcon;
    public Sprite scheme;

    [Header ("For humans")]
    public float xp;

    [Header ("For pointers -1 or 1")]
    public int stepDirection;

}

