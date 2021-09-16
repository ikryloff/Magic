using UnityEngine;

[CreateAssetMenu (fileName = "New Unit", menuName = "Unit")]
public class UnitTemplate : ScriptableObject
{
    [Header ("Base info")]
    public int unitID;
    public string unitName;
    public Unit.UnitType unitType;
    public Unit.UnitClassProperty classProperty;
    public Unit.UnitAttackPower attackPower;
    public string description;
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

    [Header ("For towers and humans")]
    public int attackRange;
    public float attackRate;
    public GameObject unitPrefab;
    public GameObject bulletPrefab;
    public GameObject impactPrefab;
    public GameObject deathPrefab;
    public Texture2D activeIcon;
    public Texture2D unActiveIcon;

    [Header ("For humans")]
    public float speed;
    public float xp;

}

