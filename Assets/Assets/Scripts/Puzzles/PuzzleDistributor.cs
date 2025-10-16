using UnityEngine;
using System.Collections;

namespace Player.Puzzles {
    public class PuzzleRaycaster : MonoBehaviour {
        // get all the puzzles (cubes) and distribute them across the scene
        
        public Transform[] puzzles;
        public float fallbackDistance = 5f;
        public LayerMask obstacleMask;

        public Transform head;

        void Start() {
            // place late because all those layers and colliders stuff take a while to happen
            StartCoroutine(DelayedPlacement());
        }

        IEnumerator DelayedPlacement() {
            // place the puzzles after 10 seconds of being in game. should be enough time for the player to get through the tutorial
            yield return new WaitForSeconds(10f);

            foreach (Transform child in puzzles) {
                Vector3 origin = child.position;

                Vector3 randomDir = Random.onUnitSphere;
                randomDir.y = 0f;
                randomDir.Normalize();

                Vector3 endpoint;

                if (Physics.Raycast(origin, randomDir, out RaycastHit hit, Mathf.Infinity, obstacleMask)) {
                    endpoint = origin + randomDir * hit.distance;
                } else {
                    endpoint = origin + randomDir * fallbackDistance;
                }

                child.position = endpoint;
            }
        }
    }
}