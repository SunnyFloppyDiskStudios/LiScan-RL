using UnityEngine;

namespace Player.PCamera { // PCamera because Camera is a Unity class
    public class CameraLayerFilter : MonoBehaviour {
        // controls what layer(s) the camera should see.
    
        public int layerToRender = 7; // GAME
        public bool includeDefault = true;

        void Start() {
            Camera cam = GetComponent<Camera>();
            if (cam is not null) {
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
}
