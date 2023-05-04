using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public KeyCode SprintKey { get; private set; } = KeyCode.LeftShift;
    public KeyCode CroughKey { get; private set; } = KeyCode.LeftControl;
    public KeyCode JumpKey { get; private set; } = KeyCode.Space;
    public KeyCode TakeKey { get; private set; } = KeyCode.E;
    public KeyCode ThrowKey { get; private set; } = KeyCode.Q;
    public KeyCode LeftMouseButtonKey { get; private set; } = KeyCode.Mouse0;

    public KeyCode Item1Key { get; private set; } = KeyCode.Alpha1;
    public KeyCode Item2Key { get; private set; } = KeyCode.Alpha2;
    public KeyCode Item3Key { get; private set; } = KeyCode.Alpha3;
    public KeyCode Item4Key { get; private set; } = KeyCode.Alpha4;

    public Vector3 MoveVector3 { get; set; }
    public float CameraX { get; set; }
    public float CameraY { get; set; }

    public event UnityAction SprintKeyClicked;
    public event UnityAction SprintKeyUp;
    public event UnityAction CroughKeyClicked;
    public event UnityAction JumpKeyClicked;
    public event UnityAction TakeKeyClicked;
    public event UnityAction ThroableKeyClicked;
    public event UnityAction LeftMouseButtonKeyClicked;
    public event UnityAction<int> ItemKeyClicked;

    private void Update()
    {
        MoveVector3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CameraX = Input.GetAxis("Mouse X");
        CameraY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(SprintKey))
            SprintKeyClicked?.Invoke();

        if (Input.GetKeyUp(SprintKey))
            SprintKeyUp?.Invoke();

        if (Input.GetKeyDown(CroughKey))
            CroughKeyClicked?.Invoke();

        if (Input.GetKeyDown(JumpKey))
            JumpKeyClicked?.Invoke();

        if (Input.GetKeyDown(TakeKey))
            TakeKeyClicked?.Invoke();

        if (Input.GetKeyDown(ThrowKey))
            ThroableKeyClicked?.Invoke();

        if (Input.GetKeyDown(LeftMouseButtonKey))
            LeftMouseButtonKeyClicked?.Invoke();

        if (Input.GetKeyDown(Item1Key))
            ItemKeyClicked?.Invoke(1);
        else if(Input.GetKeyDown(Item2Key))
            ItemKeyClicked?.Invoke(2);
        else if (Input.GetKeyDown(Item3Key))
            ItemKeyClicked?.Invoke(3);
        else if (Input.GetKeyDown(Item4Key))
            ItemKeyClicked?.Invoke(4);
    }
}
