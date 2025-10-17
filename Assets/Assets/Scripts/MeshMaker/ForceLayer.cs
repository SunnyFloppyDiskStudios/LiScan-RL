using UnityEngine;

namespace Player.MeshMaker {
    public class ForceLayer : MonoBehaviour {
        // Force the layer of objects that aren't layer 7 or 8

        public int newLayer = 8;

        private int ranTimes = 0;
        
        void Start() {
            InvokeRepeating(nameof(FindObjects), 5f, 1f); // so it's not reiterating through HUNDEREDS of objects each frame
        }

        void Update() {
            // i need it to run at least for 4-5 frames because mesh maker isn't immediate
            if (ranTimes < 5) {
                FindObjects();
                ranTimes++;
            }
        }

        void FindObjects() {
            // find all the objects and set their layer
            foreach (GameObject go in FindObjectsByType<GameObject>(FindObjectsSortMode.None)) {
                if (go.layer != 7 && go.layer != 8 && !go.CompareTag("Player")) {
                    SetLayerRecursively(go, newLayer);
                }
            }
        }

        void SetLayerRecursively(GameObject obj, int layer) {
            // the function to set the layer. technically it could be in the update loop
            
            if (obj.layer == 7 || obj.layer == 8) return; // skip because child seems to be important enough to get this layer
        
            if (obj.layer == layer) return; // skip to not waste resources

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