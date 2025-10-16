using UnityEngine;

public class BillboardUI : MonoBehaviour {
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }
    
    void LateUpdate() {
        if (cam is not null) {
            transform.LookAt(cam.transform, Vector3.up);
        }
    }
}