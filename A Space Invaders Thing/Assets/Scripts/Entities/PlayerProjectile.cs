using UnityEngine;

public enum side { left, right, top, bottom, front, back };

public class PlayerProjectile : Projectile
{
    [SerializeField]
    protected float damper, dampingRate;
    protected side dampSide;
    
    void Start ()
    {
        
        speed = 8F;
        _time = 0;
        interval = timeLimit = 1;

        /* Bullet curvature

        dampingRate = 10F;

        damper = dampingRate * Input.GetAxis("Horizontal");

        if (damper > 0)
            dampSide = side.left;

        else if (damper < 0)
            dampSide = side.right;
            
         */
    }
    
    void Update ()
    {
        WaitAndDestroy();

        /* Bullet curvature
        DampenMovement();
        transform.Translate((new Vector3(damper, speed, 0) * Time.deltaTime));
        */

        transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
    }

    void DampenMovement()
    {
        if (dampSide == side.left)
        {
            damper -= dampingRate * Time.deltaTime;

            if (damper < 0)
                damper = 0;
        }

        else if (dampSide == side.right)
        {
            damper += dampingRate * Time.deltaTime;

            if (damper > 0)
                damper = 0;
        }
    }

    void OnTriggerEnter (Collider o)
    {
        if (o.gameObject.tag == "Alien")
            Destroy(gameObject);
    }
}