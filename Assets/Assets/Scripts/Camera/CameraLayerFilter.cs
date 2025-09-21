using UnityEngine;

public class CameraLayerFilter : MonoBehaviour {
    public int layerToRender = 7; // GAME
    public bool includeDefault = true;

    void Start() {
        Camera cam = GetComponent<Camera>();
        if (cam != null) {
            int mask = 1 << layerToRender;
            if (includeDefault) {
                mask |= 1 << 0;
            }
            cam.cullingMask = mask;
        } else {
            Debug.LogWarning("No camera found on this GameObject!");
        }
    }
}