using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Wizard wizard;
    [SerializeField] private WaveController waveController;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpellsMaps spellsMaps;
    [SerializeField] private Player player;

    public static GameState State;

    private void Awake()
    {
        GameEvents.current.OnGameStateChangedEvent += ChangeGameState;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnGameStateChangedEvent -= ChangeGameState;
    }

    private void Start()
    {
        ChangeGameState (GameState.StartGame);
    }

    private void StartGame()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameEvents.current.SwitchTouch (true);
        Debug.Log ("State: StartGame");
        Debug.Log ("GameManager");
        uIManager.OpenLevelMenu ();
        
    }

    public void InitLevel( int level )
    {
        spellsMaps.Init ();
        Storage.LoadData (player, level);
        wizard.Init ();
        waveController.Init (level);
        uIManager.StartGame ();
        Debug.Log ("Init level " + level);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene (0);
        StartGame ();
    }

    public void SaveGame()
    {
        Storage.SaveData (player, player.GetPlayerStage () + 1);
    }

    private void ChangeGameState( GameState newState )
    {

        State = newState;

        switch ( State )
        {
            case GameState.StartGame:
                StartGame ();
                break;
            case GameState.BoardSleep:
                BoardSleep ();
                break;
            case GameState.BoardActive:
                BoardActive ();
                break;
            case GameState.PauseGame:
                PauseGame ();
                break;
            case GameState.ResumeGame:
                ResumeGame ();
                break;
            case GameState.FastGame:
                FastGame ();
                break;
            case GameState.GameOver:
                PauseGame ();
                GameOver ();
                break;
            case GameState.LevelComplete:
                PauseGame ();
                LevelComplete ();
                break;
        }
    }


    private void BoardSleep()
    {
        Debug.Log ("BoardSleep");
        GameEvents.current.SwitchTouch (false);
    }
    private void BoardActive()
    {
        Debug.Log ("BoardActive");
        GameEvents.current.StopCastingAction ();
        GameEvents.current.SwitchTouch (true);
    }

    private void PauseGame()
    {
        Debug.Log ("Pause");
        GameEvents.current.SwitchTouch (false);
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Debug.Log ("Play");
        GameEvents.current.SwitchTouch (true);
        Time.timeScale = 1;
    }

    private void FastGame()
    {
        if ( Time.timeScale > 1 )
        {
            ChangeGameState (GameState.ResumeGame);
        }
        else
        {
            Debug.Log ("Fast");
            Time.timeScale = 20;
        }

    }

    private void LevelComplete()
    {
        Debug.Log ("Level Copmlete");
    }

    private void GameOver()
    {
        Debug.Log ("Game Over");
    }

    public enum GameState
    {
        StartGame,
        BoardSleep,
        BoardActive,
        PauseGame,
        ResumeGame,
        FastGame,
        GameOver,
        LevelComplete,
    }


}
