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
    

    private int defencePoints;
    private int manaPoints;
    private int xpPoints;

    private void Awake()
    {
        uISpellsManager = GetComponent<UISpellsManager> ();
        
        

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
        CloseSpellsPanel ();
    }

    public void OpenMenu()
    {
        StopSpelling ();
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

    public void StopSpelling()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.PauseGame);
    }
    public void ResumeSpelling()
    {
        GameEvents.current.GameStateChangedAction (GameManager.GameState.ResumeGame);
    }

}
