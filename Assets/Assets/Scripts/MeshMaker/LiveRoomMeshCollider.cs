using UnityEngine;
using System.Collections.Generic;

namespace Player.MeshMaker {
    public class LiveRoomMeshCollider : MonoBehaviour {
    // create a mesh **collider** LIVE, based on the mesh created by Guardian

    public Transform roomMesh;
    
    private readonly List<GameObject> colliders = new();
    public int roomLayer = 10; // Literally anything that's not GAME

    void Start() {
        RefreshColliders();
        // refresh colliders every so often. better than an update loop or coroutine
        InvokeRepeating(nameof(RefreshColliders), 0f, 3f);
    }

    void RefreshColliders() {
        // make sure all the colliders are accurate and accounted for
        
        // to prevent excess colliders, delete all existing ones
        // too many colliders will cause LAG!
        foreach (var go in colliders) {
            if (go is not null) Destroy(go);
        }
        colliders.Clear();
    
        // find all the objects and create mesh
        var meshObjects = FindObjectsByType<MeshFilter>(FindObjectsSortMode.None);

        foreach (var mf in meshObjects) {
            if (mf.gameObject.CompareTag("Player")) continue;
            if (mf.sharedMesh is null) continue;

            var anchor = mf.GetComponentInParent<OVRSceneAnchor>();

            GameObject go = new GameObject($"RuntimeCollider_{mf.name}");

            SetLayerRecursive(go, roomLayer); // set layer. this has been done in a lot of scripts because of how IMPORTANT it is to do!

            go.transform.parent = roomMesh;

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

    void SetLayerRecursive(GameObject obj, int layer) {
        // layer setter function
        
        obj.layer = layer;
        foreach (Transform child in obj.GetComponentsInChildren<Transform>(true)) {
            child.gameObject.layer = layer;
        }
    }
}
}
