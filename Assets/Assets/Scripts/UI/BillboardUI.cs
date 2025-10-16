using UnityEngine;

namespace Player.PUI {
    public class BillboardUI : MonoBehaviour {
        // make the UI act like a billboard element
        // billboard UIs face the player at all times
        
        private Camera cam;

        void Start() {
            // get camera
            cam = Camera.main;
        }
    
        void LateUpdate() {
            if (cam is not null) {
                // face the camera to the player (simple enough)
                transform.LookAt(cam.transform, Vector3.up);
            }
        }
    }
}