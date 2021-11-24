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
    [SerializeField] private GameObject _spellItemView;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _wizardView;


    private UISpellsManager uISpellsManager;
    private SpellPropertiesView _spellPropertiesView;


    private int defencePoints;
    private int manaPoints;
    private int xpPoints;

    private void Awake()
    {
        uISpellsManager = GetComponent<UISpellsManager> ();
        _spellPropertiesView = _spellItemView.GetComponent<SpellPropertiesView> ();

        GameEvents.current.OnItemButtonClickedEvent += OpenSpellView;

        // Events
        //GameEvents.current.OnNewGameMessage += SetMessage;
    }


    private void OnDestroy()
    {
        GameEvents.current.OnNewGameMessage -= SetMessage;
        GameEvents.current.OnItemButtonClickedEvent -= OpenSpellView;
    }

    private void Start()
    {
        CleanMessage ();
        PrintSpellsQuantity ();
        _gameMenu.SetActive (false);
        CloseSpellsPanel ();
    }

    public void OpenMenu()
    {
        StopSpelling ();
        _gameMenu.SetActive (true);
    }

    public void CloseMenu()
    {
        ResumeSpelling ();
        _gameMenu.SetActive (false);
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

    private void OpenSpellView(UnitTemplate unitTemplate)
    {
        _spellItemView.SetActive (true);
        _spellPropertiesView.UpdateView (unitTemplate);
        
    }

    public void OpenWizardView()
    {
        _wizardView.SetActive (true);

    }

    public void CloseWizardView()
    {
        _wizardView.SetActive (false);

    }


    public void OpenSpellsPanel()
    {
        Debug.Log ("Open");
        _spellsPanel.SetActive (true);
        _inGameUI.SetActive (false);
        _spellItemView.SetActive (false);
        GameEvents.current.GameStateChangedAction (GameManager.GameState.PauseGame);
        GameEvents.current.TabChangeAction (TabButton.SchoolIndex);
    }

    public void CloseSpellsPanel()
    {
        Debug.Log ("Close");
        _spellsPanel.SetActive (false);
        _spellItemView.SetActive (false);
        _inGameUI.SetActive (true);
        CloseWizardView ();
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
