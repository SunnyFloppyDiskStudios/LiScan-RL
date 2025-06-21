using UnityEngine;
using System.Collections.Generic;

public class LiveRoomMeshCollider : MonoBehaviour {
    private readonly List<GameObject> colliders = new();

    void Start() {
        InvokeRepeating(nameof(RefreshColliders), 2f, 3f);
    }

    void RefreshColliders() {
        foreach (var go in colliders) {
            if (go != null) Destroy(go);
        }
        colliders.Clear();

        var meshObjects = FindObjectsOfType<MeshFilter>();

        foreach (var mf in meshObjects) {
            if (mf.sharedMesh == null) continue;

            var mc = mf.GetComponent<MeshCollider>();
            var anchor = mf.GetComponentInParent<OVRSceneAnchor>();

            GameObject go = new GameObject($"RuntimeCollider_{mf.name}");
            go.transform.SetPositionAndRotation(mf.transform.position, mf.transform.rotation);
            go.transform.localScale = mf.transform.lossyScale;

            var newMF = go.AddComponent<MeshFilter>();
            newMF.sharedMesh = mf.sharedMesh;

            var newMC = go.AddComponent<MeshCollider>();
            newMC.sharedMesh = mf.sharedMesh;
            newMC.convex = false;

            colliders.Add(go);
        }
    }
}
