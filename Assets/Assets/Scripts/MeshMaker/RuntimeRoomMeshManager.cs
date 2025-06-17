using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class SpatialMeshColliderUpdater : MonoBehaviour {
    private MeshCollider combinedCollider;
    private Mesh combinedMesh;

    void Start() {
        combinedCollider = gameObject.AddComponent<MeshCollider>();
        combinedMesh = new Mesh();

        BuildSceneMeshCollider();
    }
    
    public class SceneCaptureTrigger : MonoBehaviour {
        public OVRSceneManager sceneManager;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                sceneManager.RequestSceneCapture();
            }
        }
    }

    void BuildSceneMeshCollider() {
        var sceneMeshes = FindObjectsOfType<OVRScenePlaneMeshFilter>();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        int vertexOffset = 0;

        foreach (var plane in sceneMeshes) {
            Mesh mesh = plane.GetComponent<MeshFilter>().sharedMesh;
            if (mesh == null) continue;

            var verts = mesh.vertices;
            var tris = mesh.triangles;

            for (int i = 0; i < verts.Length; i++) {
                Vector3 worldVert = plane.transform.TransformPoint(verts[i]);
                vertices.Add(transform.InverseTransformPoint(worldVert));
            }

            for (int i = 0; i < tris.Length; i++) {
                triangles.Add(tris[i] + vertexOffset);
            }

            vertexOffset += verts.Length;
        }

        if (vertices.Count > 0) {
            combinedMesh.Clear();
            combinedMesh.SetVertices(vertices);
            combinedMesh.SetTriangles(triangles, 0);
            combinedMesh.RecalculateNormals();

            combinedCollider.sharedMesh = combinedMesh;
        }
    }
}