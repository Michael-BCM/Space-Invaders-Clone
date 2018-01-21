using UnityEngine;

[System.Serializable]
public class FloatSides { public float left, right, top, bottom; }

[System.Serializable]
public class Vector3Directions
{
    public Vector3 left, right, up, down, forward, back;

    public void SetLeftRightDown(Vector3 _left, Vector3 _right, Vector3 _down)
    {
        left = _left;
        right = _right;
        down = _down;
    }
}

public class TheAlienInvasion : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    protected FloatSides bounds;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float downInterval;

    [SerializeField]
    protected Vector3Directions directions;
    [SerializeField]
    protected Vector3 origin;

    [Header("Objects")]
    [SerializeField]
    protected GameObject spawner;

    [System.Serializable]
    public class Limits { public float[] rightVals, leftVals; public bool[] resetL, resetR; public int c0, c1; }    
    public Limits lims = new Limits();
    
    void Awake()
    {
        speed = 0.5F;

        downInterval = 0.2F;

        origin = transform.position;
        bounds.top = origin.y;

        lims.c0 = 0;
        lims.c1 = 1;
        
        lims.rightVals = new float[] { bounds.top, bounds.top - (2 * downInterval), bounds.top - (4 * downInterval), bounds.top - (6 * downInterval), bounds.top - (8 * downInterval), bounds.top - (10 * downInterval), bounds.top - (12 * downInterval), bounds.top - (14 * downInterval) };
        lims.leftVals = new float[] { bounds.top - downInterval, bounds.top - (3 * downInterval), bounds.top - (5 * downInterval), bounds.top - (7 * downInterval), bounds.top - (9 * downInterval), bounds.top - (11 * downInterval), bounds.top - (13 * downInterval), -bounds.top - (15 * downInterval) };
        
        lims.resetL = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        lims.resetR = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

        directions.SetLeftRightDown(new Vector3(-speed, 0, 0), -directions.left, new Vector3(0, -speed, 0));
    }

    void Update ()
    {
        #region Moves the alien(s) back and forth, Space Invaders style.

        UtilitySet.moveIfOutOfBounds(transform, transform.position.x, bounds.left, bounds.right, directions.down);
        UtilitySet.moveIfOnALine(transform, lims.leftVals, transform.position.y, new Vector3(-speed, 0, 0));
        UtilitySet.moveIfOnALine(transform, lims.rightVals, transform.position.y, new Vector3(speed, 0, 0));
                
        if (lims.c0 < 7 && lims.c1 < 8)
        {
            if (transform.position.y < lims.leftVals[lims.c0] && !lims.resetL[lims.c0])
            {
                lims.resetL[lims.c0] = true;
                transform.position = new Vector3(bounds.right, lims.leftVals[lims.c0], origin.z);
                lims.c0 += 1;
            }

            if (transform.position.y < lims.rightVals[lims.c1] && !lims.resetR[lims.c0])
            {
                lims.resetR[lims.c0] = true;
                transform.position = new Vector3(bounds.left, lims.rightVals[lims.c1], origin.z);
                lims.c1 += 1;
            }
        }
        #endregion

        #region Spawn a new group of invaders, when the current group is destroyed. 
        if (transform.childCount <= 0)
        {
            InstantiationObject _spawner = new InstantiationObject(spawner, origin, transform.rotation);
            UtilitySet.DestroyAndSpawn(gameObject, _spawner);
        }
        #endregion
    }
}


