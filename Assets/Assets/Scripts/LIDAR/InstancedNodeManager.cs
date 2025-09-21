using System.Collections.Generic;
using UnityEngine;

public class InstancedNodeManager : MonoBehaviour {
    public static InstancedNodeManager instance;

    public Mesh dotMesh;
    public Material instancedMaterial;
    private List<Matrix4x4> matrices = new();
    private List<Vector4> colors = new();

    private MaterialPropertyBlock mpb;
    private const int MAX_BATCH_SIZE = 1023;
    
    public Camera nodeCamera;

    void Awake() {
        instance = this;
        mpb = new MaterialPropertyBlock();

        if (instancedMaterial != null && !instancedMaterial.enableInstancing) {
            instancedMaterial.enableInstancing = true;
        }
    }

    public void AddInstance(Vector3 position, Color color) {
        matrices.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * 0.025f));
        colors.Add(color);
    }

    void LateUpdate() {
        for (int i = 0; i < matrices.Count; i += MAX_BATCH_SIZE) {
            int count = Mathf.Min(MAX_BATCH_SIZE, matrices.Count - i);
            mpb.Clear();
            mpb.SetVectorArray("_BaseColor", colors.GetRange(i, count).ToArray());

            Graphics.DrawMeshInstanced(
                dotMesh,
                0,
                instancedMaterial,
                matrices.GetRange(i, count).ToArray(),
                count,
                mpb,
                UnityEngine.Rendering.ShadowCastingMode.Off,
                false,
                0,
                nodeCamera
            );
        }
    }
}