using UnityEngine;

public enum FiringMode { OneProjectile, SemiAutomatic, Burst, FullAutomatic }

[System.Serializable]
public class Limiter { public float left, right, up, down, forward, backward; }

public class SpaceshipOperations : MonoBehaviour
{
    [Header("Properties")]
    public float health = 100;
    public Color _Colour;

    [Header("Modes")]
    public FiringMode _firingMode;

    [Header("Objects")]
    public GameObject ship;
    public GameObject bullet;    

    [Header("Universal Cooldown Settings")]
    public float _time;    
    public float cooldownLength;

    [Header("Burst Cooldown Settings")]
    public float burstTime;
    public float burstCooldownLength;
    public float burstNo = 0;

    [Header("Renderers")]
    public Renderer[] _rendererArray;
    public Renderer _renderer;

    [System.Serializable]
    public class Checks { public bool M1, M2; public bool count; public bool burstCount; }

    public Checks _checks = new Checks();

    public Limiter movementLimiter;

    public GameLoop _gameLoop;

    public MenuSystem1D firingModeMenu;

    void Awake()
    {
        _time = 0;
        _checks.count = false;

        _rendererArray = transform.GetComponentsInChildren<Renderer>();
        _renderer = GetComponent<Renderer>();        
    }

    void Update()
    {
        #region Fire Mode Change + Output
        switch (firingModeMenu.currentSelection.gameObject.name)
        {
            case "1 Projectile": _firingMode = FiringMode.OneProjectile; break;
            case "Timed Projectile": _firingMode = FiringMode.SemiAutomatic; break;
            case "Burst Fire": _firingMode = FiringMode.Burst; break;
            case "Fully Automatic": _firingMode = FiringMode.FullAutomatic; break;
        }

        

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("A Button")) && _firingMode == FiringMode.OneProjectile)
        {
            if (UtilitySet.CheckSceneFor1Obj("Ship Projectile", ref _checks.M1))
                Fire();
        }

        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A Button")) && _firingMode == FiringMode.SemiAutomatic)
        {
            if (!(UtilitySet.IsOnCooldown(_time, ref _checks.M2)))
            {
                Fire();
                _checks.count = true;
            }
        }

        else if (Input.GetKey(KeyCode.Space) || Input.GetButton("A Button"))
        {
            if (_firingMode == FiringMode.Burst)
            {
                if (burstNo < 3 && !_checks.burstCount && !_checks.count)
                {
                    Fire();
                    burstNo++;
                    _checks.burstCount = true;
                }
                else if (burstNo >= 3)
                {
                    burstNo = 0;
                    _checks.count = true;
                }
            }
            else if (_firingMode == FiringMode.FullAutomatic)
            {
                if (!(UtilitySet.IsOnCooldown(_time, ref _checks.M2)))
                {
                    Fire();
                    _checks.count = true;
                }
            }
        }

        UtilitySet.Cooldown(ref _checks.count, ref _time, 1, cooldownLength);   //Standard single shot cooldown.
        UtilitySet.Cooldown(ref _checks.burstCount, ref burstTime, 1, burstCooldownLength);    //Burst fire cooldown. 
        #endregion

        #region Set spaceship colour
        foreach (Renderer block in _rendererArray)
        {
            if (health >= 50 && health <= 100)
                block.material.color = new Color((200 - (2 * health)) / 100, 1, 0, 1);

            else if (health >= 0 && health < 50)
                block.material.color = new Color(1, health / 50F, 0, 1);

            _Colour = block.material.color;
        }
        #endregion

        #region Restrict spaceship position to the screen's bounds
        Vector3 pos = transform.position;
        Vector3 screenInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        Vector3 RBoundSize = ship.GetComponent<Renderer>().bounds.size;

        float halfRBoundSizeX = RBoundSize.x / 2, halfRBoundSizeY = RBoundSize.y / 2;

        if (pos.x >= (screenInWorldSpace.x - halfRBoundSizeX + movementLimiter.right)) //If the 'x' value of the var representing the spaceship's position goes beyond the right end of the screen, 
        {
            pos.x = screenInWorldSpace.x - halfRBoundSizeX + movementLimiter.right; //it snaps to the right end of the screen,  
            transform.position = pos; //and the position becomes this value. 
        }
        if (pos.x <= -(screenInWorldSpace.x - halfRBoundSizeX + movementLimiter.left))
        {
            pos.x = -(screenInWorldSpace.x - halfRBoundSizeX + movementLimiter.left);
            transform.position = pos;
        }
        if (pos.y >= (screenInWorldSpace.y - halfRBoundSizeY + movementLimiter.up))
        {
            pos.y = screenInWorldSpace.y - halfRBoundSizeY + movementLimiter.up;
            transform.position = pos;
        }
        if (pos.y <= -(screenInWorldSpace.y - halfRBoundSizeY + movementLimiter.down))
        {
            pos.y = -(screenInWorldSpace.y - halfRBoundSizeY + movementLimiter.down);
            transform.position = pos;
        }
        #endregion

        #region Regulate Health              

        if (health > 100)
            health = 100;

        if (health <= 0)
        {
            health = 0;
            _gameLoop.GameOver();
        }
            
        #endregion
    }

    void Fire()
    {
        Instantiate(bullet, GameObject.Find("Ship Surface").transform.position, Quaternion.identity);
    }
}