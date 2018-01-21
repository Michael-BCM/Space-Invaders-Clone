using UnityEngine;

public class InstantiationObject
{
    public GameObject Obj;
    public Vector3 Pos;
    public Quaternion Rot;

    public InstantiationObject() { }

    public InstantiationObject (GameObject O, Vector3 P, Quaternion R)
    {
        Obj = O;
        Pos = P;
        Rot = R;
    }

    public void Set(GameObject O, Vector3 P, Quaternion R)
    {
        Obj = O;
        Pos = P;
        Rot = R;
    }    
}