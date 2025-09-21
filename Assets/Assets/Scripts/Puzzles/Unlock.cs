using UnityEngine;

public class Unlock : MonoBehaviour {
    public Renderer block1;
    public Renderer usb1;
    public Renderer block2;
    public Renderer usb2;
    public Renderer block3;
    public Renderer usb3;

    void Update() {
        if (block1 != null && usb1 != null) {
            usb1.enabled = block1.enabled;
        }

        if (block2 != null && usb2 != null) {
            usb2.enabled = block2.enabled;
        }

        if (block3 != null && usb3 != null) {
            usb3.enabled = block3.enabled;
        }
    }
}