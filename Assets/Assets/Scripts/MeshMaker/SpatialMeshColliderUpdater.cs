using UnityEngine;

public class SpatialMeshColliderUpdater : MonoBehaviour {
    private MeshCollider meshCollider;
    private RoomMeshAnchor roomMeshAnchor;
    private Mesh lastMesh;

    void Start() {
        meshCollider = GetComponent<MeshCollider>();
        roomMeshAnchor = FindObjectOfType<RoomMeshAnchor>();
    }

    void Update() {
        if (roomMeshAnchor == null) return;

        Mesh currentMesh = roomMeshAnchor.GetComponent<MeshFilter>().sharedMesh;

        if (currentMesh != null && currentMesh != lastMesh) {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = currentMesh;
            lastMesh = currentMesh;
        }
    }
}