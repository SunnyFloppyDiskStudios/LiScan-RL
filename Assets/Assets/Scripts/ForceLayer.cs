using UnityEngine;

public class ForceLayer : MonoBehaviour {
    public int newLayer = 8;

    void Update() {
        foreach (GameObject go in FindObjectsOfType<GameObject>()) {
            if (go.layer != 7 && go.layer != 8) {
                SetLayerRecursively(go, newLayer);
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int layer) {
        if (obj.layer == layer) return;

        obj.layer = layer;

        MeshFilter mf = obj.GetComponent<MeshFilter>();
        if (mf is not null && mf.sharedMesh is not null) {
            MeshCollider mc = obj.GetComponent<MeshCollider>();
            if (mc is null) mc = obj.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.sharedMesh;
            mc.convex = true;
        }

        foreach (Transform child in obj.transform) {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}