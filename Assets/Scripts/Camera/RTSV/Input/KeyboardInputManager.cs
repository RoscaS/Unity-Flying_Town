using UnityEngine;

public class KeyboardInputManager : InputManager
{
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    void Update() {
        Directions();
        Rotations();
        Zoom();
    }

    private void Directions() {
        if (Input.GetKey(KeyCode.W)) {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S)) {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A)) {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetKey(KeyCode.D)) {
            OnMoveInput?.Invoke(Vector3.right);
        }
    }
    
    private void Rotations() {
        if (Input.GetKey(KeyCode.E)) {
            OnRotateInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.Q)) {
            OnRotateInput?.Invoke(1f);
        }
    }
    
    private void Zoom() {
        if (Input.GetKey(KeyCode.Z)) {
            OnZoomInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.X)) {
            OnZoomInput?.Invoke(1f);
        }
    }    
}