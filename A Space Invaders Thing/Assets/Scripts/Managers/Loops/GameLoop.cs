using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("Objects")]
    public GameObject firingModeTile;

    [Header("Spaceship Script Accessors")]
    public SpaceshipOperations _SpaceshipOperations;
    public SpaceshipMovement _SpaceshipMovement;

    [Header("Statistics")]
    public int score;

    public MenuSystem1D firingModeMenu;

    private Vector3 tileDifference;

    private class Checks { public bool start; }
    private Checks _checks = new Checks();
    private bool set;
    private void setUp() { set = true; }

    [SerializeField]
    private SceneFader gameOver;

    void Awake()
    {
        _checks.start = false;

        _SpaceshipOperations._firingMode = FiringMode.OneProjectile;
        _SpaceshipOperations.health = 100;
        score = 0;

        tileDifference = new Vector3(0, 0, 0.5F);
    }

    void Start() { Invoke("setUp", 0.05F); }

    void Update()
    {
        if (set)
            firingModeTile.transform.position = firingModeMenu.currentSelection.transform.position + tileDifference;
    }

    public void GameOver()
    {
        Destroy(_SpaceshipMovement.gameObject);

        gameOver.FadeTo("Main Menu");
        //The ship explodes into particles, and the words 'Game Over' appear in its place. 

        //Hide the HUD.

        //The aliens stop moving and firing and all move to a portal on the screen, starting at random times and at random speeds. 
        //After reaching the portal, they vanish. 
    }    
}

/*
 * 
    public GameObject moveModeTile;
 * 
        #region Movement Mode Toggle
        if (_SpaceshipMovement.movementMode == ControlType.Keyboard)
        {
            Vector3 destPos = GameObject.Find("WASD").transform.position;
            moveModeTile.transform.position = destPos + tileDifference;
        }
        else if (_SpaceshipMovement.movementMode == ControlType.Controller)
        {
            Vector3 destPos = GameObject.Find("Left Thumbstick").transform.position;
            moveModeTile.transform.position = destPos + tileDifference;
        }
        #endregion
    */
