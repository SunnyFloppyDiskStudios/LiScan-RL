using Unity.VisualScripting;
using UnityEngine;

namespace Player.Pickup {
    public class HoldableItem : MonoBehaviour {
        private Vector3 _respawnPos;

        public int interactionNodes;

        private bool pass;

        public bool canGrab;
        
        private void Awake() {
            _respawnPos = transform.position;
        }

        private void Start() {
            if (interactionNodes == 0) {
                canGrab = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponent<Outline>().enabled = false;
            }
        }
        
        private void Update() {
            if (transform.position.y < -50f) {
                transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                transform.position = _respawnPos;
                transform.rotation = Quaternion.identity;
            }

            if (interactionNodes < 10) {
                canGrab = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponent<Outline>().enabled = false;
            }

            if (interactionNodes >= 10 & !pass) {
                canGrab = true;
                pass = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<MeshRenderer>().enabled = true;
                transform.GetComponent<Outline>().enabled = true;
            }
        }
    }
}

