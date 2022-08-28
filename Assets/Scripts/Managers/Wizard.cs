using TMPro;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaValue;
    [SerializeField] private TextMeshProUGUI xpValue;

    public static int count = 0;

    private float mana_norm;
    private float defencePoints;
    public static float ManaPoints;
    private float manaCicle;
    private float manaCicleRate;
    private bool isOn;

    private Player player;


    private void Awake()
    {
        GameEvents.current.OnManaWasteEvent += ManaWaste;
        GameEvents.current.OnDieEvent += CalcXP;
        player = GetComponent<Player> ();
    }

    private void OnDestroy()
    {
        GameEvents.current.OnManaWasteEvent -= ManaWaste;
        GameEvents.current.OnDieEvent -= CalcXP;
    }

    private void Update()
    {
        if ( !isOn ) return;
        ManaRecoveryCicle ();
    }

    public void Init()
    {
        UpdateManaValues ();
        mana_norm = 1f;
        manaCicle = 1f;
        manaCicleRate = 1f;
        ShowXPPoints ();
        isOn = true;
    }


    private void UpdateManaValues()
    {
        ManaPoints = player.GetManaPoints();
        CalcAndShowManaPoints ();
    }

    private void ManaRecoveryCicle()
    {
        if ( manaCicle <= 0 )
        {
            ManaRecover (1f);
            manaCicle = manaCicleRate;
        }
        manaCicle -= Time.deltaTime;
    }

    public void ManaRecover( float mPoints )
    {
        float playerMana = player.GetManaPoints ();
        if ( ManaPoints >= playerMana )
            return;
        ManaPoints += mPoints;
        if ( ManaPoints > playerMana )
        {
            ManaPoints = playerMana;
        }
        CalcAndShowManaPoints ();
    }


    private void ManaWaste( float mPoints )
    {
        count++;
        Debug.Log (count + " " + this.GetInstanceID().ToString());
        Debug.Log (mPoints + " WASTED");
        ManaPoints -= mPoints;
        if ( ManaPoints < 0 )
            ManaPoints = 0;
        CalcAndShowManaPoints ();
    }

    public void CalcAndShowManaPoints()
    {
        mana_norm = ManaPoints / player.GetManaPoints ();
        manaValue.text = Mathf.RoundToInt (ManaPoints).ToString ();
        GameEvents.current.ManaValueChangedAction (mana_norm);
    }

    public void ShowXPPoints()
    {
        xpValue.text = player.GetXPPoints ().ToString ();
    }

    public void CalcXP( BoardUnit unit )
    {
        if ( unit.GetUnitType () != Unit.UnitType.Human ) return;
        player.AddXPPoints (unit.GetUnitTemplate ().xp);
        ShowXPPoints ();
    }


    public static float GetManapoints()
    {
        return ManaPoints;
    }


    //For test
    public void PrintSpellsIDList()
    {
        for ( int i = 0; i < player.GetSpellsIDList ().Length; i++ )
        {
            print (player.GetSpellsIDList () [i] + ", ");
        }
        print ("Quantity: " + player.GetPlayerSpellsQuantity ());
    }
}
