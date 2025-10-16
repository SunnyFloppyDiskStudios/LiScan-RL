using UnityEngine;

namespace Player.MeshMaker {
    public class RoomMeshLayerSetter : MonoBehaviour {
        // another script to set the layer of the room
    
        private int targetLayer = 10; // NOT GAME

        public void SetLayer(MeshFilter meshFilter) {
            // actual layer setter. it's looking for a mesh and then puts it's object on the layer
        
            if (meshFilter is not null) {
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
}