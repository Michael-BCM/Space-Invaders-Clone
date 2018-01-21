using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected GameObject numberPopUp;
    [SerializeField]
    protected InstantiationObject numberPopUpIO;

    [Header("Statistics"), SerializeField]
    protected int maximumHitPoints;
    [SerializeField]
    protected int hitPoints;
    [SerializeField]
    protected GameLoop _GameLoop;

    [Header("Mechanics")]
    [SerializeField]
    protected bool fire;
    [SerializeField]
    protected float timer = 0;

    [Header("Projectile Frequency")]
    [SerializeField]
    protected float minRandNum = 0;
    [SerializeField]
    protected float maxRandNum = 12;
    [SerializeField]
    protected float threshold = 9;

    void Awake()
    {
        numberPopUpIO = new InstantiationObject(numberPopUp, transform.position, Quaternion.identity);
        _GameLoop = GameObject.Find("Game Manager").GetComponent<GameLoop>();
    }

    void OnTriggerEnter (Collider o)
    {
        if(o.gameObject.tag == "Ship Projectile")
        {
            _GameLoop.score += 1;
            UtilitySet.DestroyAndSpawn(gameObject.transform.parent.gameObject, numberPopUpIO);
        }
    }
    
    void Update ()
    {
        Vector3 currentPos = transform.position;
        timer += 1 * Time.deltaTime;

        if(timer >= 1)
        {
            fire = true;
            timer = 0;            
        }
        UtilitySet.SpawnObjectRandomTime(ref fire, minRandNum, maxRandNum, threshold, projectile, currentPos, Quaternion.identity);
    }
}
