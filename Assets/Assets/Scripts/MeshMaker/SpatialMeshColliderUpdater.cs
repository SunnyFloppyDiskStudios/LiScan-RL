using UnityEngine;

public class SpatialMeshColliderUpdater : MonoBehaviour {
    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    private Mesh lastMesh;

    void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }

    void Update() {
        var currentMesh = meshFilter.sharedMesh;
        if (currentMesh != null && currentMesh != lastMesh) {
            lastMesh = currentMesh;
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = currentMesh;
            Debug.Log("[RoomMesh] Collider updated with new mesh.");
        }
    }
}