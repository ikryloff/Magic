using UnityEngine;
using TMPro;

public class Wizard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaValue;
    [SerializeField] private TextMeshProUGUI xpValue;

    private float mana_norm;
    private float defencePoints;
    public static float ManaPoints;
    private float manaCicle;
    private float manaCicleRate;


    void Start()
    {
        ManaPoints = LevelBook.GetPlayerManaPoints();
        Debug.Log (ManaPoints);
        mana_norm = 1f;
        manaCicle = 1f;
        manaCicleRate = 1f;
        CalcAndShowManaPoints ();
        ShowXPPoints ();
        GameEvents.current.OnManaWasteEvent += ManaWaste;
        GameEvents.current.OnDieEvent += CalcXP;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnManaWasteEvent -= ManaWaste;
        GameEvents.current.OnDieEvent -= CalcXP;
    }

    private void Update()
    {
        ManaRecoveryCicle ();
    }

    private void ManaRecoveryCicle()
    {
        if ( manaCicle <= 0 )
        {
            ManaRecover (Player.GetPlayerMPPS ());
            manaCicle = manaCicleRate;
        }
        manaCicle -= Time.deltaTime;
    }

    public void ManaRecover( float mPoints )
    {
        if ( ManaPoints >= LevelBook.GetPlayerManaPoints () )
            return;
        ManaPoints += mPoints;
        if ( ManaPoints > LevelBook.GetPlayerManaPoints () )
        {
            ManaPoints = LevelBook.GetPlayerManaPoints ();
        }
        CalcAndShowManaPoints ();
    }


    public void ManaWaste( float mPoints )
    {
        ManaPoints -= mPoints;
        if ( ManaPoints < 0 )
            ManaPoints = 0;
        CalcAndShowManaPoints ();
    }

    public void CalcAndShowManaPoints()
    {
        mana_norm = ManaPoints / LevelBook.GetPlayerManaPoints ();
        manaValue.text = Mathf.RoundToInt (ManaPoints).ToString();
        GameEvents.current.ManaValueChangedAction (mana_norm);
    }

    public void ShowXPPoints()
    {
        xpValue.text = LevelBook.GetXPPoints ().ToString ();
    }

    public void CalcXP(BoardUnit unit)
    {
        if ( unit.GetUnitType () != Unit.UnitType.Human ) return;
        LevelBook.AddXPPoints (unit.GetUnitTemplate ().xp);
        ShowXPPoints ();
    }


    public static float GetManapoints()
    {
        return ManaPoints;
    }




    //For test
    public void PrintSpellsIDList()
    {
        for ( int i = 0; i < Player.GetPlayerSpellsIDList ().Length; i++ )
        {
            print (Player.GetPlayerSpellsIDList () [i] + ", ");
        }
        print ("Quantity: " + Player.GetPlayerSpellsQuantity ());
    }
}
