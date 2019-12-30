using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")] 
    public Vector2 cameraOffset = new Vector2(8f, 16f);
    public float lookAtOffset = 2f;

    [Header("Move Controls")] 
    public float inOutSpeed = 30f;
    public float lateralSpeed = 30f;
    public float rotateSpeed = 45f;

    [Header("Move Bounds")] 
    public Vector2 minBounds, maxBounds;

    [Header("Zoom Controls")] 
    public float zoomSpeed = 10f;
    public float nearZoomLimit = 4f;
    public float farZoomLimit = 20f;
    public float startingZoomLevel = 14f;

    private IZoomStrategy zoomStrategy;
    private Vector3 frameMove;
    private float frameRotate;
    private float frameZoom;
    private Camera cam;

    private void Awake() {
        float y = Mathf.Abs(cameraOffset.y);
        float z = -Mathf.Abs(cameraOffset.x);
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, y, z);
        zoomStrategy = cam.orthographic
            ? (IZoomStrategy) new OrthographicZoomStrategy(cam, startingZoomLevel)
            : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoomLevel); 
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
    }

    private void OnEnable() {
        KeyboardInputManager.OnMoveInput += UpdateFrameMove;
        KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
        
        MouseInputManager.OnMoveInput += UpdateFrameMove;
        MouseInputManager.OnRotateInput += UpdateFrameRotate;
        MouseInputManager.OnZoomInput += UpdateFrameZoom;
    }

    private void OnDisable() {
        KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
        KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
        KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
        
        MouseInputManager.OnMoveInput -= UpdateFrameMove;
        MouseInputManager.OnRotateInput -= UpdateFrameRotate;
        MouseInputManager.OnZoomInput -= UpdateFrameZoom;
    }

    private void LateUpdate() {
        Position();
        Rotation();
        Zoom();
    }

    private void Position() {
        if (frameMove != Vector3.zero) {
            float time = Time.deltaTime;
            float x = frameMove.x * lateralSpeed;
            float y = frameMove.y;
            float z = frameMove.z * inOutSpeed;
            Vector3 speedModFrameMove = new Vector3(x, y, z);

            transform.position += transform.TransformDirection(speedModFrameMove) * time;
            LockPositionInBounds();
            frameMove = Vector3.zero;
        }
    }

    private void Rotation() {
        if (frameRotate != 0f) {
            transform.Rotate(Vector3.up, frameRotate * Time.deltaTime * rotateSpeed);
            frameRotate = 0f;
        }
    }

    private void Zoom() {
        if (frameZoom < 0f) {
            float delta = Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed;
            zoomStrategy.ZoomIn(cam, delta, nearZoomLimit);
            frameZoom = 0f;
        }
        else if (frameZoom > 0f) {
            float delta = Time.deltaTime * frameZoom * zoomSpeed;
            zoomStrategy.ZoomOut(cam, delta, farZoomLimit);
            frameZoom = 0f;
        }
    }

    private void LockPositionInBounds() {
        float x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float y = transform.position.y;
        float z = Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y);
        transform.position = new Vector3(x, y, z);
    }

    private void UpdateFrameMove(Vector3 movevector) {
        frameMove += movevector;
    }

    private void UpdateFrameRotate(float rotateamount) {
        frameRotate += rotateamount;
    }

    private void UpdateFrameZoom(float zoomamount) {
        frameZoom += zoomamount;
    }
}