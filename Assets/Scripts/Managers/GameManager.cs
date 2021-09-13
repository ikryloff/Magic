using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameState State;

    private void Awake()
    {
        GameEvents.current.OnGameStateChangedAction += ChangeGameState;
    }

    private void OnDisable()
    {
        GameEvents.current.OnGameStateChangedAction -= ChangeGameState;
    }

    private void Start()
    {
        StartGame ();
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
        }


    }

    private void StartGame()
    {
        StartCoroutine (GameStartWithDelay ());        

    }

    private void BoardSleep()
    {
        GameEvents.current.SwitchTouch (false);
    }
    private void BoardActive()
    {
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

    public enum GameState
    {
        InitObjects,
        StartGame,
        BoardSleep,
        BoardActive,
        PauseGame,
        ResumeGame,

    }

    IEnumerator GameStartWithDelay()
    {
        GameEvents.current.SwitchTouch (false);
        Time.timeScale = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlayerCharacters.SetPlayerLevel (1);
        yield return new WaitForSeconds (0.5f);
        GameEvents.current.SwitchTouch (true);
        Debug.Log ("State: StartGame");
        Debug.Log ("GameManager");
    }
}
