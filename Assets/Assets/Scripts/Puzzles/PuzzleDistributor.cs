using UnityEngine;

public class PuzzleRaycaster : MonoBehaviour {
    public Transform[] puzzles;
    public float fallbackDistance = 5f;
    public LayerMask obstacleMask;

    public Transform head;

    void Start() {
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