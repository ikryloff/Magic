using UnityEngine;

public class Wizard : MonoBehaviour
{
    private UIManager uI;
    public float startHp;
    public float startMana;
    private float hp_norm;
    private float mana_norm;
    private float defencePoints;
    private float manaPoints;
    private float manaCicle;
    private float manaCicleRate;
    public float temp;

    public static bool IsStopCasting;

    void Start()
    {
        uI = ObjectsHolder.Instance.uIManager;
        temp = Time.time;
        defencePoints = PlayerCharacters.GetPlayerDP ();
        manaPoints = PlayerCharacters.GetPlayerMP ();
        startHp = defencePoints;
        startMana = manaPoints;
        hp_norm = 1f;
        mana_norm = 1f;
        manaCicle = 1f;
        manaCicleRate = 1f;
        uI.SetDefenceValue (hp_norm * 100, defencePoints);
        CalcMana ();
        //PrintSpellsIDList ();
    }
    private void Update()
    {
        ManaRecoveryCicle ();
    }

    private void ManaRecoveryCicle()
    {
        if ( manaCicle <= 0 )
        {
            ManaRecover (PlayerCharacters.GetPlayerMPPS ());
            manaCicle = manaCicleRate;
        }
        manaCicle -= Time.deltaTime;
    }

    public void ManaRecover( float mPoints )
    {
        manaPoints += mPoints;
        if ( manaPoints > startMana )
        {
            manaPoints = startMana;
        }
        CalcMana ();
    }

    public void CalcDamage( float damage )
    {
        defencePoints -= damage;
        hp_norm = defencePoints / startHp;
        uI.SetDefenceValue (hp_norm * 100, defencePoints);
        PlayerCharacters.SetPlayerDP ((int)defencePoints);
        CheckDefence ();
    }

    private void CheckDefence()
    {
        if ( defencePoints <= 0 )
        {
            defencePoints = 0;
            uI.SetDefenceValue (0, defencePoints);
            PlayerCharacters.SetPlayerDP ((int)defencePoints);
            print ("GameOver");

            temp = Time.time - temp;
            Time.timeScale = 0;
            print (temp);
        }
    }

    public void ManaWaste( float mPoints )
    {
        manaPoints -= mPoints;
        if ( manaPoints < 0 )
            manaPoints = 0;
        CalcMana ();
    }

    public void CalcMana()
    {
        mana_norm = manaPoints / startMana;
        uI.SetManaValue (mana_norm * 100, (int)manaPoints);
    }

    public float GetManapoints()
    {
        return manaPoints;
    }




    //For test
    public void PrintSpellsIDList()
    {
        for ( int i = 0; i < PlayerCharacters.GetPlayerSpellsIDList ().Length; i++ )
        {
            print (PlayerCharacters.GetPlayerSpellsIDList () [i] + ", ");
        }
        print ("Quantity: " + PlayerCharacters.GetPlayerSpellsQuantity ());
    }
}
