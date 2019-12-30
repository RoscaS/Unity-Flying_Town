using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveZoomStrategy : IZoomStrategy
{
    private Vector3 normalizedCameraPosition;
    private float currentZoomLevel;

    public PerspectiveZoomStrategy(Camera cam, Vector3 offset, float startingZoom) {
        float x = 0f;
        float y = Mathf.Abs(offset.y);
        float z = -Mathf.Abs(offset.x);
        normalizedCameraPosition = new Vector3(x, y, z).normalized;
        currentZoomLevel = startingZoom;
        PositionCamera(cam);
    }

    private void PositionCamera(Camera cam) {
        cam.transform.localPosition = normalizedCameraPosition * currentZoomLevel;
    }

    public void ZoomIn(Camera cam, float delta, float nearZoomLimit) {
        if (currentZoomLevel <= nearZoomLimit) return;
        currentZoomLevel = Mathf.Max(currentZoomLevel - delta, nearZoomLimit);
        PositionCamera(cam);
    }

    public void ZoomOut(Camera cam, float delta, float farZoomLimit) {
        if (currentZoomLevel >= farZoomLimit) return;
        currentZoomLevel = Mathf.Min(currentZoomLevel + delta, farZoomLimit);
        PositionCamera(cam);
    }
}
