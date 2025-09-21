using UnityEngine;

public class ForceLayer : MonoBehaviour {
    public int keepLayer = 7;
    public int newLayer = 2;

    void Update() {
        foreach (GameObject go in FindObjectsOfType<GameObject>()) {
            if (go.layer != keepLayer) {
                SetLayerRecursively(go, newLayer);
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int layer) {
        if (obj.layer == layer) return;
        obj.layer = layer;
        foreach (Transform child in obj.transform) {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}