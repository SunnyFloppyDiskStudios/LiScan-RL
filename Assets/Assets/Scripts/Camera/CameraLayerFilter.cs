using UnityEngine;

public class CameraLayerFilter : MonoBehaviour {
    public int layerToRender = 7; // GAME

    void Start() {
        Camera cam = transform.GetComponent<Camera>();
        if (cam != null) {
            cam.cullingMask = 1 << layerToRender;
        } else {
            Debug.LogWarning("No main camera found!");
        }
    }
}