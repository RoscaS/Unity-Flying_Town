﻿using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AutoFocus : MonoBehaviour
{
    public bool debug;
    public LayerMask mask;
    public PostProcessVolume volume;
    public float focusSpeed = 6f;
    public bool focusOnMouse = false;

    private Camera cam;
    private DepthOfField dof;

    void Start() {
        cam = GetComponent<Camera>();
        volume.sharedProfile.TryGetSettings(out dof);
    }

    void LateUpdate() {
        if (!volume) return;
        float distance = FocusedObjectDistance();
        float focusDistance = dof.focusDistance.value;
        float delta = Time.deltaTime * Mathf.Abs(distance - focusDistance) * focusSpeed;

        if (distance > focusDistance) {
            dof.focusDistance.value += delta;
        }
        else if (distance < focusDistance) {
            dof.focusDistance.value -= delta;
        }
    }

    private void OnDisable() {
        dof.focusDistance.value = 40;
    }


    private float FocusedObjectDistance() {
        RaycastHit hit;
        Ray rayToFloor;
        if (focusOnMouse) {
            rayToFloor = cam.ScreenPointToRay(Input.mousePosition);
        }
        else {
            rayToFloor = cam.ViewportPointToRay(new Vector3(.5f, .3f, 0));
        }

        Physics.Raycast(rayToFloor, out hit, 500.0f, mask);
        if (debug) {
            Vector3 start = rayToFloor.origin;
            Vector3 end = rayToFloor.direction * hit.distance;
            Debug.DrawRay(start, end, Color.red, .01f);
        }

        return hit.distance != 0 ? hit.distance : 500f;
    }
}