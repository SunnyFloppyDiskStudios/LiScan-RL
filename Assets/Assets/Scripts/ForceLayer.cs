using UnityEngine;

public class ForceLayer : MonoBehaviour {
    public int newLayer = 8;

    void Update() {
        foreach (GameObject go in FindObjectsOfType<GameObject>()) {
            if (go.layer != 7 && go.layer != 2) {
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