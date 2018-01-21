using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    void Start()
    {
        InstantiationObject IO = new InstantiationObject(objectToSpawn, transform.position, transform.rotation);
        UtilitySet.DestroyAndSpawn(gameObject, IO);
    }
}
