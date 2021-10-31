using UnityEngine;

public class Wizard : MonoBehaviour
{
    private UIManager uI;
    private float mana_norm;
    private float defencePoints;
    public static float ManaPoints;
    private float manaCicle;
    private float manaCicleRate;
    public float temp;


    void Start()
    {
        uI = ObjectsHolder.Instance.uIManager;
        temp = Time.time;
        ManaPoints = Player.GetPlayerMP ();
        mana_norm = 1f;
        manaCicle = 1f;
        manaCicleRate = 1f;
        CalcMana ();
        //PrintSpellsIDList ();
        GameEvents.current.OnManaWasteEvent += ManaWaste;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnManaWasteEvent -= ManaWaste;
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
        if ( ManaPoints >= Player.GetPlayerMP () )
            return;
        ManaPoints += mPoints;
        if ( ManaPoints > Player.GetPlayerMP () )
        {
            ManaPoints = Player.GetPlayerMP ();
        }
        CalcMana ();
    }


    public void ManaWaste( float mPoints )
    {
        ManaPoints -= mPoints;
        if ( ManaPoints < 0 )
            ManaPoints = 0;
        CalcMana ();
    }

    public void CalcMana()
    {
        mana_norm = ManaPoints / Player.GetPlayerMP ();
        GameEvents.current.ManaValueChangedAction (mana_norm);
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
