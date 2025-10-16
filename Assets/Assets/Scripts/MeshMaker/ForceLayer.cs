using UnityEngine;

namespace Player.MeshMaker {
    public class ForceLayer : MonoBehaviour {
        // Force the layer of objects that aren't layer 7 or 8

        public int newLayer = 8;

        void Update() {
            // find all the objects and set their layer
            foreach (GameObject go in FindObjectsByType<GameObject>(FindObjectsSortMode.None)) {
                if (go.layer != 7 && go.layer != 8) {
                    SetLayerRecursively(go, newLayer);
                }
            }
        }

        void SetLayerRecursively(GameObject obj, int layer) {
            // the function to set the layer. technically it could be in the update loop
        
            if (obj.layer == layer) return;

            obj.layer = layer;

            if (obj.GetComponent<BoxCollider>() is null && obj.GetComponent<MeshFilter>() is not null) {
                obj.AddComponent<BoxCollider>();
            }

            foreach (Transform child in obj.transform) {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}