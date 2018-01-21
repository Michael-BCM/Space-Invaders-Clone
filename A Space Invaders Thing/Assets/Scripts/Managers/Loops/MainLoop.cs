using UnityEngine;

public enum GameState { Playing, Paused, Inventory, Menu} //Playing = in game. Paused = in pause menu. Inventory = in a menu in game (Pip-Boy, etc). Menu = in a (main?) menu. 

public class MainLoop : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField]
    protected MenuSystem pauseMenu, mainMenu;
    
    private class Checks { public bool axisIsInUse; public bool camSetup; }
    private Checks _checks = new Checks();

    [Header("States")]
    public GameState _gameState;       
    
    public GameObject[] cameras = new GameObject[] { };

    void Awake()
    {
        _checks.axisIsInUse = _checks.camSetup = false;

        _gameState = GameState.Menu;
    }

    void Update ()
    {
        PauseGame();
        ResumeGame();
        QuitToMenu();
        SetUpCamsOnce();
    }

    void PauseGame()
    {
        if (Input.GetButtonDown("Start Button") && _gameState == GameState.Playing)
        {
            _gameState = GameState.Paused;
            ResetCamCheck();
        }
    }

    void ResumeGame()
    {
        if (Input.GetButtonDown("A Button") && _gameState == GameState.Paused)
        {
            if (pauseMenu.currentSelection == pauseMenu.OptionsList[0])
            {
                _gameState = GameState.Playing;
                ResetCamCheck();
            }
        }
    }

    void QuitToMenu()
    {
        if (Input.GetButtonDown("A Button") && _gameState == GameState.Paused)
        {
            if (pauseMenu.currentSelection == pauseMenu.OptionsList[2])
            {
                _gameState = GameState.Menu;
                ResetCamCheck();
            }
        }
    }

    public void SetUpCamsOnce()
    {
        if (_gameState == GameState.Menu && !_checks.camSetup)
        {
            _checks.camSetup = true;
            foreach (GameObject c in cameras)
                c.SetActive(false);

            cameras[0].SetActive(true);
        }
        else if (_gameState == GameState.Playing && !_checks.camSetup)
        {
            _checks.camSetup = true;
            foreach (GameObject c in cameras)
                c.SetActive(false);

            cameras[1].SetActive(true);
        }
        else if (_gameState == GameState.Paused && !_checks.camSetup)
        {
            _checks.camSetup = true;
            foreach (GameObject c in cameras)
                c.SetActive(false);

            cameras[2].SetActive(true);
        }
    }

    public void AllFalseOneTrue(int state, int stateNum, bool checkOnce, GameObject[] array, GameObject arrayItem, int index)
    {
        if (state == stateNum && !checkOnce)
        {
            checkOnce = true;

            foreach (GameObject i in array)
                i.SetActive(false);
        }
    }

    public void ResetCamCheck()
    {
        _checks.camSetup = false;
    }

}
