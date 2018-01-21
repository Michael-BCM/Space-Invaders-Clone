using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    protected float _time;
    [SerializeField]
    protected float timeLimit;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float interval;
            
    public void WaitAndDestroy ()
    {
        _time += interval * Time.deltaTime;

        if (_time >= timeLimit)
            Destroy(gameObject);
    }
}