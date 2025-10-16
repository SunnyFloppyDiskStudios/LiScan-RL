using UnityEngine;

namespace Player.Pickup {
    public class HoldableItem : MonoBehaviour {
        // a class for objects that are interactable. don't let the name fool you
        
        private Vector3 _respawnPos;

        public int interactionNodes;

        private bool pass;

        public bool canGrab;
        
        private void Awake() {
            // if the item falls off the map, it should come back to this position
            _respawnPos = transform.position;
        }

        private void Start() {
            // make sure it's invisible
            if (interactionNodes == 0) {
                canGrab = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponent<Outline>().enabled = false;
            }
        }
        
        private void Update() {
            // if it goes under, come back!
            if (transform.position.y < -50f) {
                transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                transform.position = _respawnPos;
                transform.rotation = Quaternion.identity;
            }
            
            // make sure it says invisible
            if (interactionNodes < 10) {
                canGrab = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponent<Outline>().enabled = false;
            }
            
            // allow the object to be interacted
            if (interactionNodes >= 10 & !pass) {
                canGrab = true;
                pass = true;
                transform.GetComponent<MeshRenderer>().enabled = true;
                transform.GetComponent<Outline>().enabled = true;
            }
        }
    }
}

