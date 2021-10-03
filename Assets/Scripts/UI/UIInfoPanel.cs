using UnityEngine;
using UnityEngine.UIElements;

public class UIInfoPanel : MonoBehaviour
{
    UIManager uIManager;

    private VisualElement root;

    private Label defense;
    private Label mana;
    private Label exp;
    private Label manaPPS;
    private Label manaPR;
    private Label possibleSpells;

    private Label defenseValue;
    private Label manaValue;
    private Label expValue;
    private Label manaPPSValue;
    private Label manaPRValue;
    private Label possibleSpellsValue;

    private Label known;
    private Label elem;
    private Label nature;
    private Label demon;
    private Label necro;
    private Label defensive;

    private Label knownValue;
    private Label elemValue;
    private Label natureValue;
    private Label demonValue;
    private Label necroValue;
    private Label defensiveValue;

    private SpellsMaps spells;

   
    private void Start()
    {
        spells = ObjectsHolder.Instance.spells;
        SetInfoPanelText ();
    }

    public void SetRootAndInit( VisualElement _root )
    {
        root = _root;
        Init ();
    }

    private void Init()
    {
        uIManager = GetComponent<UIManager> ();

        defense = root.Query<Label> ("player-defense");
        mana = root.Query<Label> ("player-mana");
        exp = root.Query<Label> ("player-exp");
        manaPPS = root.Query<Label> ("player-mana-pps");
        manaPR = root.Query<Label> ("player-mana-return");
        possibleSpells = root.Query<Label> ("player-possible");

        defenseValue = root.Query<Label> ("player-defense-value");
        manaValue = root.Query<Label> ("player-mana-value");
        expValue = root.Query<Label> ("player-exp-value");
        manaPPSValue = root.Query<Label> ("player-mana-pps-value");
        manaPRValue = root.Query<Label> ("player-mana-return-value");
        possibleSpellsValue = root.Query<Label> ("player-possible-value");

        known = root.Query<Label> ("player-spells");
        elem = root.Query<Label> ("player-spells-elem");
        nature = root.Query<Label> ("player-spells-nature");
        demon = root.Query<Label> ("player-spells-demon");
        necro = root.Query<Label> ("player-spells-necro");
        defensive = root.Query<Label> ("player-spells-def");

        knownValue = root.Query<Label> ("player-spells-value");
        elemValue = root.Query<Label> ("player-spells-elem-value");
        natureValue = root.Query<Label> ("player-spells-nature-value");
        demonValue = root.Query<Label> ("player-spells-demon-value");
        necroValue = root.Query<Label> ("player-spells-necro-value");
        defensiveValue = root.Query<Label> ("player-spells-def-value");        

    }

    public void SetInfoPanelText()
    {
        defense.text = Localization.GetString ("defense");
        mana.text = Localization.GetString ("mana");
        exp.text = Localization.GetString ("exp");
        manaPPS.text = Localization.GetString ("manaPPS");
        manaPR.text = Localization.GetString ("manaPR");
        possibleSpells.text = Localization.GetString ("possibleSpells");

        defenseValue.text = PlayerCharacters.GetPlayerDP ().ToString ();
        manaValue.text = PlayerCharacters.GetPlayerMP ().ToString ();
        expValue.text = PlayerCharacters.GetPlayerXP ().ToString ();
        manaPPSValue.text = PlayerCharacters.GetPlayerMPPS ().ToString ();
        manaPRValue.text = (PlayerCharacters.GetPlayerManaReturn () * 100).ToString ();

        known.text = Localization.GetString ("known");
        elem.text = Localization.GetString ("elemental_stat");
        nature.text = Localization.GetString ("natural_stat");
        demon.text = Localization.GetString ("demon_stat");
        necro.text = Localization.GetString ("necro_stat");
        defensive.text = Localization.GetString ("defencive_stat");

        knownValue.text = PlayerCharacters.GetPlayerSpellsQuantity ().ToString ();
        elemValue.text = spells.GetSchoolLearnedSpells(spells.GetElementalListByIndex(0), spells.GetElementalListByIndex (1)).ToString();
        natureValue.text = spells.GetSchoolLearnedSpells (spells.GetNatureListByIndex (0), spells.GetNatureListByIndex (1)).ToString ();
        demonValue.text = spells.GetSchoolLearnedSpells (spells.GetDemonologyListByIndex (0), spells.GetDemonologyListByIndex (1)).ToString ();
        necroValue.text = spells.GetSchoolLearnedSpells (spells.GetNecromancyListByIndex (0), spells.GetNecromancyListByIndex (1)).ToString (); 
        defensiveValue.text = spells.GetSchoolLearnedSpells (spells.GetDefensiveListByIndex (0), spells.GetDefensiveListByIndex (1)).ToString ();
    }
}
