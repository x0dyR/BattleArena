using UnityEngine;

public class InputSystem
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    public Vector3 ReadMoveDirection() => new(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));

    public bool IsLeftMouseButtonDown() => Input.GetMouseButtonDown(LeftMouseButton);
    public bool IsRightMouseButtonDown() => Input.GetMouseButtonDown(RightMouseButton);

}