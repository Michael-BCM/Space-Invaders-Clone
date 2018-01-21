using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public Animation Anim;
    public AnimationClip[] Selected;
    public MenuMaster _menuMaster;
    public MenuSystem1D _menuSystem;
    
    private void Update()
    {
        if(_menuSystem.enabled)
        {
            if (_isSelected())
                Anim.Play(Selected[0].name);
            else
            {
                Anim.Stop(Selected[0].name);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private bool _isSelected ()
    {
        if (_menuSystem.currentSelection.name == gameObject.name)
            return true;
        return false;
    }
}