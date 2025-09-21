using UnityEngine;

public class RoomMeshLayerSetter : MonoBehaviour {
    private int targetLayer = 2; // NOT GAME

    public void SetLayer(MeshFilter meshFilter) {
        if (meshFilter != null) {
            SetLayerRecursively(meshFilter.gameObject, targetLayer);
        }
    }

    private void SetLayerRecursively(GameObject go, int layer) {
        go.layer = layer;
        foreach (Transform child in go.transform) {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}