using UnityEngine;
using System.Collections.Generic;

public class MenuSystem : MonoBehaviour
{
    [Header("Menu Objects")]
    public GameObject currentSelection;
    public GameObject[] OptionsList;
    [SerializeField]
    protected List<GameObject> otherOptions = new List<GameObject>();

    private bool scroll;

    bool AxisIsInUse() { if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)) { return true; } return false; }

    void Start() { currentSelection = OptionsList[0]; scroll = false; }
      
    void Update()
    {
        if (AxisIsInUse() && !scroll)
        {
            scroll = true;
            Selection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0, currentSelection, otherOptions);
        }
        else if (!AxisIsInUse())
            scroll = false;

        /*
        ////////////////////////////////////////////////////////extract this if MenuSystem will be used in other applications. 
        foreach (GameObject op in objs.OptionsList)
        {
            if (op == objs.currentSelection)
            {
                op.GetComponent<TextMesh>().color = Color.red;
            }
            else
            {
                op.GetComponent<TextMesh>().color = Color.white;
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        */
    }

    void Selection(float directionX, float directionY, float directionZ, GameObject originObject, List<GameObject> destinations)
    {
        Vector3 direction = new Vector3(directionX, directionY, directionZ);
        RaycastHit[] hitObjects;
        Ray r = new Ray(originObject.transform.position, direction);
        hitObjects = Physics.RaycastAll(r);
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].transform.gameObject.name != originObject.name)
            {
                destinations.Add(hitObjects[i].transform.gameObject);
                if (i == (hitObjects.Length - 1))
                {
                    GameObject[] destinationsA = destinations.ToArray();
                    GameObject closest = null;
                    float minimumDistance = Mathf.Infinity;
                    Vector3 currentPosition = originObject.transform.position;
                    foreach (GameObject o in destinationsA)
                    {
                        float dist = Vector3.Distance(o.transform.position, currentPosition);

                        if (dist < minimumDistance)
                        {
                            closest = o;
                            minimumDistance = dist;
                            currentSelection = closest;
                        }
                    }
                    destinations.Clear();
                }
            }
        }
    }
}
