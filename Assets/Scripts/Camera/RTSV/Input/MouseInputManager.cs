using UnityEngine;

public class MouseInputManager : InputManager
{
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private Vector2Int screen;
    private float mousePositionOnRotateStart;

    private void Awake() {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update() {
        Vector3 mp = Input.mousePosition;
        bool x = (mp.x <= screen.x * 1.05f && mp.x >= screen.x * -0.05f);
        bool y = (mp.y <= screen.y * 1.05f && mp.y >= screen.y * -0.05f);
        if (!(x && y)) return;

        Position(mp);
        Rotation(mp);
        Zoom();
    }

    private void Position(Vector3 mp) {
        if (mp.x > screen.x * 0.95f) {
            OnMoveInput?.Invoke(Vector3.right);
        }
        else if (mp.x < screen.x * 0.05f) {
            OnMoveInput?.Invoke(-Vector3.right);
        }

        if (mp.y > screen.y * 0.95f) {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        else if (mp.y < screen.y * 0.05f) {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
    }

    private void Rotation(Vector3 mp) {
        if (Input.GetMouseButtonDown(1)) {
            mousePositionOnRotateStart = mp.x;
        }
        else if (Input.GetMouseButton(1)) {
            if (mp.x < mousePositionOnRotateStart) {
                OnRotateInput?.Invoke(-1f);
            }
            else if (mp.x > mousePositionOnRotateStart) {
                OnRotateInput?.Invoke(1f);
            }
        }
    }

    private void Zoom() {
        if (Input.mouseScrollDelta.y > 0) {
            OnZoomInput?.Invoke(-3f);
        }
        else if (Input.mouseScrollDelta.y < 0) {
            OnZoomInput?.Invoke(3f);
        }
    }
}
