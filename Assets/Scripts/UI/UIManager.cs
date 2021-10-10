using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SpellsMaps spells;
    Button menuButton;
    Button speedButton;
    [SerializeField] private GameObject _spellsPanel;
    [SerializeField] private GameObject _inGameUI;
    

    private UISpellsManager uISpellsManager;
    private UIMenuManager uIMenuManager;
    private UIInfoPanel uIInfoPanel;

    private int defencePoints;
    private int manaPoints;
    private int xpPoints;

    private void Awake()
    {
        uISpellsManager = GetComponent<UISpellsManager> ();
        uIMenuManager = GetComponent<UIMenuManager> ();
        uIInfoPanel = GetComponent<UIInfoPanel> ();
        

        // Events
        //GameEvents.current.OnNewGameMessage += SetMessage;
    }


    private void OnDisable()
    {
        GameEvents.current.OnNewGameMessage -= SetMessage;
    }

    private void Start()
    {
        CleanMessage ();
        PrintSpellsQuantity ();
        SetPrepareValue (100);
        SetDefaultPrepareIcon ();
    }

    public void OpenMenu()
    {
        StopSpelling ();
        uIMenuManager.OpenMenu ();
    }

    public void SpeedGame()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.FastGame);
    }


    public void SetMessage( string mess )
    {
        StopCoroutine (Message (mess));
        StartCoroutine (Message (mess));
    }

    IEnumerator Message( string mess )
    {
        float messTime = 2.5f;
        while ( messTime > 0 )
        {
            messTime -= Time.deltaTime;
            yield return null;

        }
        CleanMessage ();
    }



    public void CleanMessage()
    {
       
    }

    public void PrintSpellsQuantity()
    {
        
    }

    public void OpenSpellsPanel()
    {
        Debug.Log ("Open");
        _spellsPanel.SetActive (true);
        _inGameUI.SetActive (false);
        GameEvents.current.GameStateChangedAction (GameManager.GameState.PauseGame);
    }

    public void CloseSpellsPanel()
    {
        Debug.Log ("Close");
        _spellsPanel.SetActive (false);
        _inGameUI.SetActive (true);
        GameEvents.current.GameStateChangedAction (GameManager.GameState.ResumeGame);
    }

    public void SetPrepareValue( float value )
    {
        
        
    }

    public void SetManaValue( float value, float points )
    {
        
    }

    public void SetDefenceValue( float value, float points )
    {
        
    }

    public void SetXPoints( int value )
    {
        
    }

    public void StopSpelling()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.PauseGame);
    }
    public void ResumeSpelling()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.ResumeGame);
    }


    public void SetPrepareIcon( UnitTemplate et )
    {
        
    }

    public void SetDefaultPrepareIcon()
    {
        
    }

}
