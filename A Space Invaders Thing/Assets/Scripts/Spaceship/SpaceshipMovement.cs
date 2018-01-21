using UnityEngine;

public class Directions { public Vector3 Up, Down, Left, Right; }

public class SpaceshipMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    protected float speed = 10F;

    [Header("Modes")]
    [SerializeField]
    public ControlType movementMode = ControlType.Keyboard;

    private Directions _directions = new Directions();

    public bool changeOnce, changeOnce0;

    KeyCode[] Directions = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    void Start()
    {
        _directions.Up = Camera.main.transform.InverseTransformDirection(Camera.main.transform.up) * speed;
        _directions.Down = -_directions.Up;
        _directions.Right = Camera.main.transform.InverseTransformDirection(Camera.main.transform.forward) * speed;
        _directions.Left = -_directions.Right;

        changeOnce = changeOnce0 = false;
    }

    void Update()
    {
        if (UtilitySet.JoyAxisIsInUse())
        {
            movementMode = ControlType.Controller;
        }

        foreach (KeyCode dir in Directions)
            if (Input.GetKeyDown(dir)) //If the user pushes a directional key,
            {
                movementMode = ControlType.Keyboard; //change the movement mode type to Keyboard.
            }

        if (movementMode == ControlType.Keyboard)
        {
            UtilitySet.MoveOnKeyboardPress(transform, new KeyCode[] { KeyCode.W, KeyCode.UpArrow }, _directions.Up);
            UtilitySet.MoveOnKeyboardPress(transform, new KeyCode[] { KeyCode.A, KeyCode.LeftArrow }, _directions.Left);
            UtilitySet.MoveOnKeyboardPress(transform, new KeyCode[] { KeyCode.S, KeyCode.DownArrow }, _directions.Down);
            UtilitySet.MoveOnKeyboardPress(transform, new KeyCode[] { KeyCode.D, KeyCode.RightArrow }, _directions.Right);
        }
        else if (movementMode == ControlType.Controller)
        {
            Transform cam = Camera.main.transform;
            transform.Translate(Input.GetAxis("Horizontal") * cam.InverseTransformDirection(cam.forward) * Time.deltaTime * speed);
            transform.Translate(Input.GetAxis("Vertical") * cam.InverseTransformDirection(cam.up) * Time.deltaTime * speed);
        }
    }
}
