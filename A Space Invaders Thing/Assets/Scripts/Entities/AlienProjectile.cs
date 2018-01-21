using UnityEngine;

public class AlienProjectile : Projectile
{
    void Start() { speed = -8F; _time = 0; timeLimit = 1; interval = 1; }

    void Update() { WaitAndDestroy(); transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime); }

    void OnTriggerEnter (Collider o)
    {
        if(o.gameObject.tag == "Player")
        {
            o.GetComponent<SpaceshipOperations>().health -= 5;
            Destroy(gameObject);
        }
    }
}

//Program aliens to shoot in a direction dependent on the current pull direction of the left thumbstick? 