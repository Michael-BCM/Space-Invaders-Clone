using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    protected float _time, distance, speed, timeLimit;
    [SerializeField]
    protected Vector3 origin;

    void Start() { _time = 0; origin = transform.position; distance = 0.2F; speed = 1; timeLimit = 0.5F; }

    void Update ()
    {
        if (transform.position.y < origin.y + distance)
            transform.Translate(0, speed * Time.deltaTime, 0);

        else if (transform.position.y >= origin.y + distance)
            _time += 1 * Time.deltaTime;

        if (_time > timeLimit)
            Destroy(gameObject);
    }    
}
