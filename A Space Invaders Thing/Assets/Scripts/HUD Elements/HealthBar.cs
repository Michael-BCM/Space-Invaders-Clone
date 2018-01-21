using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    protected Renderer healthBarRenderer;
    [SerializeField]
    protected SpaceshipOperations SO;
        
    void Update()
    {
        if(SO.health > -1)
        {
            transform.localScale = new Vector3(SO.health / 10, 1, 1);
            healthBarRenderer.material.color = SO._Colour;
        }        
    }
}
