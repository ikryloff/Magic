using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _spellsPanel;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _spellItemView;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _levelMenu;
    [SerializeField] private GameObject _wizardView;

    private LevelMenuPanel levelMenuPanel;

    private SpellPropertiesView _spellPropertiesView;

    private void Awake()
    {
        _spellPropertiesView = _spellItemView.GetComponent<SpellPropertiesView> ();

        GameEvents.current.OnItemButtonClickedEvent += OpenSpellView;

        levelMenuPanel = _levelMenu.transform.GetComponentInChildren<LevelMenuPanel> ();
    }


    private void OnDestroy()
    {
        GameEvents.current.OnItemButtonClickedEvent -= OpenSpellView;
    }

    public void OpenLevelMenu()
    {
        _levelMenu.SetActive (true);
        levelMenuPanel.InitMenu ();
    }

    private void CloseLevelMenu()
    {
        _levelMenu.SetActive (false);
    }

    public void StartGame()
    {
        CloseLevelMenu ();
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
